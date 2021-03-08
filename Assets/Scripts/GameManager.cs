﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.IO;

public class GameManager : MonoBehaviour
{
    public GameObject manager;
    public GameObject[] prefabs;
    //public GameObject[] floatingOreIcon;
    GameData data;
    GameObject qManager;
    QuestManager questManager;

    public GameObject[] blocks;

    float currentOre = 1;
    public Text currentOreHealth;
    public Text currentOreName;

    //ore info
    public int stone = 0;
    public int coal = 0;
    public int bronze = 0;
    public int iron = 0;
    public Text stoneValue;
    public Text coalValue;
    public Text bronzeValue;
    public Text ironValue;

    //orechance
    public int stoneChance = 20;
    public int coalChance = 0;
    public int bronzeChance = 0;
    public int ironChance = 0;

    //equipment info
    public int eqLvl = 1;
    public Text eqLevel;
    public float attSpd = 1f;

    //coin
    public int coin = 0;
    public Text coinValue;

    //checker
    public bool isItLoaded = false;

    //etc
    public GameObject questPopUp;
    void Start()
    {
        qManager = GameObject.Find("QuestManager");
        questManager = qManager.GetComponent<QuestManager>();

        string saveData = "D:/SaveFile/data.uwansummoney";
        if (!File.Exists(saveData))
        {
            SaveSystem.SaveData(this);
        }
        if (isItLoaded == false)
        {
            questManager.LoadQuestState();
            LoadData();
            isItLoaded = true;
        }
        InvokeRepeating("AttackOre", 1f, 0.5f);

        if (blocks.Length < 4)
        {
            for (int i = 0; i < 3; i++)
            {
                Array.Clear(blocks, 0, blocks.Length);
                blocks = GameObject.FindGameObjectsWithTag("ore");
                TileMove();
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        Array.Clear(blocks, 0, blocks.Length);
        blocks = GameObject.FindGameObjectsWithTag("ore");

        foreach (GameObject block in blocks)
        {
            //ore remover + generator
            Ore ore = block.GetComponent<Ore>();

            Transform tr = block.transform;

            if (tr.transform.position.z <= 0.5f)
            {
                ore.MakeOreActive();

            }
            if (ore.IsOreActive())
            {
                currentOre = ore.GetOreHealth();
                currentOreHealth.text = currentOre.ToString();

                currentOreName.text = ore.GetName();
            }
        }

        //touch input
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;
            AttackOre();
        }

        //tile moving
        if (currentOre <= 0)
        {
            TileCheck();
            Destroy(blocks[0]);
            TileMove();
        }

        RefreshText();
    }
    public void AttackOre()
    {
        Array.Clear(blocks, 0, blocks.Length);
        blocks = GameObject.FindGameObjectsWithTag("ore");
        foreach (GameObject block in blocks)
        {
            Ore ore = block.GetComponent<Ore>();
            if (ore.IsOreActive())
            {
                ore.OreDamage();
                currentOre = ore.GetOreHealth();
            }
        }
        //tap quest
        if(questManager.isThereQuest)
        {
            if (questManager.currentActiveQuest[0].questType == QuestType.Tap)
            {
                questManager.currentActiveQuest[0].Increase(1);
                questManager.QuestCompleteCheck();
            }
            
        }
        RefreshText();
    }
    void RefreshText()
    {
        currentOreHealth.text = currentOre.ToString();

        stoneValue.text = stone.ToString();
        coalValue.text = coal.ToString();
        bronzeValue.text = bronze.ToString();
        ironValue.text = iron.ToString();
        eqLevel.text = eqLvl.ToString();

        coinValue.text = coin.ToString();
    }
    #region Tile Stuff
    void TileMove()
    {
        foreach (GameObject block in blocks)
        {
            Transform tr = block.transform;

            tr.transform.position = new Vector3(tr.transform.position.x, tr.transform.position.y, tr.transform.position.z - 1f);
        }

        TileGenerator();
    }
    void TileGenerator()
    {
        //random generator
        int randomOre = Random.Range(0, 20);

        //stone 10, coal 5, bronze 4, iron 1
        if (randomOre < ironChance)
        {
            Instantiate(prefabs[3], new Vector3(2.45f, -1.5f, 3f), Quaternion.Euler(0, 0, 90));
        }
        else if (randomOre < bronzeChance)//2
        {
            Instantiate(prefabs[2], new Vector3(2.45f, -1.5f, 3f), Quaternion.Euler(0, 0, 90));
        }
        else if (randomOre < coalChance)//6
        {
            Instantiate(prefabs[1], new Vector3(2.45f, -1.5f, 3f), Quaternion.Euler(0, 0, 90));
        }
        else if (randomOre < stoneChance)//12
        {
            Instantiate(prefabs[0], new Vector3(2.45f, -1.5f, 3f), Quaternion.Euler(0, 0, 90));
        }
    }
    void TileCheck()
    {
        foreach (GameObject block in blocks)
        {
            Ore ore = block.GetComponent<Ore>();
            if (ore.IsOreActive())
            {
                if (ore.GetName() == "Stone")
                {
                    stone++;
                    //GameObject temp = Instantiate(floatingOreIcon[0], new Vector3(2.5f, 0f, 0f), Quaternion.identity);
                    //Destroy(temp, 1);
                    //stone quest
                    if (questManager.isThereQuest)
                    {
                        if (questManager.currentActiveQuest[0].questType == QuestType.MineStone)
                        {
                            questManager.currentActiveQuest[0].Increase(1);
                            questManager.QuestCompleteCheck();
                        }
                    }
                }
                if (ore.GetName() == "Coal")
                {
                    coal++;
                    //GameObject temp = Instantiate(floatingOreIcon[1], new Vector3(2.5f, 0f, 0f), Quaternion.identity);
                    //Destroy(temp, 1);
                }
                if (ore.GetName() == "Bronze")
                {
                    bronze++;
                    //GameObject temp = Instantiate(floatingOreIcon[2], new Vector3(2.5f, 0f, 0f), Quaternion.identity);
                    //Destroy(temp, 1);
                }
                if (ore.GetName() == "Iron")
                {
                    iron++;
                    //GameObject temp = Instantiate(floatingOreIcon[3], new Vector3(2.5f, 0f, 0f), Quaternion.identity);
                    //Destroy(temp, 1);
                }
            }
        }
    }
    #endregion
    
    public void GoToShop()
    {
        SaveSystem.SaveData(this);
        SaveSystem.SaveQuestState(questManager);
        SceneManager.LoadScene("Shop");
        Destroy(GameObject.Find("InventoryData"));
        DontDestroyOnLoad(this);
        DontDestroyOnLoad(qManager);
    }
    public float Damage()
    {
        return attSpd;
    }
    public void LoadData()
    {
        data = SaveSystem.LoadData();
        stone = data.stone;
        coal = data.coal;
        bronze = data.bronze;
        iron = data.iron;

        eqLvl = data.eqLvl;
        attSpd = data.attSpd;

        stoneChance = data.stoneChance;
        coalChance = data.coalChance;
        bronzeChance = data.bronzeChance;
        ironChance = data.ironChance;

        coin = data.coin;
    }
    public void ShowQuest()
    {
        questPopUp.SetActive(true);
    }
    public void HideQuest()
    {
        questPopUp.SetActive(false);
    }
    void OnApplicationQuit()
    {
        isItLoaded = false;
        SaveSystem.SaveData(this);
    }
}
