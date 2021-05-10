﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.IO;

public class IdleManager : MonoBehaviour
{
    public GameObject[] managerChecker;
    public GameObject[] prefabs;
    GameData data, pData;

    //GameObject qManager, pManager, cManager;
    //QuestManager questManager;
    //PetManager petManager;
    //CostumeManager costumeManager;

    public GameObject currentBlock;

    float currentOreHealth;

    //ore info
    public int[] oreCollection;
    public Text[] mapOreValueText;

    //orechance
    public int[] map12OreChance;
    public int[] map3OreChance;
    public int[] map4OreChance;

    #region Equipment Info
    public int[] eqLevel;
    public float[] eqAttack;
    public int[] eqBasePrice;
    public int[] eqLatestPrice;
    public float tempMiningSpeed;
    public float tempMiningPower;
    public float trueMiningSpeed;
    public float trueMiningPower;
    #endregion

    //coin
    public int coin = 0;

    //booster variable
    //public bool speedUpToggle = false;
    //public bool powerUpToggle = false;

    //etc
    //public GameObject questPopUp, dQuestPopUp;

    void Start()
    {
        managerChecker = GameObject.FindGameObjectsWithTag("gManager");
        if (managerChecker.Length == 2)
        {
            Destroy(managerChecker[0]);
            DontDestroyOnLoad(managerChecker[1]);     
        }
        else
        {
            DontDestroyOnLoad(managerChecker[0]);            
        }        
        //qManager = GameObject.Find("QuestManager");
        //questManager = qManager.GetComponent<QuestManager>();
        //pManager = GameObject.Find("PetManager");
        //petManager = pManager.GetComponent<PetManager>();
        //cManager = GameObject.Find("CostumeManager");
        //costumeManager = cManager.GetComponent<CostumeManager>();

        string saveData = Application.persistentDataPath + "/dataBank.uwansummoney"; //always change this every update, and on saveSystem
        string previousData = Application.persistentDataPath + "/data.uwansummoney"; //this too, on saveSystem also
        if (!File.Exists(saveData))
        {
            Debug.Log("opt 1");
            if (File.Exists(previousData))
            {
                TransferData();
                File.Delete(previousData);
            }

            ChanceChecker();
            SaveAllProgress();
            data = SaveSystem.LoadData();            
        }
        else
        {
            data = SaveSystem.LoadData();

            //questManager.LoadQuestState();
            Debug.Log("opt 2");
            LoadData();
            SaveAllProgress();
        }


        #region Speed Check
        trueMiningSpeed = 1f;
        /*if (costumeManager.costumeEquipped && costumeManager.currentActiveCostume.statusTypeCostume == StatusTypeCostume.SpeedUp)
        {
            trueMiningSpeed = defaultMiningSpeed - (defaultMiningSpeed * costumeManager.currentActiveCostume.statusValue / 100);
        }
        if (petManager.petEquipped && petManager.currentActivePet.statusTypePet == StatusTypePet.SpeedUp)
        {
            if (trueMiningSpeed == 1f)
            {
                trueMiningSpeed = defaultMiningSpeed - (defaultMiningSpeed * petManager.currentActivePet.statusValue / 100);
            }
            else
                trueMiningSpeed -= trueMiningSpeed - (trueMiningSpeed * petManager.currentActivePet.statusValue / 100);
        }*/
        tempMiningSpeed = trueMiningSpeed; //for booster purposes
        /*if (speedUpToggle)
        {
            trueMiningSpeed *= 50 / 100;
        }*/
        InvokeRepeating("AttackOre", 1f, trueMiningSpeed);
        #endregion

        ChanceChecker();
        if (currentBlock == null)
        {
            TileGenerator();
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Waterfall" ||
            SceneManager.GetActiveScene().name == "Cave" ||
            SceneManager.GetActiveScene().name == "DeepCave" ||
            SceneManager.GetActiveScene().name == "EarthMantle")
        {
            //ore remover + generator
            currentBlock = GameObject.FindGameObjectWithTag("ore");
            Ore ore = currentBlock.GetComponent<Ore>();

            currentOreHealth = ore.GetOreHealth();

            //touch input
            if (Input.GetMouseButtonDown(0))
            {
                //if (EventSystem.current.currentSelectedGameObject == null) return;
                #region Tap Quest
                /*if (questManager.activeEQuest.questType == QuestType.Tap)
                {
                    questManager.activeEQuest.Increase(1);
                }
                if (questManager.activeIQuest.questType == QuestType.Tap)
                {
                    questManager.activeIQuest.Increase(1);
                }
                if (questManager.activeHQuest.questType == QuestType.Tap)
                {
                    questManager.activeHQuest.Increase(1);
                }
                if (questManager.isThereQuest)
                {
                    if (questManager.currentActiveQuest.questType == QuestType.Tap)
                    {
                        questManager.currentActiveQuest.Increase(1);
                        questManager.QuestCompleteCheck();
                    }
                }*/
                #endregion
                AttackOre();
                Debug.Log(EventSystem.current.currentSelectedGameObject);
            }

            //tile replaced
            if (currentOreHealth <= 0)
            {
                TileCheck();
                Destroy(currentBlock);
                TileGenerator();
            }

            RefreshText();
        }
    }
    public void AttackOre()
    {
        if (SceneManager.GetActiveScene().name == "Waterfall" ||
            SceneManager.GetActiveScene().name == "Cave" ||
            SceneManager.GetActiveScene().name == "DeepCave" ||
            SceneManager.GetActiveScene().name == "EarthMantle")
        {
            currentBlock = GameObject.FindGameObjectWithTag("ore");
            Ore ore = currentBlock.GetComponent<Ore>();

            ore.OreDamage();
            currentOreHealth = ore.GetOreHealth();

            RefreshText();
        }
    }
    void RefreshText()
    {
        if (SceneManager.GetActiveScene().name == "Waterfall")
        {
            mapOreValueText[0].text = oreCollection[0].ToString();
        }
        else if (SceneManager.GetActiveScene().name == "Cave")
        {
            mapOreValueText[0].text = oreCollection[1].ToString();
            mapOreValueText[1].text = oreCollection[2].ToString();
            mapOreValueText[2].text = oreCollection[3].ToString();
        }
        else if (SceneManager.GetActiveScene().name == "DeepCave")
        {
            mapOreValueText[0].text = oreCollection[4].ToString();
            mapOreValueText[1].text = oreCollection[5].ToString();
            mapOreValueText[2].text = oreCollection[6].ToString();
        }
        else if (SceneManager.GetActiveScene().name == "EarthMantle")
        {
            mapOreValueText[0].text = oreCollection[7].ToString();
            mapOreValueText[1].text = oreCollection[8].ToString();
            mapOreValueText[2].text = oreCollection[9].ToString();
        }
    }
    #region Tile Stuff
    void TileGenerator()
    {
        //random generator
        int randomOre = Random.Range(0, 20);

        //stone 10, coal 5, bronze 4, iron 1
        if(SceneManager.GetActiveScene().name == "Waterfall")
            if (randomOre < map12OreChance[0])//stoneChance)
            {
                Instantiate(prefabs[0], new Vector3(0f, 0f, 0f), Quaternion.Euler(0, 0, 90));
            }

        if (SceneManager.GetActiveScene().name == "Cave")
        {
            if (randomOre < map12OreChance[3])//ironChance)
            {
                Instantiate(prefabs[2], new Vector3(0f, 0f, 0f), Quaternion.Euler(0, 0, 90));
            }
            else if (randomOre < map12OreChance[2])//copperChance)
            {
                Instantiate(prefabs[1], new Vector3(0f, 0f, 0f), Quaternion.Euler(0, 0, 90));
            }
            else if (randomOre < map12OreChance[1])//coalChance)
            {
                Instantiate(prefabs[0], new Vector3(0f, 0f, 0f), Quaternion.Euler(0, 0, 90));
            }
        }
        if (SceneManager.GetActiveScene().name == "DeepCave")
        {
            if (randomOre < map3OreChance[2])//titaniumChance)
            {
                Instantiate(prefabs[2], new Vector3(0f, 0f, 0f), Quaternion.Euler(0, 0, 90));
            }
            else if (randomOre < map3OreChance[1])//rubyChance)
            {
                Instantiate(prefabs[1], new Vector3(0f, 0f, 0f), Quaternion.Euler(0, 0, 90));
            }
            else if (randomOre < map3OreChance[0])//goldChance)
            {
                Instantiate(prefabs[0], new Vector3(0f, 0f, 0f), Quaternion.Euler(0, 0, 90));
            }
        }
        if (SceneManager.GetActiveScene().name == "EarthMantle")
        {
            if (randomOre < map4OreChance[2])//emeraldChance)
            {
                Instantiate(prefabs[2], new Vector3(0f, 0f, 0f), Quaternion.Euler(0, 0, 90));
            }
            else if (randomOre < map4OreChance[1])//sapphireChance)
            {
                Instantiate(prefabs[1], new Vector3(0f, 0f, 0f), Quaternion.Euler(0, 0, 90));
            }
            else if (randomOre < map4OreChance[0])//hprubyChance)
            {
                Instantiate(prefabs[0], new Vector3(0f, 0f, 0f), Quaternion.Euler(0, 0, 90));
            }
        }
    }
    void TileCheck()
    {
        Ore ore = currentBlock.GetComponent<Ore>();
        if (ore.GetName() == "Stone")
        {
            oreCollection[0]++;
            #region Stone mining quest
            /*if (questManager.isThereQuest)
            {
                if (questManager.currentActiveQuest.questType == QuestType.MineStone)
                {
                    questManager.currentActiveQuest.Increase(1);
                }
            }
            if (questManager.activeEQuest.questType == QuestType.MineStone)
            {
                questManager.activeEQuest.Increase(1);
            }
            if (questManager.activeIQuest.questType == QuestType.MineStone)
            {
                questManager.activeIQuest.Increase(1);
            }
            if (questManager.activeHQuest.questType == QuestType.MineStone)
            {
                questManager.activeHQuest.Increase(1);
            }
            questManager.QuestCompleteCheck();*/
            #endregion
        }
        if (ore.GetName() == "Coal")
        {
            oreCollection[1]++;
            #region Coal mining quest
            /*if (questManager.isThereQuest)
            {
                if (questManager.currentActiveQuest.questType == QuestType.MineCoal)
                {
                    questManager.currentActiveQuest.Increase(1);
                }
            }
            if (questManager.activeEQuest.questType == QuestType.MineCoal)
            {
                questManager.activeEQuest.Increase(1);
            }
            if (questManager.activeIQuest.questType == QuestType.MineCoal)
            {
                questManager.activeIQuest.Increase(1);
            }
            if (questManager.activeHQuest.questType == QuestType.MineCoal)
            {
                questManager.activeHQuest.Increase(1);
            }
            questManager.QuestCompleteCheck();*/
            #endregion
        }
        if (ore.GetName() == "Copper")
        {
            oreCollection[2]++;
            #region Copper mining quest
            /*if (questManager.isThereQuest)
            {
                if (questManager.currentActiveQuest.questType == QuestType.MineCopper)
                {
                    questManager.currentActiveQuest.Increase(1);
                }
            }
            if (questManager.activeEQuest.questType == QuestType.MineCopper)
            {
                questManager.activeEQuest.Increase(1);
            }
            if (questManager.activeIQuest.questType == QuestType.MineCopper)
            {
                questManager.activeIQuest.Increase(1);
            }
            if (questManager.activeHQuest.questType == QuestType.MineCopper)
            {
                questManager.activeHQuest.Increase(1);
            }
            questManager.QuestCompleteCheck();*/
            #endregion
        }
        if (ore.GetName() == "Iron")
        {
            oreCollection[3]++;
            #region Iron mining quest
            /*if (questManager.isThereQuest)
            {
                if (questManager.currentActiveQuest.questType == QuestType.MineIron)
                {
                    questManager.currentActiveQuest.Increase(1);
                }
            }
            if (questManager.activeEQuest.questType == QuestType.MineIron)
            {
                questManager.activeEQuest.Increase(1);
            }
            if (questManager.activeIQuest.questType == QuestType.MineIron)
            {
                questManager.activeIQuest.Increase(1);
            }
            if (questManager.activeHQuest.questType == QuestType.MineIron)
            {
                questManager.activeHQuest.Increase(1);
            }
            questManager.QuestCompleteCheck();*/
            #endregion
        }
        if (ore.GetName() == "Gold")
        {
            oreCollection[4]++;
        }
        if (ore.GetName() == "Ruby")
        {
            oreCollection[5]++;
        }
        if (ore.GetName() == "Titanium")
        {
            oreCollection[6]++;
        }
        if (ore.GetName() == "HPRuby")
        {
            oreCollection[7]++;
        }
        if (ore.GetName() == "Sapphire")
        {
            oreCollection[8]++;
        }
        if (ore.GetName() == "Emerald")
        {
            oreCollection[9]++;
        }
    }
    #endregion

    #region Boosters
    public void InstantGain()
    {
        for (int i = 0; i < 1000; i++)
        {
            int randomGain = Random.Range(0, 20);
            //stone 10, coal 5, bronze 4, iron 1
            if (randomGain < map12OreChance[3])//ironChance)
            {
                oreCollection[3]++;
            }
            else if (randomGain < map12OreChance[2])//copperChance)//2
            {
                oreCollection[2]++;
            }
            else if (randomGain < map12OreChance[1])//coalChance)//6
            {
                oreCollection[1]++;
            }
            else if (randomGain < map12OreChance[0])//stoneChance)//12
            {
                oreCollection[0]++;
            }
        }
    }
    public void AutoClicker()
    {
        /*if (!speedUpToggle)
        {
            trueMiningSpeed = trueMiningSpeed * 50 / 100;
            speedUpToggle = true;
        }
        else
        {
            trueMiningSpeed = tempMiningSpeed;
            speedUpToggle = false;
        }*/
        CancelInvoke();
        InvokeRepeating("AttackOre", 1f, trueMiningSpeed);
    }
    public void PowerUp()
    {
        /*if (!powerUpToggle)
        {
            powerUpToggle = true;
        }
        else powerUpToggle = false;*/
    }
    #endregion
    public float Damage()
    {
        #region Equipment Check
        if (SceneManager.GetActiveScene().name == "Waterfall")
        {
            trueMiningPower = eqAttack[0];
        }
        else if (SceneManager.GetActiveScene().name == "Cave")
        {
            trueMiningPower = eqAttack[1];
        }
        else if (SceneManager.GetActiveScene().name == "DeepCave")
        {
            trueMiningPower = eqAttack[2];
        }
        else if (SceneManager.GetActiveScene().name == "EarthMantle")
        {
            trueMiningPower = eqAttack[3];
        }
        #endregion
        /*if (costumeManager.costumeEquipped && costumeManager.currentActiveCostume.statusTypeCostume == StatusTypeCostume.PowerUp)
        {
            trueMiningPower += trueMiningPower * costumeManager.currentActiveCostume.statusValue / 100;
        }
        if (petManager.petEquipped && petManager.currentActivePet.statusTypePet == StatusTypePet.PowerUp)
        {
            trueMiningPower += trueMiningPower * petManager.currentActivePet.statusValue / 100;
        }
        if (powerUpToggle)
        {
            trueMiningPower += trueMiningPower * 50 / 100;
        }*/
        return trueMiningPower;
    }
    public void LoadData()
    {
        for (int i = 0; i < oreCollection.Length; i++)
        {
            oreCollection[i] = data.oreCollection[i];
        }

        for (int i = 0; i < eqLevel.Length; i++)
        {
            if (data.eqLevel[i] == 0)
            {
                eqLevel[i] = 1;
            }
            else eqLevel[i] = data.eqLevel[i];

            eqAttack[i] = data.eqAttack[i];
            eqBasePrice[i] = data.eqBasePrice[i];
            eqLatestPrice[i] = data.eqLatestPrice[i];
        }

        coin = data.coin;
    }
    public void ShowQuest()
    {
        //questPopUp.SetActive(true);
    }
    public void HideQuest()
    {
        //questPopUp.SetActive(false);
        //dQuestPopUp.SetActive(false);
    }
    public void SwitchDaily()
    {
        //questPopUp.SetActive(false);
        //dQuestPopUp.SetActive(true);
    }
    public void SwitchQuest()
    {
        //questPopUp.SetActive(true);
        //dQuestPopUp.SetActive(false);
    }
    void OnApplicationQuit()
    {
        SaveAllProgress();
    }
    public void SaveAllProgress()
    {
        SaveSystem.SaveData(this);
        //SaveSystem.SaveQuestState(questManager);
        //SaveSystem.SavePetManager(petManager);
        //SaveSystem.SaveCostumeManager(costumeManager);
    }
    public void ChanceChecker()
    {
        #region Gloves/Waterfall
        if (eqLevel[0] <= 10)
        {
            map12OreChance[0] = 20;
        }
        #endregion
        #region Pickaxe/Cave
        if (eqLevel[1] <= 10)
        {
            map12OreChance[1] = 20;
            map12OreChance[2] = 0;
            map12OreChance[3] = 0;
        }
        else if (eqLevel[1] <= 20)
        {
            map12OreChance[1] = 20;
            map12OreChance[2] = 6;
            map12OreChance[3] = 0;
        }
        else if (eqLevel[1] <= 30)
        {
            map12OreChance[1] = 20;
            map12OreChance[2] = 8;
            map12OreChance[3] = 2;
        }
        #endregion
        #region TNT/DeepCave
        if (eqLevel[2] <= 10)
        {
            map3OreChance[0] = 20;
            map3OreChance[1] = 0;
            map3OreChance[2] = 0;
        }
        else if (eqLevel[2] <= 20)
        {
            map3OreChance[0] = 20;
            map3OreChance[1] = 6;
            map3OreChance[2] = 0;
        }
        else if (eqLevel[2] <= 30)
        {
            map3OreChance[0] = 20;
            map3OreChance[1] = 8;
            map3OreChance[2] = 2;
        }
        #endregion
        #region HandDrill/EarthMantle
        if (eqLevel[2] <= 10)
        {
            map4OreChance[0] = 20;
            map4OreChance[1] = 0;
            map4OreChance[2] = 0;
        }
        else if (eqLevel[2] <= 20)
        {
            map4OreChance[0] = 20;
            map4OreChance[1] = 6;
            map4OreChance[2] = 0;
        }
        else if (eqLevel[2] <= 30)
        {
            map4OreChance[0] = 20;
            map4OreChance[1] = 8;
            map4OreChance[2] = 2;
        }
        #endregion
    }
    public void TransferData()
    {
        pData = SaveSystem.LoadPreviousData();
        oreCollection[0] = pData.stone;
        oreCollection[1] = pData.coal;
        oreCollection[2] = pData.copper;
        oreCollection[3] = pData.iron;
        oreCollection[4] = pData.gold;
        oreCollection[5] = pData.ruby;
        oreCollection[6] = pData.titanium;

        coin = pData.coin;
    }
    public void FeedbackLink()
    {
        Application.OpenURL("https://forms.gle/GCG8jeUtf5qtg1uUA");
    }
}
