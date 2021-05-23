using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using TMPro;
using System.IO;
using Random = UnityEngine.Random;

public class HireNPCManager : MonoBehaviour
{
    //idleManager bwt cek lvl village nnti
    GameData data;
    IdleManager iManager;

    public DateTime npc1, npc2, npc3;
    public bool npc1Running, npc2Running, npc3Running;
    public bool speedUp1, speedUp2, speedUp3;
    public GameObject[] npc1Button, npc2Button, npc3Button;
    public TextMeshProUGUI[] timer, status;
    public GameObject[] timerObject;
    public string[] hour, map;

    //time and map select variable
    public bool npc1Select, npc2Select, npc3Select, hour1, hour4, hour8, mapWaterfall, mapCave, mapDCave, mapEMantle, mapDwarf, mapECore;
    public Button[] hourBtn, mapBtn;
    public Button confirm;

    //UI refresh location
    public GameObject npcSelect;

    void Start()
    {
        iManager = GameObject.Find("IdleManager").GetComponent<IdleManager>();

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
            if (npcSelect.activeSelf)
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
                if (npc3Running)
                {
                    if (DateTime.Now.Ticks >= npc3.Ticks)
                    {
                        npc3Button[0].SetActive(false);
                        npc3Button[1].SetActive(false);
                        npc3Button[2].SetActive(true);

                        timerObject[2].SetActive(false);
                        status[2].text = "Complete";
                    }
                    else
                    {
                        npc3Button[0].SetActive(false);
                        npc3Button[1].SetActive(true);
                        npc3Button[2].SetActive(false);

                        timerObject[2].SetActive(true);
                        status[2].text = "Mining";

                        TimeSpan comparison = npc3 - DateTime.Now;
                        timer[2].text = new DateTime(comparison.Ticks).ToString("HH:mm:ss");
                    }
                }
                else
                {
                    npc3Button[0].SetActive(true);
                    npc3Button[1].SetActive(false);
                    npc3Button[2].SetActive(false);

                    timerObject[2].SetActive(false);
                    status[2].text = "Available";
                }
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
    public void SpeedUp1()
    {
        //ads disini
        speedUp1 = true;
    }
    public void SpeedUp2()
    {
        //ads disini
        speedUp2 = true;
    }
    public void SpeedUp3()
    {
        //ads disini
        speedUp3 = true;
    }
    public void Hire1()
    {
        npc1Select = true;
    }
    public void Hire2()
    {
        npc2Select = true;
    }
    public void Hire3()
    {
        npc3Select = true;
    }
    public void Claim1()
    {
        float hours = 0, multiplier = 0;
        int mapNumber = -1;

        if (hour[0] == "1")
        {
            hours = 1f;
            multiplier = 1f;
        }
        else if (hour[0] == "4")
        {
            hours = 4f;
            multiplier = 0.9f;
        }
        else if (hour[0] == "8")
        {
            hours = 8f;
            multiplier = 0.8f;
        }

        if (map[0] == "waterfall")
        {
            mapNumber = 0;
        }
        else if (map[0] == "cave")
        {
            mapNumber = 1;
        }
        else if (map[0] == "deepCave")
        {
            mapNumber = 2;
        }
        else if (map[0] == "earthMantle")
        {
            mapNumber = 3;
        }
        else if (map[0] == "dwarf")
        {
            mapNumber = 4;
        }
        else if (map[0] == "earthCore")
        {
            mapNumber = 5;
        }

        CalculateOre(hours, multiplier, mapNumber);
        speedUp1 = false;
        npc1Running = false;
        hour[0] = "";
        map[0] = "";
    }
    public void Claim2()
    {
        float hours = 0, multiplier = 0;
        int mapNumber = -1;

        if (hour[1] == "1")
        {
            hours = 1f;
            multiplier = 1f;
        }
        else if (hour[1] == "4")
        {
            hours = 4f;
            multiplier = 0.9f;
        }
        else if (hour[1] == "8")
        {
            hours = 8f;
            multiplier = 0.8f;
        }

        if (map[1] == "waterfall")
        {
            mapNumber = 0;
        }
        else if (map[1] == "cave")
        {
            mapNumber = 1;
        }
        else if (map[1] == "deepCave")
        {
            mapNumber = 2;
        }
        else if (map[1] == "earthMantle")
        {
            mapNumber = 3;
        }
        else if (map[1] == "dwarf")
        {
            mapNumber = 4;
        }
        else if (map[1] == "earthCore")
        {
            mapNumber = 5;
        }

        CalculateOre(hours, multiplier, mapNumber);
        speedUp2 = false;
        npc2Running = false;
        hour[1] = "";
        map[1] = "";
    }
    public void Claim3()
    {
        float hours = 0, multiplier = 0;
        int mapNumber = -1;

        if (hour[2] == "1")
        {
            hours = 1f;
            multiplier = 1f;
        }
        else if (hour[2] == "4")
        {
            hours = 4f;
            multiplier = 0.9f;
        }
        else if (hour[2] == "8")
        {
            hours = 8f;
            multiplier = 0.8f;
        }

        if (map[2] == "waterfall")
        {
            mapNumber = 0;
        }
        else if (map[2] == "cave")
        {
            mapNumber = 1;
        }
        else if (map[2] == "deepCave")
        {
            mapNumber = 2;
        }
        else if (map[2] == "earthMantle")
        {
            mapNumber = 3;
        }
        else if (map[2] == "dwarf")
        {
            mapNumber = 4;
        }
        else if (map[2] == "earthCore")
        {
            mapNumber = 5;
        }

        CalculateOre(hours, multiplier, mapNumber);
        speedUp3 = false;
        npc3Running = false;
        hour[2] = "";
        map[2] = "";
    }
    void CalculateOre(float hours, float multiplier, int map)
    {
        float baseRandom = 1000;
        int minRandom = (int)(((hours * baseRandom) * multiplier) - 1);
        int maxRandom = (int)(hours * baseRandom);
        int totalRandom = Random.Range(minRandom, maxRandom);
        Debug.Log(totalRandom);

        if (map == -1)
        {
            Debug.Log("shit is wrong");
        }
        else if (map == 0)
        {
            iManager.oreCollection[0] += totalRandom;
        }
        else if (map == 1)
        {
            for (int i = 0; i < totalRandom; i++)
            {
                int randomOre = Random.Range(0, 20);
                for (int j = 2; j >= 0; j--)
                {                    
                    if (randomOre < iManager.map2OreChance[j])
                    {
                        iManager.oreCollection[j + 1]++;
                    }
                }
            }            
        }
        else if (map == 2)
        {
            for (int i = 0; i < totalRandom; i++)
            {
                int randomOre = Random.Range(0, 20);
                for (int j = 2; j >= 0; j--)
                {                    
                    if (randomOre < iManager.map3OreChance[j])
                    {
                        iManager.oreCollection[j + 4]++;
                    }
                }
            }            
        }
        else if (map == 3)
        {
            for (int i = 0; i < totalRandom; i++)
            {
                int randomOre = Random.Range(0, 20);
                for (int j = 2; j >= 0; j--)
                {                    
                    if (randomOre < iManager.map4OreChance[j])
                    {
                        iManager.oreCollection[j + 7]++;
                    }
                }
            }            
        }
        else if (map == 4)
        {
            iManager.oreCollection[10] += totalRandom;
        }
        else if (map == 5)
        {
            for (int i = 0; i < totalRandom; i++)
            {
                int randomOre = Random.Range(0, 20);
                for (int j = 2; j >= 0; j--)
                {
                    if (randomOre < iManager.map6OreChance[j])
                    {
                        iManager.oreCollection[j + 11]++;
                    }
                }
            }
        }
    }
    #region Time and map select
    public void Hour1()
    {
        if (!hour1)
        {
            hour1 = true;

            for (int i = 0; i < mapBtn.Length; i++)
            {
                mapBtn[i].interactable = true;
            }
            hourBtn[1].interactable = false;
            hourBtn[2].interactable = false;
        }
        else
        {
            hour1 = false;

            for (int i = 0; i < mapBtn.Length; i++)
            {
                mapBtn[i].interactable = false;
            }
            hourBtn[1].interactable = true;
            hourBtn[2].interactable = true;

            mapWaterfall = false;
            mapCave = false;
            mapDCave = false;
            mapEMantle = false;
            mapDwarf = false;
            mapECore = false;

            confirm.interactable = false;
        }
    }
    public void Hour4()
    {
        if (!hour4)
        {
            hour4 = true;

            for (int i = 0; i < mapBtn.Length; i++)
            {
                mapBtn[i].interactable = true;
            }
            hourBtn[0].interactable = false;
            hourBtn[2].interactable = false;
        }
        else
        {
            hour4 = false;

            for (int i = 0; i < mapBtn.Length; i++)
            {
                mapBtn[i].interactable = false;
            }
            hourBtn[0].interactable = true;
            hourBtn[2].interactable = true;

            mapWaterfall = false;
            mapCave = false;
            mapDCave = false;
            mapEMantle = false;
            mapDwarf = false;
            mapECore = false;

            confirm.interactable = false;
        }
    }
    public void Hour8()
    {
        if (!hour8)
        {
            hour8 = true;

            for (int i = 0; i < mapBtn.Length; i++)
            {
                mapBtn[i].interactable = true;
            }
            hourBtn[0].interactable = false;
            hourBtn[1].interactable = false;
        }
        else
        {
            hour8 = false;

            for (int i = 0; i < mapBtn.Length; i++)
            {
                mapBtn[i].interactable = false;
            }
            hourBtn[0].interactable = true;
            hourBtn[1].interactable = true;

            mapWaterfall = false;
            mapCave = false;
            mapDCave = false;
            mapEMantle = false;
            mapDwarf = false;
            mapECore = false;

            confirm.interactable = false;
        }
    }
    public void MapWaterfall()
    {
        if (!mapWaterfall)
        {
            mapWaterfall = true;
            int temp = 0;
            for (int i = 0; i < mapBtn.Length; i++)
            {
                if (!(temp == i))
                {
                    mapBtn[i].interactable = false;
                }
            }
            confirm.interactable = true;
        }
        else
        {
            mapWaterfall = false;
            for (int i = 0; i < mapBtn.Length; i++)
            {
                mapBtn[i].interactable = true;
            }
            confirm.interactable = false;
        }
    }
    public void MapCave()
    {
        if (!mapCave)
        {
            mapCave = true;
            int temp = 1;
            for (int i = 0; i < mapBtn.Length; i++)
            {
                if (!(temp == i))
                {
                    mapBtn[i].interactable = false;
                }
            }
            confirm.interactable = true;
        }
        else
        {
            mapCave = false;
            for (int i = 0; i < mapBtn.Length; i++)
            {
                mapBtn[i].interactable = true;
            }
            confirm.interactable = false;
        }
    }
    public void MapDeepCave()
    {
        if (!mapDCave)
        {
            mapDCave = true;
            int temp = 2;
            for (int i = 0; i < mapBtn.Length; i++)
            {
                if (!(temp == i))
                {
                    mapBtn[i].interactable = false;
                }
            }
            confirm.interactable = true;
        }
        else
        {
            mapDCave = false;
            for (int i = 0; i < mapBtn.Length; i++)
            {
                mapBtn[i].interactable = true;
            }
            confirm.interactable = false;
        }
    }
    public void MapEarthMantle()
    {
        if (!mapEMantle)
        {
            mapEMantle = true;
            int temp = 3;
            for (int i = 0; i < mapBtn.Length; i++)
            {
                if (!(temp == i))
                {
                    mapBtn[i].interactable = false;
                }
            }
            confirm.interactable = true;
        }
        else
        {
            mapEMantle = false;
            for (int i = 0; i < mapBtn.Length; i++)
            {
                mapBtn[i].interactable = true;
            }
            confirm.interactable = false;
        }
    }
    public void MapDwarfVillage()
    {
        if (!mapDwarf)
        {
            mapDwarf = true;
            int temp = 4;
            for (int i = 0; i < mapBtn.Length; i++)
            {
                if (!(temp == i))
                {
                    mapBtn[i].interactable = false;
                }
            }
            confirm.interactable = true;
        }
        else
        {
            mapDwarf = false;
            for (int i = 0; i < mapBtn.Length; i++)
            {
                mapBtn[i].interactable = true;
            }
            confirm.interactable = false;
        }
    }
    public void MapEarthCore()
    {
        if (!mapECore)
        {
            mapECore = true;
            int temp = 5;
            for (int i = 0; i < mapBtn.Length; i++)
            {
                if (!(temp == i))
                {
                    mapBtn[i].interactable = false;
                }
            }
            confirm.interactable = true;
        }
        else
        {
            mapECore = false;
            for (int i = 0; i < mapBtn.Length; i++)
            {
                mapBtn[i].interactable = true;
            }
            confirm.interactable = false;
        }
    }
    #endregion
    public void Confirm()
    {
        if ((npc1Select || npc2Select || npc3Select) && (hour1 || hour4 || hour8) && (mapWaterfall || mapCave || mapDCave || mapEMantle || mapDwarf || mapECore))
        {
            if (npc1Select)
            {
                #region Time Select
                if (hour1)
                {
                    hour[0] = "1";
                    npc1 = DateTime.Now.AddHours(1);
                }
                else if (hour4)
                {
                    hour[0] = "4";
                    npc1 = DateTime.Now.AddHours(4);
                }
                else if (hour8)
                {
                    hour[0] = "8";
                    npc1 = DateTime.Now.AddHours(8);
                }
                #endregion
                #region Map Select
                if (mapWaterfall)
                {
                    map[0] = "waterfall";
                }
                else if (mapCave)
                {
                    map[0] = "cave";
                }
                else if (mapDCave)
                {
                    map[0] = "deepCave";
                }
                else if (mapEMantle)
                {
                    map[0] = "earthMantle";
                }
                else if (mapDwarf)
                {
                    map[0] = "dwarf";
                }
                else if (mapECore)
                {
                    map[0] = "earthCore";
                }
                #endregion

                npc1Running = true;
            }
            else if (npc2Select)
            {
                #region Time Select
                if (hour1)
                {
                    hour[1] = "1";
                    npc2 = DateTime.Now.AddHours(1);
                }
                else if (hour4)
                {
                    hour[1] = "4";
                    npc2 = DateTime.Now.AddHours(4);
                }
                else if (hour8)
                {
                    hour[1] = "8";
                    npc2 = DateTime.Now.AddHours(8);
                }
                #endregion
                #region Map Select
                if (mapWaterfall)
                {
                    map[1] = "waterfall";
                }
                else if (mapCave)
                {
                    map[1] = "cave";
                }
                else if (mapDCave)
                {
                    map[1] = "deepCave";
                }
                else if (mapEMantle)
                {
                    map[1] = "earthMantle";
                }
                else if (mapDwarf)
                {
                    map[1] = "dwarf";
                }
                else if (mapECore)
                {
                    map[1] = "earthCore";
                }
                #endregion

                npc2Running = true;       
            }

            else if (npc3Select)
            {
                #region Time Select
                if (hour1)
                {
                    hour[2] = "1";
                    npc3 = DateTime.Now.AddHours(1);
                }
                else if (hour4)
                {
                    hour[2] = "4";
                    npc3 = DateTime.Now.AddHours(4);
                }
                else if (hour8)
                {
                    hour[2] = "8";
                    npc3 = DateTime.Now.AddHours(8);
                }
                #endregion
                #region Map Select
                if (mapWaterfall)
                {
                    map[2] = "waterfall";
                }
                else if (mapCave)
                {
                    map[2] = "cave";
                }
                else if (mapDCave)
                {
                    map[2] = "deepCave";
                }
                else if (mapEMantle)
                {
                    map[2] = "earthMantle";
                }
                else if (mapDwarf)
                {
                    map[2] = "dwarf";
                }
                else if (mapECore)
                {
                    map[2] = "earthCore";
                }
                #endregion

                npc3Running = true;
            }

            TimeMapReset();
        }
        else Debug.Log("something is not selected yet...");
    }
    public void TimeMapReset()
    {
        npc1Select = false;
        npc2Select = false;
        npc3Select = false;

        hour1 = false;
        hour4 = false;
        hour8 = false;

        mapWaterfall = false;
        mapCave = false;
        mapDCave = false;
        mapEMantle = false;
        mapDwarf = false;
        mapECore = false;

        for (int i = 0; i < hourBtn.Length; i++)
        {
            hourBtn[i].interactable = true;
        }

        for (int i = 0; i < mapBtn.Length; i++)
        {
            mapBtn[i].interactable = false;
        }

        confirm.interactable = false;
    }
}
