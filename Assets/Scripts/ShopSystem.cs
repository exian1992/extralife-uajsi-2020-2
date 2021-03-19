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
        if (gManager.map1OreCollection[0] >= 1) stone1.SetActive(true); else stone1.SetActive(false);
        if (gManager.map1OreCollection[0] >= 10) stone10.SetActive(true); else stone10.SetActive(false);
        if (gManager.map1OreCollection[0] >= 100) stone100.SetActive(true); else stone100.SetActive(false);
        if (gManager.map1OreCollection[1] >= 1) coal1.SetActive(true); else coal1.SetActive(false);
        if (gManager.map1OreCollection[1] >= 10) coal10.SetActive(true); else coal10.SetActive(false);
        if (gManager.map1OreCollection[1] >= 100) coal100.SetActive(true); else coal100.SetActive(false);
        if (gManager.map1OreCollection[2] >= 1) bronze1.SetActive(true); else bronze1.SetActive(false);
        if (gManager.map1OreCollection[2] >= 10) bronze10.SetActive(true); else bronze10.SetActive(false);
        if (gManager.map1OreCollection[2] >= 100) bronze100.SetActive(true); else bronze100.SetActive(false);
        if (gManager.map1OreCollection[3] >= 1) iron1.SetActive(true); else iron1.SetActive(false);
        if (gManager.map1OreCollection[3] >= 10) iron10.SetActive(true); else iron10.SetActive(false);
        if (gManager.map1OreCollection[3] >= 100) iron100.SetActive(true); else iron100.SetActive(false);
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
                if (gManager.map1OreCollection[0] >= 10 && gManager.coin >= 100)
                {
                    gManager.map1OreCollection[0] -= 10;
                    gManager.map1OreChance[1] = 5;
                    gManager.defaultMiningPower += 0.3f;
                    gManager.eqLvl++;
                    gManager.coin -= 100;
                }
            }
            else if (gManager.eqLvl == 2)
            {
                if (gManager.map1OreCollection[0] >= 20 && gManager.map1OreCollection[1] >= 10 && gManager.coin >= 500)
                {
                    gManager.map1OreCollection[0] -= 20;
                    gManager.map1OreCollection[1] -= 10;
                    gManager.map1OreChance[1] = 6;
                    gManager.map1OreChance[2] = 2;
                    gManager.defaultMiningPower += 0.3f;
                    gManager.eqLvl++;
                    gManager.coin -= 500;
                }
            }
            else if (gManager.eqLvl == 3)
            {
                if (gManager.map1OreCollection[0] >= 30 && gManager.map1OreCollection[1] >= 20 && gManager.map1OreCollection[2] >= 10 && gManager.coin >= 5000)
                {
                    gManager.map1OreCollection[0] -= 30;
                    gManager.map1OreCollection[1] -= 20;
                    gManager.map1OreCollection[2] -= 10;
                    gManager.map1OreChance[1] = 7;
                    gManager.map1OreChance[2] = 3;
                    gManager.map1OreChance[3] = 1;
                    gManager.defaultMiningPower += 0.3f;
                    gManager.eqLvl++;
                    gManager.coin -= 5000;
                }
            }
            else if (gManager.eqLvl == 4)
            {
                if (gManager.map1OreCollection[0] >= 40 && gManager.map1OreCollection[1] >= 30 && gManager.map1OreCollection[2] >= 20 && gManager.map1OreCollection[3] >= 10 && gManager.coin >= 10000)
                {
                    gManager.map1OreCollection[0] -= 40;
                    gManager.map1OreCollection[1] -= 30;
                    gManager.map1OreCollection[2] -= 20;
                    gManager.map1OreCollection[3] -= 10;
                    gManager.defaultMiningPower += 0.3f;
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
        gManager.map1OreCollection[0] -= 1;
        #region Stone selling quest
        if (questManager.isThereQuest)
        {
            if (questManager.currentActiveQuest.questType == QuestType.SellStone)
            {
                questManager.currentActiveQuest.Increase(1);
            }
        }
        if (questManager.activeEQuest.questType == QuestType.SellStone)
        {
            questManager.activeEQuest.Increase(1);
        }
        if (questManager.activeIQuest.questType == QuestType.SellStone)
        {
            questManager.activeIQuest.Increase(1);
        }
        if (questManager.activeHQuest.questType == QuestType.SellStone)
        {
            questManager.activeHQuest.Increase(1);
        }
        #endregion
    }
    public void Stone10()
    {
        gManager.coin += 10;
        gManager.map1OreCollection[0] -= 10;
        #region Stone selling quest
        if (questManager.isThereQuest)
        {
            if (questManager.currentActiveQuest.questType == QuestType.SellStone)
            {
                questManager.currentActiveQuest.Increase(10);
            }
        }
        if (questManager.activeEQuest.questType == QuestType.SellStone)
        {
            questManager.activeEQuest.Increase(10);
        }
        if (questManager.activeIQuest.questType == QuestType.SellStone)
        {
            questManager.activeIQuest.Increase(10);
        }
        if (questManager.activeHQuest.questType == QuestType.SellStone)
        {
            questManager.activeHQuest.Increase(10);
        }
        #endregion
    }
    public void Stone100()
    {
        gManager.coin += 100;
        gManager.map1OreCollection[0] -= 100;
        #region Stone selling quest
        if (questManager.isThereQuest)
        {
            if (questManager.currentActiveQuest.questType == QuestType.SellStone)
            {
                questManager.currentActiveQuest.Increase(100);
            }
        }
        if (questManager.activeEQuest.questType == QuestType.SellStone)
        {
            questManager.activeEQuest.Increase(100);
        }
        if (questManager.activeIQuest.questType == QuestType.SellStone)
        {
            questManager.activeIQuest.Increase(100);
        }
        if (questManager.activeHQuest.questType == QuestType.SellStone)
        {
            questManager.activeHQuest.Increase(100);
        }
        #endregion
    }
    public void Coal1()
    {
        gManager.coin += 5;
        gManager.map1OreCollection[1] -= 1;
        #region Coal selling quest
        if (questManager.isThereQuest)
        {
            if (questManager.currentActiveQuest.questType == QuestType.SellCoal)
            {
                questManager.currentActiveQuest.Increase(1);
            }
        }
        if (questManager.activeEQuest.questType == QuestType.SellCoal)
        {
            questManager.activeEQuest.Increase(1);
        }
        if (questManager.activeIQuest.questType == QuestType.SellCoal)
        {
            questManager.activeIQuest.Increase(1);
        }
        if (questManager.activeHQuest.questType == QuestType.SellCoal)
        {
            questManager.activeHQuest.Increase(1);
        }
        #endregion
    }
    public void Coal10()
    {
        gManager.coin += 50;
        gManager.map1OreCollection[1] -= 10;
        #region Coal selling quest
        if (questManager.isThereQuest)
        {
            if (questManager.currentActiveQuest.questType == QuestType.SellCoal)
            {
                questManager.currentActiveQuest.Increase(10);
            }
        }
        if (questManager.activeEQuest.questType == QuestType.SellCoal)
        {
            questManager.activeEQuest.Increase(10);
        }
        if (questManager.activeIQuest.questType == QuestType.SellCoal)
        {
            questManager.activeIQuest.Increase(10);
        }
        if (questManager.activeHQuest.questType == QuestType.SellCoal)
        {
            questManager.activeHQuest.Increase(10);
        }
        #endregion
    }
    public void Coal100()
    {
        gManager.coin += 500;
        gManager.map1OreCollection[1] -= 100;
        #region Coal selling quest
        if (questManager.isThereQuest)
        {
            if (questManager.currentActiveQuest.questType == QuestType.SellCoal)
            {
                questManager.currentActiveQuest.Increase(100);
            }
        }
        if (questManager.activeEQuest.questType == QuestType.SellCoal)
        {
            questManager.activeEQuest.Increase(100);
        }
        if (questManager.activeIQuest.questType == QuestType.SellCoal)
        {
            questManager.activeIQuest.Increase(100);
        }
        if (questManager.activeHQuest.questType == QuestType.SellCoal)
        {
            questManager.activeHQuest.Increase(100);
        }
        #endregion
    }
    public void Bronze1()
    {
        gManager.coin += 10;
        gManager.map1OreCollection[2] -= 1;
        #region Bronze selling quest
        if (questManager.isThereQuest)
        {
            if (questManager.currentActiveQuest.questType == QuestType.SellBronze)
            {
                questManager.currentActiveQuest.Increase(1);
            }
        }
        if (questManager.activeEQuest.questType == QuestType.SellBronze)
        {
            questManager.activeEQuest.Increase(1);
        }
        if (questManager.activeIQuest.questType == QuestType.SellBronze)
        {
            questManager.activeIQuest.Increase(1);
        }
        if (questManager.activeHQuest.questType == QuestType.SellBronze)
        {
            questManager.activeHQuest.Increase(1);
        }
        #endregion
    }
    public void Bronze10()
    {
        gManager.coin += 100;
        gManager.map1OreCollection[2] -= 10;
        #region Bronze selling quest
        if (questManager.isThereQuest)
        {
            if (questManager.currentActiveQuest.questType == QuestType.SellBronze)
            {
                questManager.currentActiveQuest.Increase(10);
            }
        }
        if (questManager.activeEQuest.questType == QuestType.SellBronze)
        {
            questManager.activeEQuest.Increase(10);
        }
        if (questManager.activeIQuest.questType == QuestType.SellBronze)
        {
            questManager.activeIQuest.Increase(10);
        }
        if (questManager.activeHQuest.questType == QuestType.SellBronze)
        {
            questManager.activeHQuest.Increase(10);
        }
        #endregion
    }
    public void Bronze100()
    {
        gManager.coin += 1000;
        gManager.map1OreCollection[2] -= 100;
        #region Bronze selling quest
        if (questManager.isThereQuest)
        {
            if (questManager.currentActiveQuest.questType == QuestType.SellBronze)
            {
                questManager.currentActiveQuest.Increase(100);
            }
        }
        if (questManager.activeEQuest.questType == QuestType.SellBronze)
        {
            questManager.activeEQuest.Increase(100);
        }
        if (questManager.activeIQuest.questType == QuestType.SellBronze)
        {
            questManager.activeIQuest.Increase(100);
        }
        if (questManager.activeHQuest.questType == QuestType.SellBronze)
        {
            questManager.activeHQuest.Increase(100);
        }
        #endregion
    }
    public void Iron1()
    {
        gManager.coin += 25;
        gManager.map1OreCollection[3] -= 1;
        #region Iron selling quest
        if (questManager.isThereQuest)
        {
            if (questManager.currentActiveQuest.questType == QuestType.SellIron)
            {
                questManager.currentActiveQuest.Increase(1);
            }
        }
        if (questManager.activeEQuest.questType == QuestType.SellIron)
        {
            questManager.activeEQuest.Increase(1);
        }
        if (questManager.activeIQuest.questType == QuestType.SellIron)
        {
            questManager.activeIQuest.Increase(1);
        }
        if (questManager.activeHQuest.questType == QuestType.SellIron)
        {
            questManager.activeHQuest.Increase(1);
        }
        #endregion
    }
    public void Iron10()
    {
        gManager.coin += 250;
        gManager.map1OreCollection[3] -= 10;
        #region Iron selling quest
        if (questManager.isThereQuest)
        {
            if (questManager.currentActiveQuest.questType == QuestType.SellIron)
            {
                questManager.currentActiveQuest.Increase(10);
            }
        }
        if (questManager.activeEQuest.questType == QuestType.SellIron)
        {
            questManager.activeEQuest.Increase(10);
        }
        if (questManager.activeIQuest.questType == QuestType.SellIron)
        {
            questManager.activeIQuest.Increase(10);
        }
        if (questManager.activeHQuest.questType == QuestType.SellIron)
        {
            questManager.activeHQuest.Increase(10);
        }
        #endregion
    }
    public void Iron100()
    {
        gManager.coin += 2500;
        gManager.map1OreCollection[3] -= 100;
        #region Iron selling quest
        if (questManager.isThereQuest)
        {
            if (questManager.currentActiveQuest.questType == QuestType.SellIron)
            {
                questManager.currentActiveQuest.Increase(100);
            }
        }
        if (questManager.activeEQuest.questType == QuestType.SellIron)
        {
            questManager.activeEQuest.Increase(100);
        }
        if (questManager.activeIQuest.questType == QuestType.SellIron)
        {
            questManager.activeIQuest.Increase(100);
        }
        if (questManager.activeHQuest.questType == QuestType.SellIron)
        {
            questManager.activeHQuest.Increase(100);
        }
        #endregion
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
        stoneValue.text = gManager.map1OreCollection[0].ToString();
        coalValue.text = gManager.map1OreCollection[1].ToString();
        bronzeValue.text = gManager.map1OreCollection[2].ToString();
        ironValue.text = gManager.map1OreCollection[3].ToString();
        eqLevel.text = gManager.eqLvl.ToString();
        coinValue.text = gManager.coin.ToString();
    }
}
