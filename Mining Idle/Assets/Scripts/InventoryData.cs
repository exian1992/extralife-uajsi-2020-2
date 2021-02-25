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
    public int stone, coal, bronze, iron, eqLvl;
    public Text stoneValue;
    public Text coalValue;
    public Text bronzeValue;
    public Text ironValue;
    public Text eqLevel;
    void Start()
    {
        //grabbing previous data
        manager = GameObject.Find("GameManager");
        gManager = manager.GetComponent<GameManager>();
        stone = gManager.stone;
        coal = gManager.coal;
        bronze = gManager.bronze;
        iron = gManager.iron;
        eqLvl = gManager.eqLvl;

        stoneValue.text = stone.ToString();
        coalValue.text = coal.ToString();
        bronzeValue.text = bronze.ToString();
        ironValue.text = iron.ToString();
        eqLevel.text = eqLvl.ToString();
    }
    public void Back()
    {
        SceneManager.LoadScene("MainGameplay");
        DontDestroyOnLoad(this.gameObject);
    }
    public void UpgradeEquipment()
    {
        if (eqLvl < 5)
        {
            if (eqLvl == 1)
            {
                if (stone >= 10)
                {
                    stone -= 10;
                    eqLvl++;
                }
            }
            else if (eqLvl == 2)
            {
                if (stone >= 20 && coal >= 10)
                {
                    stone -= 20;
                    coal -= 10;
                    eqLvl++;
                }
            }
            else if (eqLvl == 3)
            {
                if (stone >= 30 && coal >= 20 && bronze >= 10)
                {
                    stone -= 30;
                    coal -= 20;
                    bronze -= 10;
                    eqLvl++;
                }
            }
            else if (eqLvl == 4)
            {
                if (stone >= 40 && coal >= 30 && bronze >= 20 && iron >= 10)
                {
                    stone -= 40;
                    coal -= 30;
                    bronze -= 20;
                    iron -= 10;
                    eqLvl++;
                }
            }
            else Debug.Log("not enough materials");
        }
        else Debug.Log("max lvl reached");
        RefreshText();
    }
    void RefreshText()
    {
        stoneValue.text = stone.ToString();
        coalValue.text = coal.ToString();
        bronzeValue.text = bronze.ToString();
        ironValue.text = iron.ToString();
        eqLevel.text = eqLvl.ToString();
    }
}
