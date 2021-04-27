using System.Collections;
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
    public GameObject idleManager;
    public GameObject[] prefabs;
    GameData data;
    
    //GameObject qManager, pManager, cManager;
    //QuestManager questManager;
    //PetManager petManager;
    //CostumeManager costumeManager;

    public GameObject currentBlock;

    float currentOreHealth;

    //ore info
    public int[] mapOreCollection;
    public Text[] mapOreValueText;

    //orechance
    public int[] map12OreChance;
    public int[] map3OreChance;

    #region Equipment Info
    public EquipmentInfo[] equipmentList;
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
    public bool isLoaded;

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

        //string saveData = "D:/SaveFile/data.uwansummoney";       

        
        string saveData = Application.persistentDataPath + "/data.uwansummoney";
        if (!File.Exists(saveData))
        {
            Debug.Log("opt 1");
            ChanceChecker();
            isLoaded = true;
            SaveAllProgress();
            data = SaveSystem.LoadData();
        }
        else
        {
            data = SaveSystem.LoadData();
            if (!data.isLoaded)
            {
                //questManager.LoadQuestState();
                Debug.Log("opt 2");
                LoadData();
                isLoaded = true;
                SaveAllProgress();
            }
            else if (data.isLoaded)
            {
                Debug.Log("opt 3");
                LoadData();
            }
            else Debug.Log("Welp, ur f*ck*d...");
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
            SceneManager.GetActiveScene().name == "DeepCave")
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
        isLoaded = data.isLoaded;
    }
    public void AttackOre()
    {
        if (SceneManager.GetActiveScene().name == "Waterfall" ||
            SceneManager.GetActiveScene().name == "Cave" ||
            SceneManager.GetActiveScene().name == "DeepCave")
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
            mapOreValueText[0].text = mapOreCollection[0].ToString();
        }
        else if (SceneManager.GetActiveScene().name == "Cave")
        {
            mapOreValueText[0].text = mapOreCollection[1].ToString();
            mapOreValueText[1].text = mapOreCollection[2].ToString();
            mapOreValueText[2].text = mapOreCollection[3].ToString();
        }
        else if (SceneManager.GetActiveScene().name == "DeepCave")
        {
            mapOreValueText[0].text = mapOreCollection[4].ToString();
            mapOreValueText[1].text = mapOreCollection[5].ToString();
            mapOreValueText[2].text = mapOreCollection[6].ToString();
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
    }
    void TileCheck()
    {
        Ore ore = currentBlock.GetComponent<Ore>();
        if (ore.GetName() == "Stone")
        {
            mapOreCollection[0]++;
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
            mapOreCollection[1]++;
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
            mapOreCollection[2]++;
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
            mapOreCollection[3]++;
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
            mapOreCollection[4]++;
        }
        if (ore.GetName() == "Ruby")
        {
            mapOreCollection[5]++;
        }
        if (ore.GetName() == "Titanium")
        {
            mapOreCollection[6]++;
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
                mapOreCollection[3]++;
            }
            else if (randomGain < map12OreChance[2])//copperChance)//2
            {
                mapOreCollection[2]++;
            }
            else if (randomGain < map12OreChance[1])//coalChance)//6
            {
                mapOreCollection[1]++;
            }
            else if (randomGain < map12OreChance[0])//stoneChance)//12
            {
                mapOreCollection[0]++;
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
            trueMiningPower = equipmentList[0].GetAttInfo();
        }
        else if (SceneManager.GetActiveScene().name == "Cave")
        {
            trueMiningPower = equipmentList[1].GetAttInfo();
        }
        else if (SceneManager.GetActiveScene().name == "DeepCave")
        {
            trueMiningPower = equipmentList[2].GetAttInfo();
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
        mapOreCollection[0] = data.stone;
        mapOreCollection[1] = data.coal;
        mapOreCollection[2] = data.copper;
        mapOreCollection[3] = data.iron;
        mapOreCollection[4] = data.gold;
        mapOreCollection[5] = data.ruby;
        mapOreCollection[6] = data.titanium;

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
        isLoaded = false;
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
        if (equipmentList[0].eqLvl <= 10)
        {
            map12OreChance[0] = 20;
        }
        #endregion
        #region Pickaxe/Cave
        if (equipmentList[1].eqLvl <= 10)
        {
            map12OreChance[1] = 20;
            map12OreChance[2] = 0;
            map12OreChance[3] = 0;
        }
        else if (equipmentList[1].eqLvl <= 20)
        {
            map12OreChance[1] = 20;
            map12OreChance[2] = 6;
            map12OreChance[3] = 0;
        }
        else if (equipmentList[1].eqLvl <= 30)
        {
            map12OreChance[1] = 20;
            map12OreChance[2] = 8;
            map12OreChance[3] = 2;
        }
        #endregion
        #region TNT/DeepCave
        if (equipmentList[2].eqLvl <= 10)
        {
            map3OreChance[0] = 20;
            map3OreChance[1] = 0;
            map3OreChance[2] = 0;
        }
        else if (equipmentList[2].eqLvl <= 20)
        {
            map3OreChance[0] = 20;
            map3OreChance[1] = 6;
            map3OreChance[2] = 0;
        }
        else if (equipmentList[2].eqLvl <= 30)
        {
            map3OreChance[0] = 20;
            map3OreChance[1] = 8;
            map3OreChance[2] = 2;
        }
        #endregion
    }
    public void FeedbackLink()
    {
        Application.OpenURL("https://forms.gle/GCG8jeUtf5qtg1uUA");
    }
}
