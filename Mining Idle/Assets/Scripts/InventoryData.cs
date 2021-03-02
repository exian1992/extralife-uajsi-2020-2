using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InventoryData : MonoBehaviour
{
    GameObject manager;
    GameManager gManager;

    //item collection
    public Text stoneValue;
    public Text coalValue;
    public Text bronzeValue;
    public Text ironValue;
    public Text eqLevel;
    public Text coinValue;

    //collection Variable
    public GameObject stone1, stone10, stone100, coal1, coal10, coal100, bronze1, bronze10, bronze100, iron1, iron10, iron100;

    void Start()
    {
        manager = GameObject.Find("GameManager");
        gManager = manager.GetComponent<GameManager>();
        RefreshText();
    }
    private void Update()
    {
        #region Button Manager
        if (gManager.stone >= 1) stone1.SetActive(true); else stone1.SetActive(false);
        if (gManager.stone >= 10) stone10.SetActive(true); else stone10.SetActive(false);
        if (gManager.stone >= 100) stone100.SetActive(true); else stone100.SetActive(false);
        if (gManager.coal >= 1) coal1.SetActive(true); else coal1.SetActive(false);
        if (gManager.coal >= 10) coal10.SetActive(true); else coal10.SetActive(false);
        if (gManager.coal >= 100) coal100.SetActive(true); else coal100.SetActive(false);
        if (gManager.bronze >= 1) bronze1.SetActive(true); else bronze1.SetActive(false);
        if (gManager.bronze >= 10) bronze10.SetActive(true); else bronze10.SetActive(false);
        if (gManager.bronze >= 100) bronze100.SetActive(true); else bronze100.SetActive(false);
        if (gManager.iron >= 1) iron1.SetActive(true); else iron1.SetActive(false);
        if (gManager.iron >= 10) iron10.SetActive(true); else iron10.SetActive(false);
        if (gManager.iron >= 100) iron100.SetActive(true); else iron100.SetActive(false);
        #endregion
    }
    public void Back()
    {
        SaveSystem.SaveData(gManager);
        Destroy(manager);
        SceneManager.LoadScene("MainGameplay");
    }
    public void UpgradeEquipment()
    {
        if (gManager.eqLvl < 5)
        {
            if (gManager.eqLvl == 1)
            {
                if (gManager.stone >= 10 && gManager.coin >= 100)
                {
                    gManager.stone -= 10;
                    gManager.coalChance = 5;
                    gManager.attSpd += 0.3f;
                    gManager.eqLvl++;
                    gManager.coin -= 100;
                }
            }
            else if (gManager.eqLvl == 2)
            {
                if (gManager.stone >= 20 && gManager.coal >= 10 && gManager.coin >= 500)
                {
                    gManager.stone -= 20;
                    gManager.coal -= 10;
                    gManager.coalChance = 6;
                    gManager.bronzeChance = 2;
                    gManager.attSpd += 0.3f;
                    gManager.eqLvl++;
                    gManager.coin -= 500;
                }
            }
            else if (gManager.eqLvl == 3)
            {
                if (gManager.stone >= 30 && gManager.coal >= 20 && gManager.bronze >= 10 && gManager.coin >= 5000)
                {
                    gManager.stone -= 30;
                    gManager.coal -= 20;
                    gManager.bronze -= 10;
                    gManager.coalChance = 7;
                    gManager.bronzeChance = 3;
                    gManager.ironChance = 1;
                    gManager.attSpd += 0.3f;
                    gManager.eqLvl++;
                    gManager.coin -= 5000;
                }
            }
            else if (gManager.eqLvl == 4)
            {
                if (gManager.stone >= 40 && gManager.coal >= 30 && gManager.bronze >= 20 && gManager.iron >= 10 && gManager.coin >= 10000)
                {
                    gManager.stone -= 40;
                    gManager.coal -= 30;
                    gManager.bronze -= 20;
                    gManager.iron -= 10;
                    gManager.attSpd += 0.3f;
                    gManager.eqLvl++;
                    gManager.coin -= 10000;
                }
            }
            else Debug.Log("not enough materials");
        }
        else Debug.Log("max lvl reached");
        RefreshText();
    }
    void RefreshText()
    {
        stoneValue.text = gManager.stone.ToString();
        coalValue.text = gManager.coal.ToString();
        bronzeValue.text = gManager.bronze.ToString();
        ironValue.text = gManager.iron.ToString();
        eqLevel.text = gManager.eqLvl.ToString();
        coinValue.text = gManager.coin.ToString();
    }

    #region Sell Value
    public void Stone1()
    {
        gManager.coin += 1;
        gManager.stone -= 1;
        RefreshText();
    }
    public void Stone10()
    {
        gManager.coin += 10;
        gManager.stone -= 10;
        RefreshText();
    }
    public void Stone100()
    {
        gManager.coin += 100;
        gManager.stone -= 100;
        RefreshText();
    }
    public void Coal1()
    {
        gManager.coin += 5;
        gManager.coal -= 1;
        RefreshText();
    }
    public void Coal10()
    {
        gManager.coin += 50;
        gManager.coal -= 10;
        RefreshText();
    }
    public void Coal100()
    {
        gManager.coin += 500;
        gManager.coal -= 100;
        RefreshText();
    }
    public void Bronze1()
    {
        gManager.coin += 10;
        gManager.bronze -= 1;
        RefreshText();
    }
    public void Bronze10()
    {
        gManager.coin += 100;
        gManager.bronze -= 10;
        RefreshText();
    }
    public void Bronze100()
    {
        gManager.coin += 1000;
        gManager.bronze -= 100;
        RefreshText();
    }
    public void Iron1()
    {
        gManager.coin += 25;
        gManager.iron -= 1;
        RefreshText();
    }
    public void Iron10()
    {
        gManager.coin += 250;
        gManager.iron -= 10;
        RefreshText();
    }
    public void Iron100()
    {
        gManager.coin += 2500;
        gManager.iron -= 100;
        RefreshText();
    }
    #endregion
}
