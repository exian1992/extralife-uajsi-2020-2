using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using TMPro;
using System.IO;

public class HireNPCManager : MonoBehaviour
{
    //idleManager bwt cek lvl village nnti
    GameData data;

    [SerializeField] public DateTime npc1, npc2;
    public bool npc1Running, npc2Running;
    public bool speedUp1, speedUp2;
    public GameObject[] npc1Button, npc2Button;
    public TextMeshProUGUI[] timer, status;
    public GameObject[] timerObject;

    void Start()
    {
        //load data
        string saveData = Application.persistentDataPath + "/npcData.uwansummoney";
        if (!File.Exists(saveData))
        {
            //SaveProgress();
        }
        else LoadData();
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Village")
        {
            if (npc1Running)
            {
                if (DateTime.Now.Ticks >= npc1.Ticks || speedUp1)
                {
                    npc1Button[0].SetActive(false);
                    npc1Button[1].SetActive(false);
                    npc1Button[2].SetActive(true);

                    timerObject[0].SetActive(false);
                    status[0].text = "Complete";
                }
                else
                {
                    npc1Button[0].SetActive(false);
                    npc1Button[1].SetActive(true);
                    npc1Button[2].SetActive(false);

                    timerObject[0].SetActive(true);
                    status[0].text = "Mining";

                    TimeSpan comparison = npc1 - DateTime.Now;
                    timer[0].text = new DateTime(comparison.Ticks).ToString("HH:mm:ss");
                }
            }
            else
            {
                npc1Button[0].SetActive(true);
                npc1Button[1].SetActive(false);
                npc1Button[2].SetActive(false);

                timerObject[0].SetActive(false);
                status[0].text = "Available";
            }

            if (npc2Running)
            {
                if (DateTime.Now.Ticks >= npc2.Ticks)
                {
                    npc2Button[0].SetActive(false);
                    npc2Button[1].SetActive(false);
                    npc2Button[2].SetActive(true);

                    timerObject[1].SetActive(false);
                    status[1].text = "Complete";
                }
                else
                {
                    npc2Button[0].SetActive(false);
                    npc2Button[1].SetActive(true);
                    npc2Button[2].SetActive(false);

                    timerObject[1].SetActive(true);
                    status[1].text = "Mining";

                    TimeSpan comparison = npc2 - DateTime.Now;
                    timer[1].text = new DateTime(comparison.Ticks).ToString("HH:mm:ss");
                }
            }
            else
            {
                npc2Button[0].SetActive(true);
                npc2Button[1].SetActive(false);
                npc2Button[2].SetActive(false);

                timerObject[1].SetActive(false);
                status[1].text = "Available";
            }
        }
    }
    void LoadData()
    {
        data = SaveSystem.LoadNPCManager();
        npc1 = data.npc1;
        npc2 = data.npc2;
        npc1Running = data.npc1Running;
        npc2Running = data.npc2Running;
        speedUp1 = data.speedUp1;
        speedUp2 = data.speedUp2;
    }
    public void SaveProgress()
    {
        //SaveSystem.SaveNPCManager(this);
    }
    public void Hire1()
    {
        npc1Running = true;
        npc1 = DateTime.Now.AddMinutes(10);
    }
    public void SpeedUp1()
    {
        //ads disini
        speedUp1 = true;
    }
    public void Claim1()
    {
        Debug.Log("Complete mine NPC-1");

        speedUp1 = false;
        npc1Running = false;
    }
    public void Hire2()
    {
        npc2Running = true;
        npc2 = DateTime.Now.AddMinutes(45);
    }
    public void SpeedUp2()
    {
        //ads disini
        speedUp2 = true;
    }
    public void Claim2()
    {
        Debug.Log("Complete mine NPC-2");

        speedUp2 = false;
        npc2Running = false;
    }
}
