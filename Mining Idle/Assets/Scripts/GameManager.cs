using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject manager;
    public GameObject[] prefabs;

    public GameObject[] blocks;

    float currentOre = 1;
    public Text currentOreHealth;
    public Text currentOreName;

    //for maximum random rumber
    int max = 10;

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
    public float attackSpeed = 1;

    //checker
    public bool isItLoaded = false;
    void Start()
    {
        if (!isItLoaded)
        {
            LoadData();
            isItLoaded = true;
        }
        else
        {
            GameObject iDataObject = GameObject.Find("InventoryData");
            InventoryData iData = iDataObject.GetComponent<InventoryData>();

            stone = iData.stone;
            coal = iData.coal;
            bronze = iData.bronze;
            iron = iData.iron;

            eqLvl = iData.eqLvl;
            attackSpeed = iData.attSpd;
        }

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
                Debug.Log("Current ore Health = " + currentOre);
            }
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
        currentOreHealth.text = currentOre.ToString();

        stoneValue.text = stone.ToString();
        coalValue.text = coal.ToString();
        bronzeValue.text = bronze.ToString();
        ironValue.text = iron.ToString();
        eqLevel.text = eqLvl.ToString();
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
        int randomOre = Random.Range(0, max);

        //eq lvl checker
        if (eqLvl == 2) max = 15;
        if (eqLvl == 3) max = 19;
        if (eqLvl == 4) max = 20;

        //stone 10, coal 5, bronze 4, iron 1
        if (randomOre < ironChance)
        {
            Instantiate(prefabs[3], new Vector3(2.45f, 0f, 3f), Quaternion.Euler(0, 0, 90));
        }
        else if (randomOre < bronzeChance)
        {
            Instantiate(prefabs[2], new Vector3(2.45f, 0f, 3f), Quaternion.Euler(0, 0, 90));
        }
        else if (randomOre < coalChance)
        {
            Instantiate(prefabs[1], new Vector3(2.45f, 0f, 3f), Quaternion.Euler(0, 0, 90));
        }
        else if (randomOre < stoneChance)
        {
            Instantiate(prefabs[0], new Vector3(2.45f, 0f, 3f), Quaternion.Euler(0, 0, 90));
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
                }
                if (ore.GetName() == "Coal")
                {
                    coal++;
                }
                if (ore.GetName() == "Bronze")
                {
                    bronze++;
                }
                if (ore.GetName() == "Iron")
                {
                    iron++;
                }
            }
        }
    }
    #endregion
    
    public void GoToShop()
    {
        SaveSystem.SaveData(this);
        SceneManager.LoadScene("Shop");
        Destroy(GameObject.Find("InventoryData"));
        DontDestroyOnLoad(manager);
    }
    public float Damage()
    {
        return attackSpeed;
    }
    public void LoadData()
    {
        GameData data = SaveSystem.LoadData();
        stone = data.stone;
        coal = data.coal;
        bronze = data.bronze;
        iron = data.iron;

        eqLvl = data.eqLvl;
        attackSpeed = data.attSpd;
    }
    void OnApplicationQuit()
    {
        isItLoaded = false;
        SaveSystem.SaveData(this);
    }
}
