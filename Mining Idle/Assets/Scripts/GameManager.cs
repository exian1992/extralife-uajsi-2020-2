using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public GameObject[] prefabs;

    public GameObject[] blocks;

    int currentOre = 1;
    public Text currentOreHealth;
    public Text currentOreName;

    //for maximum random rumber
    int max = 10;

    //ore info
    int stone = 0;
    int coal = 0;
    int bronze = 0;
    int iron = 0;
    public Text stoneValue;
    public Text coalValue;
    public Text bronzeValue;
    public Text ironValue;

    //equipment info
    int eqLvl = 1;
    public Text eqLevel;
    void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            Array.Clear(blocks, 0, blocks.Length);
            blocks = GameObject.FindGameObjectsWithTag("ore");
            TileMove();
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

            if (tr.transform.position.x <= 1f)
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

        //tile moving
        if (currentOre == 0)
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

            tr.transform.position = new Vector3(tr.transform.position.x - 0.7f, tr.transform.position.y + 0.18f, tr.transform.position.z);
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
        if (randomOre < 10)
        {
            Instantiate(prefabs[0], new Vector3(3.1f, -0.54f, 100), Quaternion.Euler(new Vector3(0, 0, 75)));
        }
        else if (randomOre < 15)
        {
            Instantiate(prefabs[1], new Vector3(3.1f, -0.54f, 100), Quaternion.Euler(new Vector3(0, 0, 75)));
        }
        else if (randomOre < 19)
        {
            Instantiate(prefabs[2], new Vector3(3.1f, -0.54f, 100), Quaternion.Euler(new Vector3(0, 0, 75)));
        }
        else if (randomOre < 20)
        {
            Instantiate(prefabs[3], new Vector3(3.1f, -0.54f, 100), Quaternion.Euler(new Vector3(0, 0, 75)));
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
    public void UpgradeEquipment()
    {
        if (eqLvl < 4)
        {
            eqLvl++;
        }
        else Debug.Log("max lvl reached");
    }
}
