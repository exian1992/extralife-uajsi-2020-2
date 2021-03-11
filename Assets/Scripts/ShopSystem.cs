using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShopSystem : MonoBehaviour
{
    GameObject manager, qManager, pManager, cManager;
    GameManager gManager;
    QuestManager questManager;
    PetManager petManager;
    CostumeManager costumeManager;

    //item collection
    public Text stoneValue;
    public Text coalValue;
    public Text bronzeValue;
    public Text ironValue;
    public Text eqLevel;
    public Text coinValue;

    //collection Variable
    public GameObject stone1, stone10, stone100, coal1, coal10, coal100, bronze1, bronze10, bronze100, iron1, iron10, iron100;

    //pet shop buttons
    public GameObject buyDoge, buyTick;
    Button buyDogeBtn, buyTickBtn;
    public GameObject dogeHave, tickHave;

    //costume shop buttons
    public GameObject buyLynn, buyBrook;
    Button buyLynnBtn, buyBrookBtn;
    public GameObject lynnHave, brookHave;

    //UI organizer
    public GameObject petScreen, costumeScreen;
    void Start()
    {
        manager = GameObject.Find("GameManager");
        gManager = manager.GetComponent<GameManager>();
        qManager = GameObject.Find("QuestManager");
        questManager = qManager.GetComponent<QuestManager>();
        pManager = GameObject.Find("PetManager");
        petManager = pManager.GetComponent<PetManager>();
        cManager = GameObject.Find("CostumeManager");
        costumeManager = cManager.GetComponent<CostumeManager>();

        buyDogeBtn = buyDoge.GetComponent<Button>();
        buyTickBtn = buyTick.GetComponent<Button>();

        buyLynnBtn = buyLynn.GetComponent<Button>();
        buyBrookBtn = buyBrook.GetComponent<Button>();
        RefreshText();
    }
    private void Update()
    {
        RefreshText();

        #region Button Manager
        //for selling ore
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

        #region For pet shop
        if (!petManager.allPetsList[0].purchaseStatus)
        {
            if (gManager.coin >= 50) buyDogeBtn.interactable = true;
            else buyDogeBtn.interactable = false;
        }
        else
        {
            dogeHave.SetActive(true);
            buyDoge.SetActive(false);
        }

        if (!petManager.allPetsList[1].purchaseStatus)
        {
            if (gManager.coin >= 100) buyTickBtn.interactable = true; 
            else buyTickBtn.interactable = false;
        }
        else
        {
            tickHave.SetActive(true);
            buyTick.SetActive(false);
        }
        #endregion

        #region For costume shop
        if (!costumeManager.allCostumesList[1].purchaseStatus)
        {
            if (gManager.coin >= 100) buyLynnBtn.interactable = true;
            else buyLynnBtn.interactable = false;
        }
        else
        {
            lynnHave.SetActive(true);
            buyLynn.SetActive(false);
        }

        if (!costumeManager.allCostumesList[2].purchaseStatus)
        {
            if (gManager.coin >= 200) buyBrookBtn.interactable = true;
            else buyBrookBtn.interactable = false;
        }
        else
        {
            brookHave.SetActive(true);
            buyBrook.SetActive(false);
        }
        #endregion
    }
    public void Back()
    {
        gManager.SaveAllProgress();
        Destroy(GameObject.Find("AllManager")); 
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
    #region Buy Stuff
    public void BuyDoge()
    {
        petManager.allPetsList[0].purchaseStatus = true;
        gManager.coin -= 50;
    }
    public void BuyTick()
    {
        petManager.allPetsList[1].purchaseStatus = true;
        gManager.coin -= 100;
    }
    public void BuyLynn()
    {
        costumeManager.allCostumesList[1].purchaseStatus = true;
        costumeManager.otherCostumeUnlocked = true;
        gManager.coin -= 100;
    }
    public void BuyBrook()
    {
        costumeManager.allCostumesList[2].purchaseStatus = true;
        costumeManager.otherCostumeUnlocked = true;
        gManager.coin -= 200;
    }
    #endregion

    #region Sell Value
    public void Stone1()
    {
        gManager.coin += 1;
        gManager.stone -= 1;
        //sell stone quest
        if (questManager.isThereQuest)
        {
            if (questManager.currentActiveQuest.questType == QuestType.SellStone)
            {
                questManager.currentActiveQuest.Increase(1);
            }
        }
    }
    public void Stone10()
    {
        gManager.coin += 10;
        gManager.stone -= 10;
        //sell stone quest
        if (questManager.isThereQuest)
        {
            if (questManager.currentActiveQuest.questType == QuestType.SellStone)
            {
                questManager.currentActiveQuest.Increase(10);
            }
        }
    }
    public void Stone100()
    {
        gManager.coin += 100;
        gManager.stone -= 100;
        //sell stone quest
        if (questManager.isThereQuest)
        {
            if (questManager.currentActiveQuest.questType == QuestType.SellStone)
            {
                questManager.currentActiveQuest.Increase(100);
            }
        }
    }
    public void Coal1()
    {
        gManager.coin += 5;
        gManager.coal -= 1;
    }
    public void Coal10()
    {
        gManager.coin += 50;
        gManager.coal -= 10;
    }
    public void Coal100()
    {
        gManager.coin += 500;
        gManager.coal -= 100;
    }
    public void Bronze1()
    {
        gManager.coin += 10;
        gManager.bronze -= 1;
    }
    public void Bronze10()
    {
        gManager.coin += 100;
        gManager.bronze -= 10;
    }
    public void Bronze100()
    {
        gManager.coin += 1000;
        gManager.bronze -= 100;
    }
    public void Iron1()
    {
        gManager.coin += 25;
        gManager.iron -= 1;
    }
    public void Iron10()
    {
        gManager.coin += 250;
        gManager.iron -= 10;
    }
    public void Iron100()
    {
        gManager.coin += 2500;
        gManager.iron -= 100;
    }
    #endregion
    public void PetScreen()
    {
        petScreen.SetActive(true);
        costumeScreen.SetActive(false);
    }
    public void CostumeScreen()
    {
        petScreen.SetActive(false);
        costumeScreen.SetActive(true);
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
}
