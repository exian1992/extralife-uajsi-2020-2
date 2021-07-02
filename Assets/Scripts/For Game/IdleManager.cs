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
    public GameObject[] prefabs;
    GameData data, pData;

    HireNPCManager npcManager;
    [SerializeField] QuestManager qManager;
    //PetManager petManager;
    //CostumeManager costumeManager;

    public GameObject currentBlock;

    float currentOreHealth;

    //ore info
    public int[] oreCollection;
    public TMPro.TextMeshProUGUI[] mapOreValueText;

    //orechance
    public int[] map2OreChance;
    public int[] map3OreChance;
    public int[] map4OreChance;
    public int[] map6OreChance;

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

        UpdateManager();
        

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
            SaveProgress();
            data = SaveSystem.LoadData();            
        }
        else
        {
            data = SaveSystem.LoadData();

            Debug.Log("opt 2");
            LoadData();
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
            SceneManager.GetActiveScene().name == "EarthMantle" ||
            SceneManager.GetActiveScene().name == "DwarfVillage" ||
            SceneManager.GetActiveScene().name == "EarthCore")
        {
            //ore update
            currentBlock = GameObject.FindGameObjectWithTag("ore");
            Ore ore = currentBlock.GetComponent<Ore>();

            currentOreHealth = ore.GetOreHealth();

            //touch input
            if (Input.GetMouseButtonDown(0))
            {
                #region Tap Quest
                if (qManager.activeEQuest.questType == QuestType.Tap)
                {
                    qManager.questProgress[0]++;
                }
                if (qManager.activeIQuest.questType == QuestType.Tap)
                {
                    qManager.questProgress[1]++;
                }
                if (qManager.activeHQuest.questType == QuestType.Tap)
                {
                    qManager.questProgress[2]++;
                }                
                #endregion
                AttackOre();
            }

            //tile replaced
            if (currentOreHealth <= 0)
            {
                TileCheck();
                Destroy(currentBlock);
                TileGenerator();
            }

            UpdateManager();
            RefreshText();
        }
    }
    public void AttackOre()
    {
        if (SceneManager.GetActiveScene().name == "Waterfall" ||
            SceneManager.GetActiveScene().name == "Cave" ||
            SceneManager.GetActiveScene().name == "DeepCave" ||
            SceneManager.GetActiveScene().name == "EarthMantle" ||
            SceneManager.GetActiveScene().name == "DwarfVillage" ||
            SceneManager.GetActiveScene().name == "EarthCore")
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
        else if (SceneManager.GetActiveScene().name == "DwarfVillage")
        {
            mapOreValueText[0].text = oreCollection[10].ToString();
        }
        else if (SceneManager.GetActiveScene().name == "EarthCore")
        {
            mapOreValueText[0].text = oreCollection[11].ToString();
            mapOreValueText[1].text = oreCollection[12].ToString();
            mapOreValueText[2].text = oreCollection[13].ToString();
        }
    }
    #region Tile Stuff
    void TileGenerator()
    {
        //random generator
        int randomOre = Random.Range(0, 20);

        //stone 10, coal 5, bronze 4, iron 1
        if(SceneManager.GetActiveScene().name == "Waterfall" || SceneManager.GetActiveScene().name == "DwarfVillage")
            Instantiate(prefabs[0], new Vector3(0f, 0f, 0f), Quaternion.Euler(0, 0, 90));

        if (SceneManager.GetActiveScene().name == "Cave")
        {
            for (int i = 2; i >= 0; i--)
            {
                if (randomOre < map2OreChance[i]) //ironChance
                {
                    Instantiate(prefabs[i], new Vector3(0f, 0f, 0f), Quaternion.Euler(0, 0, 90));
                    break;
                }
            }
        }
        if (SceneManager.GetActiveScene().name == "DeepCave")
        {
            for (int i = 2; i >= 0; i--)
            {
                if (randomOre < map3OreChance[i]) //ironChance
                {
                    Instantiate(prefabs[i], new Vector3(0f, 0f, 0f), Quaternion.Euler(0, 0, 90));
                    break;
                }
            }
        }
        if (SceneManager.GetActiveScene().name == "EarthMantle")
        {
            for (int i = 2; i >= 0; i--)
            {
                if (randomOre < map4OreChance[i]) //ironChance
                {
                    Instantiate(prefabs[i], new Vector3(0f, 0f, 0f), Quaternion.Euler(0, 0, 90));
                    break;
                }
            }
        }
        if (SceneManager.GetActiveScene().name == "EarthCore")
        {
            for (int i = 2; i >= 0; i--)
            {
                if (randomOre < map6OreChance[i]) //ironChance
                {
                    Instantiate(prefabs[i], new Vector3(0f, 0f, 0f), Quaternion.Euler(0, 0, 90));
                    break;
                }
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
            if (qManager.activeEQuest.questType == QuestType.MineStone)
            {
                qManager.questProgress[0]++;
            }
            if (qManager.activeIQuest.questType == QuestType.MineStone)
            {
                qManager.questProgress[1]++;
            }
            if (qManager.activeHQuest.questType == QuestType.MineStone)
            {
                qManager.questProgress[2]++;
            }
            #endregion
        }
        if (ore.GetName() == "Coal")
        {
            oreCollection[1]++;
            #region Coal mining quest
            if (qManager.activeEQuest.questType == QuestType.MineCoal)
            {
                qManager.questProgress[0]++;
            }
            if (qManager.activeIQuest.questType == QuestType.MineCoal)
            {
                qManager.questProgress[1]++;
            }
            if (qManager.activeHQuest.questType == QuestType.MineCoal)
            {
                qManager.questProgress[2]++;
            }
            #endregion
        }
        if (ore.GetName() == "Copper")
        {
            oreCollection[2]++;
            #region Copper mining quest
            if (qManager.activeEQuest.questType == QuestType.MineCopper)
            {
                qManager.questProgress[0]++;
            }
            if (qManager.activeIQuest.questType == QuestType.MineStone)
            {
                qManager.questProgress[1]++;
            }
            if (qManager.activeHQuest.questType == QuestType.MineStone)
            {
                qManager.questProgress[2]++;
            }
            #endregion
        }
        if (ore.GetName() == "Iron")
        {
            oreCollection[3]++;
            #region Iron mining quest
            if (qManager.activeEQuest.questType == QuestType.MineIron)
            {
                qManager.questProgress[0]++;
            }
            if (qManager.activeIQuest.questType == QuestType.MineIron)
            {
                qManager.questProgress[1]++;
            }
            if (qManager.activeHQuest.questType == QuestType.MineIron)
            {
                qManager.questProgress[2]++;
            }
            #endregion
        }
        if (ore.GetName() == "Gold")
        {
            oreCollection[4]++;
            #region Gold mining quest
            if (qManager.activeEQuest.questType == QuestType.MineGold)
            {
                qManager.questProgress[0]++;
            }
            if (qManager.activeIQuest.questType == QuestType.MineGold)
            {
                qManager.questProgress[1]++;
            }
            if (qManager.activeHQuest.questType == QuestType.MineGold)
            {
                qManager.questProgress[2]++;
            }
            #endregion
        }
        if (ore.GetName() == "Ruby")
        {
            oreCollection[5]++;
            #region Ruby mining quest
            if (qManager.activeEQuest.questType == QuestType.MineRuby)
            {
                qManager.questProgress[0]++;
            }
            if (qManager.activeIQuest.questType == QuestType.MineRuby)
            {
                qManager.questProgress[1]++;
            }
            if (qManager.activeHQuest.questType == QuestType.MineRuby)
            {
                qManager.questProgress[2]++;
            }
            #endregion
        }
        if (ore.GetName() == "Titanium")
        {
            oreCollection[6]++;
            #region Titanium mining quest
            if (qManager.activeEQuest.questType == QuestType.MineTitanium)
            {
                qManager.questProgress[0]++;
            }
            if (qManager.activeIQuest.questType == QuestType.MineTitanium)
            {
                qManager.questProgress[1]++;
            }
            if (qManager.activeHQuest.questType == QuestType.MineTitanium)
            {
                qManager.questProgress[2]++;
            }
            #endregion
        }
        if (ore.GetName() == "HPRuby")
        {
            oreCollection[7]++;
            #region HPRuby mining quest
            if (qManager.activeEQuest.questType == QuestType.MineHPRuby)
            {
                qManager.questProgress[0]++;
            }
            if (qManager.activeIQuest.questType == QuestType.MineHPRuby)
            {
                qManager.questProgress[1]++;
            }
            if (qManager.activeHQuest.questType == QuestType.MineHPRuby)
            {
                qManager.questProgress[2]++;
            }
            #endregion
        }
        if (ore.GetName() == "Sapphire")
        {
            oreCollection[8]++;
            #region Sapphire mining quest
            if (qManager.activeEQuest.questType == QuestType.MineSapphire)
            {
                qManager.questProgress[0]++;
            }
            if (qManager.activeIQuest.questType == QuestType.MineSapphire)
            {
                qManager.questProgress[1]++;
            }
            if (qManager.activeHQuest.questType == QuestType.MineSapphire)
            {
                qManager.questProgress[2]++;
            }
            #endregion
        }
        if (ore.GetName() == "Emerald")
        {
            oreCollection[9]++;
            #region Emerald mining quest
            if (qManager.activeEQuest.questType == QuestType.MineEmerald)
            {
                qManager.questProgress[0]++;
            }
            if (qManager.activeIQuest.questType == QuestType.MineEmerald)
            {
                qManager.questProgress[1]++;
            }
            if (qManager.activeHQuest.questType == QuestType.MineEmerald)
            {
                qManager.questProgress[2]++;
            }
            #endregion
        }
        if (ore.GetName() == "Dwarfnium")
        {
            oreCollection[10]++;
            #region Dwarfnium mining quest
            if (qManager.activeEQuest.questType == QuestType.MineDwarfnium)
            {
                qManager.questProgress[0]++;
            }
            if (qManager.activeIQuest.questType == QuestType.MineDwarfnium)
            {
                qManager.questProgress[1]++;
            }
            if (qManager.activeHQuest.questType == QuestType.MineDwarfnium)
            {
                qManager.questProgress[2]++;
            }
            #endregion
        }
        if (ore.GetName() == "HPSapphire")
        {
            oreCollection[11]++;
            #region HPSapphire mining quest
            if (qManager.activeEQuest.questType == QuestType.MineHPSapphire)
            {
                qManager.questProgress[0]++;
            }
            if (qManager.activeIQuest.questType == QuestType.MineHPSapphire)
            {
                qManager.questProgress[1]++;
            }
            if (qManager.activeHQuest.questType == QuestType.MineHPSapphire)
            {
                qManager.questProgress[2]++;
            }
            #endregion
        }
        if (ore.GetName() == "Diamond")
        {
            oreCollection[12]++;
            #region Diamond mining quest
            if (qManager.activeEQuest.questType == QuestType.MineDiamond)
            {
                qManager.questProgress[0]++;
            }
            if (qManager.activeIQuest.questType == QuestType.MineDiamond)
            {
                qManager.questProgress[1]++;
            }
            if (qManager.activeHQuest.questType == QuestType.MineDiamond)
            {
                qManager.questProgress[2]++;
            }
            #endregion
        }
        if (ore.GetName() == "Alexandrite")
        {
            oreCollection[13]++;
            #region Alexandrite mining quest
            if (qManager.activeEQuest.questType == QuestType.MineAlexandrite)
            {
                qManager.questProgress[0]++;
            }
            if (qManager.activeIQuest.questType == QuestType.MineAlexandrite)
            {
                qManager.questProgress[1]++;
            }
            if (qManager.activeHQuest.questType == QuestType.MineAlexandrite)
            {
                qManager.questProgress[2]++;
            }
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
            if (randomGain < map2OreChance[3])//ironChance)
            {
                oreCollection[3]++;
            }
            else if (randomGain < map2OreChance[2])//copperChance)//2
            {
                oreCollection[2]++;
            }
            else if (randomGain < map2OreChance[1])//coalChance)//6
            {
                oreCollection[1]++;
            }
            else if (randomGain < map2OreChance[0])//stoneChance)//12
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
        else if (SceneManager.GetActiveScene().name == "DwarfVillage")
        {
            trueMiningPower = eqAttack[4];
        }
        else if (SceneManager.GetActiveScene().name == "EarthCore")
        {
            trueMiningPower = eqAttack[5];
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
                eqAttack[i] = 1;
            }
            else
            {
                eqLevel[i] = data.eqLevel[i];
                if (data.eqAttack[i] != 1 + (0.15f * (eqLevel[i] - 1)))
                {
                    eqAttack[i] = 1 + (0.15f * (eqLevel[i] - 1));
                }
                else eqAttack[i] = data.eqAttack[i];
            }
                        
            eqBasePrice[i] = data.eqBasePrice[i];
            eqLatestPrice[i] = data.eqLatestPrice[i];
        }

        if (eqBasePrice[0] == 0)
        {
            eqBasePrice[0] = 100;
            eqLatestPrice[0] = 100;
        }
        if (eqBasePrice[1] == 0)
        {
            eqBasePrice[1] = 1000;
            eqLatestPrice[1] = 1000;
        }
        if (eqBasePrice[2] == 0)
        {
            eqBasePrice[2] = 10000;
            eqLatestPrice[2] = 10000;
        }
        if (eqBasePrice[3] == 0)
        {
            eqBasePrice[3] = 25000;
            eqLatestPrice[3] = 25000;
        }
        if (eqBasePrice[4] == 0)
        {
            eqBasePrice[4] = 75000;
            eqLatestPrice[4] = 75000;
        }
        if (eqBasePrice[5] == 0)
        {
            eqBasePrice[5] = 150000;
            eqLatestPrice[5] = 150000;
        }

        coin = data.coin;
    }
    public void SaveProgress()
    {
        SaveSystem.SaveData(this);
    }
    public void ChanceChecker()
    {
        #region Pickaxe/Cave
        if (eqLevel[1] <= 10)
        {
            map2OreChance[0] = 20;
            map2OreChance[1] = 0;
            map2OreChance[2] = 0;
        }
        else if (eqLevel[1] <= 20)
        {
            map2OreChance[0] = 20;
            map2OreChance[1] = 6;
            map2OreChance[2] = 0;
        }
        else if (eqLevel[1] <= 30)
        {
            map2OreChance[0] = 20;
            map2OreChance[1] = 8;
            map2OreChance[2] = 2;
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
        if (eqLevel[3] <= 10)
        {
            map4OreChance[0] = 20;
            map4OreChance[1] = 0;
            map4OreChance[2] = 0;
        }
        else if (eqLevel[3] <= 20)
        {
            map4OreChance[0] = 20;
            map4OreChance[1] = 6;
            map4OreChance[2] = 0;
        }
        else if (eqLevel[3] <= 30)
        {
            map4OreChance[0] = 20;
            map4OreChance[1] = 8;
            map4OreChance[2] = 2;
        }
        #endregion
        #region PlasmaDrill/EarthCore
        if (eqLevel[5] <= 10)
        {
            map6OreChance[0] = 20;
            map6OreChance[1] = 0;
            map6OreChance[2] = 0;
        }
        else if (eqLevel[5] <= 20)
        {
            map6OreChance[0] = 20;
            map6OreChance[1] = 6;
            map6OreChance[2] = 0;
        }
        else if (eqLevel[5] <= 30)
        {
            map6OreChance[0] = 20;
            map6OreChance[1] = 8;
            map6OreChance[2] = 2;
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
    void UpdateManager()
    {
        npcManager = GameObject.Find("HireNPCManager").GetComponent<HireNPCManager>();
        qManager = GameObject.Find("QuestManager").GetComponent<QuestManager>();
        //pManager = GameObject.Find("PetManager");
        //petManager = pManager.GetComponent<PetManager>();
        //cManager = GameObject.Find("CostumeManager");
        //costumeManager = cManager.GetComponent<CostumeManager>();
    }
}
