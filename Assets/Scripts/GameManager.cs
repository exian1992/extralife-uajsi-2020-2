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
    //public GameObject[] floatingOreIcon;
    GameData data;
    GameObject qManager, pManager, cManager;
    QuestManager questManager;
    PetManager petManager;
    CostumeManager costumeManager;

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
    public float defaultMiningPower = 1f;
    public float defaultMiningSpeed = 1f;
    public float trueMiningSpeed;
    public float trueMiningPower;

    //coin
    public int coin = 0;
    public Text coinValue;

    //checker
    public bool isItLoaded = false;

    //booster variable
    public bool speedUpToggle = false;
    public bool powerUpToggle = false;
    public float tempMiningSpeed;
    public float tempMiningPower;

    //etc
    public GameObject questPopUp;

    void Start()
    {
        qManager = GameObject.Find("QuestManager");
        questManager = qManager.GetComponent<QuestManager>();
        pManager = GameObject.Find("PetManager");
        petManager = pManager.GetComponent<PetManager>();
        cManager = GameObject.Find("CostumeManager");
        costumeManager = cManager.GetComponent<CostumeManager>();

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

        //check if equipped with SpeedUp buff (auto-clicker)
        #region Old Speed Formula
        /*float temp;
        if (petManager.petEquipped && costumeManager.costumeEquipped && costumeManager.currentActiveCostume.statusTypeCostume != StatusTypeCostume.None)
        {
            if (petManager.currentActivePet.statusTypePet == StatusTypePet.SpeedUp)
            {
                temp = defaultMiningSpeed - (defaultMiningSpeed * petManager.currentActivePet.statusValue / 100) - (defaultMiningSpeed * costumeManager.currentActiveCostume.statusValue / 100);
                Debug.Log("speed = " + temp + "f");
                InvokeRepeating("AttackOre", 1f, temp);
            }
            else
            {
                Debug.Log("speed = " + defaultMiningSpeed + "f");
                InvokeRepeating("AttackOre", 1f, defaultMiningSpeed);
            }
        }
        else if (petManager.petEquipped)
        {
            if (petManager.currentActivePet.statusTypePet == StatusTypePet.SpeedUp)
            {
                temp = defaultMiningSpeed - (defaultMiningSpeed * petManager.currentActivePet.statusValue / 100);
                Debug.Log("speed = " + temp + "f");
                InvokeRepeating("AttackOre", 1f, temp);
            }
            else
            {
                Debug.Log("speed = " + defaultMiningSpeed + "f");
                InvokeRepeating("AttackOre", 1f, defaultMiningSpeed);
            }
        }
        else if (costumeManager.costumeEquipped && costumeManager.currentActiveCostume.statusTypeCostume != StatusTypeCostume.None)
        {
            if (costumeManager.currentActiveCostume.statusTypeCostume == StatusTypeCostume.SpeedUp)
            {
                temp = defaultMiningSpeed - (defaultMiningSpeed * costumeManager.currentActiveCostume.statusValue / 100);
                Debug.Log("speed = " + temp + "f");
                InvokeRepeating("AttackOre", 1f, temp);
            }
            else
            {
                Debug.Log("speed = " + defaultMiningSpeed + "f");
                InvokeRepeating("AttackOre", 1f, defaultMiningSpeed);
            }
        }
        else
        {
            Debug.Log("speed = " + defaultMiningSpeed + "f");
            InvokeRepeating("AttackOre", 1f, defaultMiningSpeed);
        }*/
        #endregion

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
            //tap quest
            if (questManager.isThereQuest)
            {
                if (questManager.currentActiveQuest.questType == QuestType.Tap)
                {
                    questManager.currentActiveQuest.Increase(1);
                    questManager.QuestCompleteCheck();
                }
            }
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
        RefreshText();
    }
    void RefreshText()
    {
        if (SceneManager.GetActiveScene().name == "MainGameplay")
        {
            currentOreHealth.text = currentOre.ToString();

            stoneValue.text = stone.ToString();
            coalValue.text = coal.ToString();
            bronzeValue.text = bronze.ToString();
            ironValue.text = iron.ToString();
            eqLevel.text = eqLvl.ToString();

            coinValue.text = coin.ToString();
        }
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
                        if (questManager.currentActiveQuest.questType == QuestType.MineStone)
                        {
                            questManager.currentActiveQuest.Increase(1);
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

    #region Boosters
    public void InstantGain()
    {
        for (int i = 0; i < 1000; i++)
        {
            int randomGain = Random.Range(0, 20);
            //stone 10, coal 5, bronze 4, iron 1
            if (randomGain < ironChance)
            {
                iron++;
            }
            else if (randomGain < bronzeChance)//2
            {
                bronze++;
            }
            else if (randomGain < coalChance)//6
            {
                coal++;
            }
            else if (randomGain < stoneChance)//12
            {
                stone++;
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
        SaveAllProgress();
        SceneManager.LoadScene("Shop");
        DontDestroyOnLoad(allManager);
    }
    public void GoToPetSelection()
    {
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
        stone = data.stone;
        coal = data.coal;
        bronze = data.bronze;
        iron = data.iron;

        eqLvl = data.eqLvl;
        defaultMiningPower = data.attSpd;

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
        SaveAllProgress();
    }
    public void SaveAllProgress()
    {
        SaveSystem.SaveData(this);
        SaveSystem.SaveQuestState(questManager);
        SaveSystem.SavePetManager(petManager);
        SaveSystem.SaveCostumeManager(costumeManager);
    }
}
