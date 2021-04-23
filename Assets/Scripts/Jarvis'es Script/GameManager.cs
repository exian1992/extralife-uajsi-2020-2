using System.Collections;
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
    public GameObject allManager;
    public GameObject manager;
    public GameObject[] prefabs;
    GameData data;
    GameObject qManager, pManager, cManager;
    QuestManager questManager;
    PetManager petManager;
    CostumeManager costumeManager;

    public GameObject currentBlock;

    float currentOreHealth;
    public Text currentOreHealthText;
    public Text currentOreName;

    //ore info
    public int[] map1OreCollection = new int[4];
    public Text[] map1OreValueText = new Text[4];
    //public Text stoneValue;
    //public Text coalValue;
    //public Text bronzeValue;
    //public Text ironValue;

    //orechance
    public int[] map1OreChance = new int[4];

    //equipment info
    public int eqLvl = 1;
    public Text eqLevel;
    public float defaultMiningPower = 1f;
    public float defaultMiningSpeed = 1f;
    public float tempMiningSpeed;
    public float tempMiningPower;
    public float trueMiningSpeed;
    public float trueMiningPower;

    //coin
    public int coin = 0;
    public Text coinValue;

    //booster variable
    public bool speedUpToggle = false;
    public bool powerUpToggle = false;

    //etc
    public GameObject questPopUp, dQuestPopUp;

    void Start()
    {
        qManager = GameObject.Find("QuestManager");
        questManager = qManager.GetComponent<QuestManager>();
        pManager = GameObject.Find("PetManager");
        petManager = pManager.GetComponent<PetManager>();
        cManager = GameObject.Find("CostumeManager");
        costumeManager = cManager.GetComponent<CostumeManager>();

        //string saveData = "D:/SaveFile/data.uwansummoney";
        string saveData = Application.persistentDataPath + "/data.uwansummoney";
        if (!File.Exists(saveData))
        {
            SaveSystem.SaveData(this);
        }
        else
        {
            questManager.LoadQuestState();
            LoadData();
        }

        ChanceChecker();

        //check if equipped with SpeedUp buff (auto-clicker)
        trueMiningSpeed = 1f;
        if (costumeManager.costumeEquipped && costumeManager.currentActiveCostume.statusTypeCostume == StatusTypeCostume.SpeedUp)
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
        }
        tempMiningSpeed = trueMiningSpeed;
        if (speedUpToggle)
        {
            trueMiningSpeed *= 50 / 100;
        }
        InvokeRepeating("AttackOre", 1f, trueMiningSpeed);

        if (currentBlock == null)
        {
            TileGenerator();
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "MainGameplay")
        {
            //ore remover + generator
            currentBlock = GameObject.FindGameObjectWithTag("ore");
            Ore ore = currentBlock.GetComponent<Ore>();

            Transform tr = currentBlock.transform;

            currentOreHealth = ore.GetOreHealth();
            currentOreHealthText.text = currentOreHealth.ToString();
            currentOreName.text = ore.GetName();

            //touch input
            if (Input.GetMouseButtonDown(0))
            {
                if (EventSystem.current.IsPointerOverGameObject()) return;
                #region Tap Quest
                if (questManager.activeEQuest.questType == QuestType.Tap)
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
                }
                #endregion
                AttackOre();
            }

            //tile moving
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
        currentBlock = GameObject.FindGameObjectWithTag("ore");
        Ore ore = currentBlock.GetComponent<Ore>();

        ore.OreDamage();
        currentOreHealth = ore.GetOreHealth();
    
        RefreshText();
    }
    void RefreshText()
    {
        if (SceneManager.GetActiveScene().name == "MainGameplay")
        {
            currentOreHealthText.text = currentOreHealth.ToString();

            map1OreValueText[0].text = map1OreCollection[0].ToString();
            map1OreValueText[1].text = map1OreCollection[1].ToString();
            map1OreValueText[2].text = map1OreCollection[2].ToString();
            map1OreValueText[3].text = map1OreCollection[3].ToString();
            eqLevel.text = eqLvl.ToString();

            coinValue.text = coin.ToString();
        }
    }   
    #region Tile Stuff
    void TileGenerator()
    {
        //random generator
        int randomOre = Random.Range(0, 20);

        //stone 10, coal 5, bronze 4, iron 1
        if (randomOre < map1OreChance[3])//ironChance)
        {
            Instantiate(prefabs[3], new Vector3(2.45f, -1.5f, 0f), Quaternion.Euler(0, 0, 90));
        }
        else if (randomOre < map1OreChance[2])//bronzeChance)
        {
            Instantiate(prefabs[2], new Vector3(2.45f, -1.5f, 0f), Quaternion.Euler(0, 0, 90));
        }
        else if (randomOre < map1OreChance[1])//coalChance)
        {
            Instantiate(prefabs[1], new Vector3(2.45f, -1.5f, 0f), Quaternion.Euler(0, 0, 90));
        }
        else if (randomOre < map1OreChance[0])//stoneChance)
        {
            Instantiate(prefabs[0], new Vector3(2.45f, -1.5f, 0f), Quaternion.Euler(0, 0, 90));
        }
    }
    void TileCheck()
    {
        Ore ore = currentBlock.GetComponent<Ore>();
        if (ore.GetName() == "Stone")
        {
            map1OreCollection[0]++;
            #region Stone mining quest
                if (questManager.isThereQuest)
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
                questManager.QuestCompleteCheck();
            #endregion
        }
        if (ore.GetName() == "Coal")
        {
            map1OreCollection[1]++;
            #region Coal mining quest
                if (questManager.isThereQuest)
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
                questManager.QuestCompleteCheck();
                #endregion
        }
        if (ore.GetName() == "Bronze")
        {
            map1OreCollection[2]++;
            #region Bronze mining quest
                if (questManager.isThereQuest)
                {
                    if (questManager.currentActiveQuest.questType == QuestType.MineBronze)
                    {
                        questManager.currentActiveQuest.Increase(1);
                    }
                }
                if (questManager.activeEQuest.questType == QuestType.MineBronze)
                {
                    questManager.activeEQuest.Increase(1);
                }
                if (questManager.activeIQuest.questType == QuestType.MineBronze)
                {
                    questManager.activeIQuest.Increase(1);
                }
                if (questManager.activeHQuest.questType == QuestType.MineBronze)
                {
                    questManager.activeHQuest.Increase(1);
                }
                questManager.QuestCompleteCheck();
                #endregion
        }
        if (ore.GetName() == "Iron")
        {
            map1OreCollection[3]++;
            #region Iron mining quest
                if (questManager.isThereQuest)
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
                questManager.QuestCompleteCheck();
                #endregion
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
            if (randomGain < map1OreChance[3])//ironChance)
            {
                map1OreCollection[3]++;
            }
            else if (randomGain < map1OreChance[2])//bronzeChance)//2
            {
                map1OreCollection[2]++;
            }
            else if (randomGain < map1OreChance[1])//coalChance)//6
            {
                map1OreCollection[1]++;
            }
            else if (randomGain < map1OreChance[0])//stoneChance)//12
            {
                map1OreCollection[0]++;
            }
        }
    }
    public void AutoClicker()
    {
        if (!speedUpToggle)
        {
            trueMiningSpeed = trueMiningSpeed * 50 / 100;
            speedUpToggle = true;
        }
        else
        {
            trueMiningSpeed = tempMiningSpeed;
            speedUpToggle = false;
        }
        CancelInvoke();
        InvokeRepeating("AttackOre", 1f, trueMiningSpeed);
    }
    public void PowerUp()
    {
        if (!powerUpToggle)
        {
            powerUpToggle = true;
        }
        else powerUpToggle = false;
    }
    #endregion
    public void GoToShop()
    {
        CancelInvoke();
        SaveAllProgress();
        SceneManager.LoadScene("Shop");
        DontDestroyOnLoad(allManager);
    }
    public void GoToPetSelection()
    {
        CancelInvoke();
        SaveAllProgress();
        SceneManager.LoadScene("PetCostume");
        DontDestroyOnLoad(allManager);
    }
    public float Damage()
    {
        #region Old Damage Formula
        /*float temp;
        if (petManager.petEquipped && costumeManager.costumeEquipped && costumeManager.currentActiveCostume.statusTypeCostume != StatusTypeCostume.None)
        {
            if (petManager.currentActivePet.statusTypePet == StatusTypePet.PowerUp && costumeManager.currentActiveCostume.statusTypeCostume == StatusTypeCostume.PowerUp)
            {
                temp = defaultMiningPower + (defaultMiningPower * petManager.currentActivePet.statusValue / 100) + (defaultMiningPower * costumeManager.currentActiveCostume.statusValue / 100);
                Debug.Log("attack = " + temp);
                return temp;
            }
            else
            {
                Debug.Log("attack = " + defaultMiningPower);
                return defaultMiningPower;
            }
        }
        else if (petManager.petEquipped)
        {
            if (petManager.currentActivePet.statusTypePet == StatusTypePet.PowerUp)
            {
                temp = defaultMiningPower + (defaultMiningPower * petManager.currentActivePet.statusValue / 100);
                Debug.Log("attack = " + temp);
                return temp;
            }
            else
            {
                Debug.Log("attack = " + defaultMiningPower);
                return defaultMiningPower;
            }
        }
        else if (costumeManager.costumeEquipped && costumeManager.currentActiveCostume.statusTypeCostume != StatusTypeCostume.None)
        {
            if (costumeManager.currentActiveCostume.statusTypeCostume == StatusTypeCostume.PowerUp)
            {
                temp = defaultMiningPower + (defaultMiningPower * costumeManager.currentActiveCostume.statusValue / 100);
                Debug.Log("attack = " + temp);
                return temp;
            }
            else
            {
                Debug.Log("attack = " + defaultMiningPower);
                return defaultMiningPower;
            }
        }
        else
        {
            Debug.Log("attack = " + defaultMiningPower);
            return defaultMiningPower;
        }*/
        #endregion
        trueMiningPower = defaultMiningPower;
        if (costumeManager.costumeEquipped && costumeManager.currentActiveCostume.statusTypeCostume == StatusTypeCostume.PowerUp)
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
        }
        return trueMiningPower;
    }
    public void LoadData()
    {
        data = SaveSystem.LoadData();
        map1OreCollection[0] = data.stone;
        map1OreCollection[1] = data.coal;
        map1OreCollection[2] = data.copper;
        map1OreCollection[3] = data.iron;

        eqLvl = data.eqLvl;
        defaultMiningPower = data.attSpd;

        map1OreChance[0] = data.map12OreChance[0];
        map1OreChance[1] = data.map12OreChance[1];
        map1OreChance[2] = data.map12OreChance[2];
        map1OreChance[3] = data.map12OreChance[3];

        coin = data.coin;
    }
    public void ShowQuest()
    {
        questPopUp.SetActive(true);
    }
    public void HideQuest()
    {
        questPopUp.SetActive(false);
        dQuestPopUp.SetActive(false);
    }
    public void SwitchDaily()
    {
        questPopUp.SetActive(false);
        dQuestPopUp.SetActive(true);
    }
    public void SwitchQuest()
    {
        questPopUp.SetActive(true);
        dQuestPopUp.SetActive(false);
    }
    void OnApplicationQuit()
    {
        SaveAllProgress();
    }
    public void SaveAllProgress()
    {
        SaveSystem.SaveData(this);
        SaveSystem.SaveQuestState(questManager);
        SaveSystem.SavePetManager(petManager);
        SaveSystem.SaveCostumeManager(costumeManager);
    }
    public void ChanceChecker()
    {
        if (eqLvl <= 10)
        {
            map1OreChance[0] = 20;
            map1OreChance[1] = 0;
            map1OreChance[2] = 0;
            map1OreChance[3] = 0;
        }
        else if (eqLvl <= 20)
        {
            map1OreChance[0] = 20;
            map1OreChance[1] = 5;
            map1OreChance[2] = 0;
            map1OreChance[3] = 0;
        }
        else if (eqLvl <= 30)
        {
            map1OreChance[0] = 20;
            map1OreChance[1] = 6;
            map1OreChance[2] = 2;
            map1OreChance[3] = 0;
        }
        else if (eqLvl <= 40)
        {
            map1OreChance[0] = 20;
            map1OreChance[1] = 7;
            map1OreChance[2] = 3;
            map1OreChance[3] = 1;
        }
    }
}
