using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShopSystem : MonoBehaviour
{
    IdleManager iManager;
    QuestManager qManager;
    PetManager petManager;
    CostumeManager costumeManager;
    MapManager mapManager;

    public TMPro.TextMeshProUGUI eqLevel;
    public Sprite[] materialImagesSprite;

    #region Merchant
    //pet shop buttons
    public GameObject buyDoge, buyTick;
    Button buyDogeBtn, buyTickBtn;
    public GameObject dogeHave, tickHave;

    //collection Variable
    public GameObject[] orePlacementObject;
    public Image[] oreDisplay;
    public TMPro.TextMeshProUGUI[] oreName;
    public TMPro.TextMeshProUGUI[] oreCollection;
    public int[] oreSell;
    public TMPro.TextMeshProUGUI[] oreSellValue;

    //costume shop buttons
    public GameObject buyLynn, buyBrook;
    Button buyLynnBtn, buyBrookBtn;
    public GameObject lynnHave, brookHave;

    //UI
    public TMPro.TMP_Dropdown merchantType;
    public GameObject[] merchantScreen;

    public Button sellButton, sellAllButton;

    //for click delay
    int i, j;
    #endregion

    #region Blacksmith
    //advance weapon    
    public GameObject[] upgradeType;
    public Image materialOnScreen;
    public TMPro.TextMeshProUGUI materialName;
    public TMPro.TextMeshProUGUI materialRequirement;
    public TMPro.TextMeshProUGUI[] coinRequirement;

    //UI
    public TMPro.TMP_Dropdown equipmentList;
    public GameObject[] coinText;
    public GameObject upgradeBtn;
    public GameObject coinLogo;

    public GameObject[] brookAnimations;
    #endregion

    //UI stuff
    public TMPro.TextMeshProUGUI sceneName;
    public GameObject mScreen, bScreen; 
    
    void Start()
    {
        iManager = GameObject.Find("IdleManager").GetComponent<IdleManager>();
        mapManager = GameObject.Find("MapManager").GetComponent<MapManager>();

        buyDogeBtn = buyDoge.GetComponent<Button>();
        buyTickBtn = buyTick.GetComponent<Button>();

        buyLynnBtn = buyLynn.GetComponent<Button>();
        buyBrookBtn = buyBrook.GetComponent<Button>();

        if (mapManager.toWhatShop == "merchant")
        {
            mScreen.SetActive(true);
            sceneName.text = "Merchant";
            UpdateOreMerchant();
        }
        else if (mapManager.toWhatShop == "blacksmith")
        {
            bScreen.SetActive(true);
            sceneName.text = "Blacksmith";
            AdvanceInfoRefresh();
        }
    }
    private void Update()
    {
        iManager = GameObject.Find("IdleManager").GetComponent<IdleManager>();
        qManager = GameObject.Find("QuestManager").GetComponent<QuestManager>();
        //petManager = GameObject.Find("PetManager").GetComponent<PetManager>();
        //costumeManager = GameObject.Find("CostumeManager").GetComponent<CostumeManager>();
        
        RefreshText();

        #region Ore Merchant UI Conditioning
        //normal sell
        if (oreSell[0] != 0 || oreSell[1] != 0 || oreSell[2] != 0 || oreSell[3] != 0 || oreSell[4] != 0 || oreSell[5] != 0 || oreSell[6] != 0 || oreSell[7] != 0 || oreSell[8] != 0 || oreSell[9] != 0 || oreSell[10] != 0 || oreSell[11] != 0 || oreSell[12] != 0 || oreSell[13] != 0)
        {
            sellButton.interactable = true;
        }
        else sellButton.interactable = false;

        //sell all
        if (iManager.oreCollection[0] != 0 || iManager.oreCollection[1] != 0 || iManager.oreCollection[2] != 0 || iManager.oreCollection[3] != 0 || iManager.oreCollection[4] != 0 || iManager.oreCollection[5] != 0 || iManager.oreCollection[6] != 0 || iManager.oreCollection[7] != 0 || iManager.oreCollection[8] != 0 || iManager.oreCollection[9] != 0 || iManager.oreCollection[10] != 0 || iManager.oreCollection[11] != 0 || iManager.oreCollection[12] != 0 || iManager.oreCollection[13] != 0)
        {
            sellAllButton.interactable = true;
        }
        else sellAllButton.interactable = false;
        #endregion

        #region For pet shop
        /*
        if (!petManager.allPetsList[0].purchaseStatus)
        {
            if (iManager.coin >= 50) buyDogeBtn.interactable = true;
            else buyDogeBtn.interactable = false;
        }
        else
        {
            dogeHave.SetActive(true);
            buyDoge.SetActive(false);
        }

        if (!petManager.allPetsList[1].purchaseStatus)
        {
            if (iManager.coin >= 100) buyTickBtn.interactable = true; 
            else buyTickBtn.interactable = false;
        }
        else
        {
            tickHave.SetActive(true);
            buyTick.SetActive(false);
        }
        */
        #endregion

        #region For costume shop
        /*
        if (!costumeManager.allCostumesList[1].purchaseStatus)
        {
            if (iManager.coin >= 100) buyLynnBtn.interactable = true;
            else buyLynnBtn.interactable = false;
        }
        else
        {
            lynnHave.SetActive(true);
            buyLynn.SetActive(false);
        }

        if (!costumeManager.allCostumesList[2].purchaseStatus)
        {
            if (iManager.coin >= 200) buyBrookBtn.interactable = true;
            else buyBrookBtn.interactable = false;
        }
        else
        {
            brookHave.SetActive(true);
            buyBrook.SetActive(false);
        }
        */
        #endregion
    }
    #region Buy Stuff
    public void BuyDoge()
    {
        petManager.allPetsList[0].purchaseStatus = true;
        iManager.coin -= 50;
    }
    public void BuyTick()
    {
        petManager.allPetsList[1].purchaseStatus = true;
        iManager.coin -= 100;
    }
    public void BuyLynn()
    {
        costumeManager.allCostumesList[1].purchaseStatus = true;
        costumeManager.otherCostumeUnlocked = true;
        iManager.coin -= 100;
    }
    public void BuyBrook()
    {
        costumeManager.allCostumesList[2].purchaseStatus = true;
        costumeManager.otherCostumeUnlocked = true;
        iManager.coin -= 200;
    }
    #endregion
    #region Sell Value
    public void ConfirmSell()
    {
        for (int temp = 0; temp < oreSell.Length; temp++)
        {
            if (oreSell[temp] > 0 && oreSell[temp] <= iManager.oreCollection[temp])
            {
                #region Pricing
                int price = 0;
                if (temp == 0) //stone
                {
                    price = 1;
                }
                else if (temp == 1) //coal
                {
                    price = 5;
                }
                else if (temp == 2) //copper
                {
                    price = 10;
                }
                else if (temp == 3) //iron
                {
                    price = 25;
                }
                else if (temp == 4) //gold
                {
                    price = 50;
                }
                else if (temp == 5) //ruby
                {
                    price = 75;
                }
                else if (temp == 6) //titanium
                {
                    price = 100;
                }
                else if (temp == 7) //hpruby
                {
                    price = 250;
                }
                else if (temp == 8) //sapphire
                {
                    price = 400;
                }
                else if (temp == 9) //emerald
                {
                    price = 600;
                }
                else if (temp == 10) //dwarfnium
                {
                    price = 500;
                }
                else if (temp == 11) //hpsapphire
                {
                    price = 1000;
                }
                else if (temp == 12) //diamond
                {
                    price = 1500;
                }
                else if (temp == 12) //alexandrite
                {
                    price = 3000;
                }
                #endregion
                iManager.coin += oreSell[temp] * price;
                iManager.oreCollection[temp] -= oreSell[temp];
                #region Stone selling quest
                if (temp == 0)
                {
                    if (qManager.activeEQuest.questType == QuestType.SellStone)
                    {
                        qManager.questProgress[0] += oreSell[temp];
                    }
                    if (qManager.activeIQuest.questType == QuestType.SellStone)
                    {
                        qManager.questProgress[1] += oreSell[temp];
                    }
                    if (qManager.activeHQuest.questType == QuestType.SellStone)
                    {
                        qManager.questProgress[2] += oreSell[temp];
                    }
                }
                #endregion
                #region Coal selling quest
                if (temp == 1)
                {
                    if (qManager.activeEQuest.questType == QuestType.SellCoal)
                    {
                        qManager.questProgress[0] += oreSell[temp];
                    }
                    if (qManager.activeIQuest.questType == QuestType.SellCoal)
                    {
                        qManager.questProgress[1] += oreSell[temp];
                    }
                    if (qManager.activeHQuest.questType == QuestType.SellCoal)
                    {
                        qManager.questProgress[2] += oreSell[temp];
                    }
                }
                #endregion
                #region Copper selling quest
                if (temp == 2)
                {
                    if (qManager.activeEQuest.questType == QuestType.SellCopper)
                    {
                        qManager.questProgress[0] += oreSell[temp];
                    }
                    if (qManager.activeIQuest.questType == QuestType.SellCopper)
                    {
                        qManager.questProgress[1] += oreSell[temp];
                    }
                    if (qManager.activeHQuest.questType == QuestType.SellCopper)
                    {
                        qManager.questProgress[2] += oreSell[temp];
                    }
                }
                #endregion
                #region Iron selling quest
                if (temp == 3)
                {
                    if (qManager.activeEQuest.questType == QuestType.SellIron)
                    {
                        qManager.questProgress[0] += oreSell[temp];
                    }
                    if (qManager.activeIQuest.questType == QuestType.SellIron)
                    {
                        qManager.questProgress[1] += oreSell[temp];
                    }
                    if (qManager.activeHQuest.questType == QuestType.SellIron)
                    {
                        qManager.questProgress[2] += oreSell[temp];
                    }
                }
                #endregion
                #region Gold selling quest
                if (temp == 4)
                {
                    if (qManager.activeEQuest.questType == QuestType.SellGold)
                    {
                        qManager.questProgress[0] += oreSell[temp];
                    }
                    if (qManager.activeIQuest.questType == QuestType.SellGold)
                    {
                        qManager.questProgress[1] += oreSell[temp];
                    }
                    if (qManager.activeHQuest.questType == QuestType.SellGold)
                    {
                        qManager.questProgress[2] += oreSell[temp];
                    }
                }
                #endregion
                #region Ruby selling quest
                if (temp == 5)
                {
                    if (qManager.activeEQuest.questType == QuestType.SellRuby)
                    {
                        qManager.questProgress[0] += oreSell[temp];
                    }
                    if (qManager.activeIQuest.questType == QuestType.SellRuby)
                    {
                        qManager.questProgress[1] += oreSell[temp];
                    }
                    if (qManager.activeHQuest.questType == QuestType.SellRuby)
                    {
                        qManager.questProgress[2] += oreSell[temp];
                    }
                }
                #endregion
                #region Titanium selling quest
                if (temp == 6)
                {
                    if (qManager.activeEQuest.questType == QuestType.SellTitanium)
                    {
                        qManager.questProgress[0] += oreSell[temp];
                    }
                    if (qManager.activeIQuest.questType == QuestType.SellTitanium)
                    {
                        qManager.questProgress[1] += oreSell[temp];
                    }
                    if (qManager.activeHQuest.questType == QuestType.SellTitanium)
                    {
                        qManager.questProgress[2] += oreSell[temp];
                    }
                }
                #endregion
                #region HPRuby selling quest
                if (temp == 7)
                {
                    if (qManager.activeEQuest.questType == QuestType.SellHPRuby)
                    {
                        qManager.questProgress[0] += oreSell[temp];
                    }
                    if (qManager.activeIQuest.questType == QuestType.SellHPRuby)
                    {
                        qManager.questProgress[1] += oreSell[temp];
                    }
                    if (qManager.activeHQuest.questType == QuestType.SellHPRuby)
                    {
                        qManager.questProgress[2] += oreSell[temp];
                    }
                }
                #endregion
                #region Sapphire selling quest
                if (temp == 8)
                {
                    if (qManager.activeEQuest.questType == QuestType.SellSapphire)
                    {
                        qManager.questProgress[0] += oreSell[temp];
                    }
                    if (qManager.activeIQuest.questType == QuestType.SellSapphire)
                    {
                        qManager.questProgress[1] += oreSell[temp];
                    }
                    if (qManager.activeHQuest.questType == QuestType.SellSapphire)
                    {
                        qManager.questProgress[2] += oreSell[temp];
                    }
                }
                #endregion
                #region Emerald selling quest
                if (temp == 9)
                {
                    if (qManager.activeEQuest.questType == QuestType.SellEmerald)
                    {
                        qManager.questProgress[0] += oreSell[temp];
                    }
                    if (qManager.activeIQuest.questType == QuestType.SellEmerald)
                    {
                        qManager.questProgress[1] += oreSell[temp];
                    }
                    if (qManager.activeHQuest.questType == QuestType.SellEmerald)
                    {
                        qManager.questProgress[2] += oreSell[temp];
                    }
                }                
                #endregion
                #region Dwarfnium selling quest
                if (temp == 10)
                {
                    if (qManager.activeEQuest.questType == QuestType.SellDwarfnium)
                    {
                        qManager.questProgress[0] += oreSell[temp];
                    }
                    if (qManager.activeIQuest.questType == QuestType.SellDwarfnium)
                    {
                        qManager.questProgress[1] += oreSell[temp];
                    }
                    if (qManager.activeHQuest.questType == QuestType.SellDwarfnium)
                    {
                        qManager.questProgress[2] += oreSell[temp];
                    }
                }                
                #endregion
                #region HPSapphire selling quest
                if (temp == 11)
                {
                    if (qManager.activeEQuest.questType == QuestType.SellHPSapphire)
                    {
                        qManager.questProgress[0] += oreSell[temp];
                    }
                    if (qManager.activeIQuest.questType == QuestType.SellHPSapphire)
                    {
                        qManager.questProgress[1] += oreSell[temp];
                    }
                    if (qManager.activeHQuest.questType == QuestType.SellHPSapphire)
                    {
                        qManager.questProgress[2] += oreSell[temp];
                    }
                }                
                #endregion
                #region Diamond selling quest
                if (temp == 12)
                {
                    if (qManager.activeEQuest.questType == QuestType.SellDiamond)
                    {
                        qManager.questProgress[0] += oreSell[temp];
                    }
                    if (qManager.activeIQuest.questType == QuestType.SellDiamond)
                    {
                        qManager.questProgress[1] += oreSell[temp];
                    }
                    if (qManager.activeHQuest.questType == QuestType.SellDiamond)
                    {
                        qManager.questProgress[2] += oreSell[temp];
                    }
                }                
                #endregion
                #region Alexandrite selling quest
                if (temp == 13)
                {
                    if (qManager.activeEQuest.questType == QuestType.SellAlexandrite)
                    {
                        qManager.questProgress[0] += oreSell[temp];
                    }
                    if (qManager.activeIQuest.questType == QuestType.SellAlexandrite)
                    {
                        qManager.questProgress[1] += oreSell[temp];
                    }
                    if (qManager.activeHQuest.questType == QuestType.SellAlexandrite)
                    {
                        qManager.questProgress[2] += oreSell[temp];
                    }
                }                
                #endregion

                oreSell[temp] = 0;                
            }            
        }
        iManager.SaveProgress();
        qManager.SaveProgress();
    }
    public void SellAllOre()
    {
        for (int temp = 0; temp < oreSell.Length; temp++)
        {
            #region Pricing
            int price = 0;
            if (temp == 0) //stone
            {
                price = 1;
            }
            else if (temp == 1) //coal
            {
                price = 5;
            }
            else if (temp == 2) //copper
            {
                price = 10;
            }
            else if (temp == 3) //iron
            {
                price = 25;
            }
            else if (temp == 4) //gold
            {
                price = 50;
            }
            else if (temp == 5) //ruby
            {
                price = 75;
            }
            else if (temp == 6) //titanium
            {
                price = 100;
            }
            else if (temp == 7) //hpruby
            {
                price = 250;
            }
            else if (temp == 8) //sapphire
            {
                price = 400;
            }
            else if (temp == 9) //emerald
            {
                price = 600;
            }
            else if (temp == 10) //dwarfnium
            {
                price = 500;
            }
            else if (temp == 11) //hpsapphire
            {
                price = 1000;
            }
            else if (temp == 12) //diamond
            {
                price = 1500;
            }
            else if (temp == 12) //alexandrite
            {
                price = 3000;
            }
            #endregion
            iManager.coin += iManager.oreCollection[temp] * price;
            
            #region Stone selling quest
            if (temp == 0)
            {
                if (qManager.activeEQuest.questType == QuestType.SellStone)
                {
                    qManager.questProgress[0] += iManager.oreCollection[temp];
                }
                if (qManager.activeIQuest.questType == QuestType.SellStone)
                {
                    qManager.questProgress[1] += iManager.oreCollection[temp];
                }
                if (qManager.activeHQuest.questType == QuestType.SellStone)
                {
                    qManager.questProgress[2] += iManager.oreCollection[temp];
                }
            }
            #endregion
            #region Coal selling quest
            if (temp == 1)
            {
                if (qManager.activeEQuest.questType == QuestType.SellCoal)
                {
                    qManager.questProgress[0] += iManager.oreCollection[temp];
                }
                if (qManager.activeIQuest.questType == QuestType.SellCoal)
                {
                    qManager.questProgress[1] += iManager.oreCollection[temp];
                }
                if (qManager.activeHQuest.questType == QuestType.SellCoal)
                {
                    qManager.questProgress[2] += iManager.oreCollection[temp];
                }
            }
            #endregion
            #region Copper selling quest
            if (temp == 2)
            {
                if (qManager.activeEQuest.questType == QuestType.SellCopper)
                {
                    qManager.questProgress[0] += iManager.oreCollection[temp];
                }
                if (qManager.activeIQuest.questType == QuestType.SellCopper)
                {
                    qManager.questProgress[1] += iManager.oreCollection[temp];
                }
                if (qManager.activeHQuest.questType == QuestType.SellCopper)
                {
                    qManager.questProgress[2] += iManager.oreCollection[temp];
                }
            }
            #endregion
            #region Iron selling quest
            if (temp == 3)
            {
                if (qManager.activeEQuest.questType == QuestType.SellIron)
                {
                    qManager.questProgress[0] += iManager.oreCollection[temp];
                }
                if (qManager.activeIQuest.questType == QuestType.SellIron)
                {
                    qManager.questProgress[1] += iManager.oreCollection[temp];
                }
                if (qManager.activeHQuest.questType == QuestType.SellIron)
                {
                    qManager.questProgress[2] += iManager.oreCollection[temp];
                }
            }
            #endregion
            #region Gold selling quest
            if (temp == 4)
            {
                if (qManager.activeEQuest.questType == QuestType.SellGold)
                {
                    qManager.questProgress[0] += iManager.oreCollection[temp];
                }
                if (qManager.activeIQuest.questType == QuestType.SellGold)
                {
                    qManager.questProgress[1] += iManager.oreCollection[temp];
                }
                if (qManager.activeHQuest.questType == QuestType.SellGold)
                {
                    qManager.questProgress[2] += iManager.oreCollection[temp];
                }
            }
            #endregion
            #region Ruby selling quest
            if (temp == 5)
            {
                if (qManager.activeEQuest.questType == QuestType.SellRuby)
                {
                    qManager.questProgress[0] += iManager.oreCollection[temp];
                }
                if (qManager.activeIQuest.questType == QuestType.SellRuby)
                {
                    qManager.questProgress[1] += iManager.oreCollection[temp];
                }
                if (qManager.activeHQuest.questType == QuestType.SellRuby)
                {
                    qManager.questProgress[2] += iManager.oreCollection[temp];
                }
            }
            #endregion
            #region Titanium selling quest
            if (temp == 6)
            {
                if (qManager.activeEQuest.questType == QuestType.SellTitanium)
                {
                    qManager.questProgress[0] += iManager.oreCollection[temp];
                }
                if (qManager.activeIQuest.questType == QuestType.SellTitanium)
                {
                    qManager.questProgress[1] += iManager.oreCollection[temp];
                }
                if (qManager.activeHQuest.questType == QuestType.SellTitanium)
                {
                    qManager.questProgress[2] += iManager.oreCollection[temp];
                }
            }
            #endregion
            #region HPRuby selling quest
            if (temp == 7)
            {
                if (qManager.activeEQuest.questType == QuestType.SellHPRuby)
                {
                    qManager.questProgress[0] += iManager.oreCollection[temp];
                }
                if (qManager.activeIQuest.questType == QuestType.SellHPRuby)
                {
                    qManager.questProgress[1] += iManager.oreCollection[temp];
                }
                if (qManager.activeHQuest.questType == QuestType.SellHPRuby)
                {
                    qManager.questProgress[2] += iManager.oreCollection[temp];
                }
            }
            #endregion
            #region Sapphire selling quest
            if (temp == 8)
            {
                if (qManager.activeEQuest.questType == QuestType.SellSapphire)
                {
                    qManager.questProgress[0] += iManager.oreCollection[temp];
                }
                if (qManager.activeIQuest.questType == QuestType.SellSapphire)
                {
                    qManager.questProgress[1] += iManager.oreCollection[temp];
                }
                if (qManager.activeHQuest.questType == QuestType.SellSapphire)
                {
                    qManager.questProgress[2] += iManager.oreCollection[temp];
                }
            }
            #endregion
            #region Emerald selling quest
            if (temp == 9)
            {
                if (qManager.activeEQuest.questType == QuestType.SellEmerald)
                {
                    qManager.questProgress[0] += iManager.oreCollection[temp];
                }
                if (qManager.activeIQuest.questType == QuestType.SellEmerald)
                {
                    qManager.questProgress[1] += iManager.oreCollection[temp];
                }
                if (qManager.activeHQuest.questType == QuestType.SellEmerald)
                {
                    qManager.questProgress[2] += iManager.oreCollection[temp];
                }
            }
            #endregion
            #region Dwarfnium selling quest
            if (temp == 10)
            {
                if (qManager.activeEQuest.questType == QuestType.SellDwarfnium)
                {
                    qManager.questProgress[0] += oreSell[temp];
                }
                if (qManager.activeIQuest.questType == QuestType.SellDwarfnium)
                {
                    qManager.questProgress[1] += oreSell[temp];
                }
                if (qManager.activeHQuest.questType == QuestType.SellDwarfnium)
                {
                    qManager.questProgress[2] += oreSell[temp];
                }
            }
            #endregion
            #region HPSapphire selling quest
            if (temp == 11)
            {
                if (qManager.activeEQuest.questType == QuestType.SellHPSapphire)
                {
                    qManager.questProgress[0] += oreSell[temp];
                }
                if (qManager.activeIQuest.questType == QuestType.SellHPSapphire)
                {
                    qManager.questProgress[1] += oreSell[temp];
                }
                if (qManager.activeHQuest.questType == QuestType.SellHPSapphire)
                {
                    qManager.questProgress[2] += oreSell[temp];
                }
            }
            #endregion
            #region Diamond selling quest
            if (temp == 12)
            {
                if (qManager.activeEQuest.questType == QuestType.SellDiamond)
                {
                    qManager.questProgress[0] += oreSell[temp];
                }
                if (qManager.activeIQuest.questType == QuestType.SellDiamond)
                {
                    qManager.questProgress[1] += oreSell[temp];
                }
                if (qManager.activeHQuest.questType == QuestType.SellDiamond)
                {
                    qManager.questProgress[2] += oreSell[temp];
                }
            }
            #endregion
            #region Alexandrite selling quest
            if (temp == 13)
            {
                if (qManager.activeEQuest.questType == QuestType.SellAlexandrite)
                {
                    qManager.questProgress[0] += oreSell[temp];
                }
                if (qManager.activeIQuest.questType == QuestType.SellAlexandrite)
                {
                    qManager.questProgress[1] += oreSell[temp];
                }
                if (qManager.activeHQuest.questType == QuestType.SellAlexandrite)
                {
                    qManager.questProgress[2] += oreSell[temp];
                }
            }
            #endregion

            iManager.oreCollection[temp] = 0;
            oreSell[temp] = 0;            
        }
        iManager.SaveProgress();
        qManager.SaveProgress();
    }
    #region OreInc
    Coroutine oreInc = null, oreDec = null;
    public void OreIncDown(int oreNumber)
    {
        oreInc = StartCoroutine(OreIncDelay(oreNumber));
    }
    public void OreIncUp()
    {
        StopCoroutine(oreInc);
        i = 0;
    }
    IEnumerator OreIncDelay(int oreNumber)
    {
        for (i = 0; i > -1; i++)
        {
            if (oreSell[oreNumber] >= iManager.oreCollection[oreNumber]) oreSell[oreNumber] = iManager.oreCollection[oreNumber];
            else if (i < 3) oreSell[oreNumber] += 1;
            else if ((iManager.oreCollection[oreNumber] - oreSell[oreNumber]) < 10) oreSell[oreNumber] = iManager.oreCollection[oreNumber];
            else if (i < 5) oreSell[oreNumber] += 10;
            else if ((iManager.oreCollection[oreNumber] - oreSell[oreNumber]) < 100) oreSell[oreNumber] = iManager.oreCollection[oreNumber];
            else if (i < 8) oreSell[oreNumber] += 100;
            else if ((iManager.oreCollection[oreNumber] - oreSell[oreNumber]) < 1000) oreSell[oreNumber] = iManager.oreCollection[oreNumber];
            else oreSell[oreNumber] += 1000;
            yield return new WaitForSeconds(0.2f);
        }
    }
    #endregion
    #region OreDec
    public void OreDecDown(int oreNumber)
    {
        oreDec = StartCoroutine(OreDecDelay(oreNumber));
    }
    public void OreDecUp()
    {
        StopCoroutine(oreDec);
        j = 0;
    }
    IEnumerator OreDecDelay(int oreNumber)
    {
        for (j = 0; j > -1; j++)
        {
            if (oreSell[oreNumber] <= 0) oreSell[oreNumber] = 0;
            else if (j < 3) oreSell[oreNumber] -= 1;
            else if (oreSell[oreNumber] < 10) oreSell[oreNumber] = 0;
            else if (j < 5) oreSell[oreNumber] -= 10;
            else if (oreSell[oreNumber] < 100) oreSell[oreNumber] = 0;
            else if (j < 8) oreSell[oreNumber] -= 100;
            else if (oreSell[oreNumber] < 1000) oreSell[oreNumber] = 0;
            else oreSell[oreNumber] -= 1000;
            yield return new WaitForSeconds(0.2f);            
        }
    }
    #endregion
    #endregion
    public void ConfirmAdvance()
    {
        bool enough = false;

        if (iManager.coin >= iManager.eqLatestPrice[equipmentList.value])
        {
            if (iManager.eqLevel[equipmentList.value] != 10 && iManager.eqLevel[equipmentList.value] != 20 && iManager.eqLevel[equipmentList.value] != 29)
            {
                enough = true;
            }
            #region Enough ore checking for advance
            else if (equipmentList.value == 1)
            {
                if (iManager.oreCollection[1] >= 1000 && iManager.eqLevel[1] == 10)
                {
                    enough = true;
                    iManager.oreCollection[1] -= 1000;
                }
                else if (iManager.oreCollection[2] >= 1000 && iManager.eqLevel[1] == 20)
                {
                    enough = true;
                    iManager.oreCollection[2] -= 1000;
                }
                else if (iManager.oreCollection[3] >= 1000 && iManager.eqLevel[1] == 29)
                {
                    enough = true;
                    iManager.oreCollection[3] -= 1000;
                }
            }
            else if (equipmentList.value == 2)
            {
                if (iManager.oreCollection[4] >= 1000 && iManager.eqLevel[2] == 10)
                {
                    enough = true;
                    iManager.oreCollection[4] -= 1000;
                }
                else if (iManager.oreCollection[5] >= 1000 && iManager.eqLevel[2] == 20)
                {
                    enough = true;
                    iManager.oreCollection[5] -= 1000;
                }
                else if (iManager.oreCollection[6] >= 1000 && iManager.eqLevel[2] == 29)
                {
                    enough = true;
                    iManager.oreCollection[6] -= 1000;
                }
            }
            else if (equipmentList.value == 3)
            {
                if (iManager.oreCollection[7] >= 1000 && iManager.eqLevel[3] == 10)
                {
                    enough = true;
                    iManager.oreCollection[7] -= 1000;
                }
                else if (iManager.oreCollection[8] >= 1000 && iManager.eqLevel[3] == 20)
                {
                    enough = true;
                    iManager.oreCollection[8] -= 1000;
                }
                else if (iManager.oreCollection[9] >= 1000 && iManager.eqLevel[3] == 29)
                {
                    enough = true;
                    iManager.oreCollection[9] -= 1000;
                }
            }
            else if (equipmentList.value == 5)
            {
                if (iManager.oreCollection[11] >= 1000 && iManager.eqLevel[5] == 10)
                {
                    enough = true;
                    iManager.oreCollection[11] -= 1000;
                }
                else if (iManager.oreCollection[12] >= 1000 && iManager.eqLevel[5] == 20)
                {
                    enough = true;
                    iManager.oreCollection[12] -= 1000;
                }
                else if (iManager.oreCollection[13] >= 1000 && iManager.eqLevel[5] == 29)
                {
                    enough = true;
                    iManager.oreCollection[13] -= 1000;
                }
            }
            #endregion
        }

        if (enough)
        {
            StartCoroutine("BrookAnim");

            //Equipment value go up (level, attack power)
            iManager.eqLevel[equipmentList.value]++;
            iManager.eqAttack[equipmentList.value] += 0.15f;
            iManager.coin -= iManager.eqLatestPrice[equipmentList.value];

            //Equipment pricing
            if (enough && (iManager.eqLevel[equipmentList.value] != 10 || iManager.eqLevel[equipmentList.value] != 20 || iManager.eqLevel[equipmentList.value] != 29))
            {
                if (iManager.eqLevel[equipmentList.value] == 11 || iManager.eqLevel[equipmentList.value] == 21 || iManager.eqLevel[equipmentList.value] == 29)
                {
                    iManager.eqLatestPrice[equipmentList.value] += (iManager.eqLatestPrice[equipmentList.value] * 50 / 100);
                }
                else iManager.eqLatestPrice[equipmentList.value] += (iManager.eqBasePrice[equipmentList.value] * 50 / 100);
            }
        }
        else
        {
            Debug.Log("Not enough material(s)/coin!");
        }                
    }
    IEnumerator BrookAnim()
    {
        brookAnimations[0].SetActive(true);
        yield return new WaitForSeconds(2f);
        brookAnimations[1].SetActive(true);
        brookAnimations[0].SetActive(false);
        yield return new WaitForSeconds(1f);
        brookAnimations[1].SetActive(false);

        AdvanceInfoRefresh();
    }
    public void AdvanceInfoRefresh()
    {
        coinRequirement[0].text = iManager.coin + " / " + iManager.eqLatestPrice[equipmentList.value].ToString();
        coinRequirement[1].text = iManager.coin + " / " + iManager.eqLatestPrice[equipmentList.value].ToString();

        #region Equipment Check
        int a = 0, b = 0, c = 0;
        if (equipmentList.value == 0) //gloves
        {
            a = 0; b = 0; c = 0;
        }
        else if (equipmentList.value == 1) //pickaxe
        {
            a = 1; b = 2; c = 3;
        }
        else if (equipmentList.value == 2) //tnt
        {
            a = 4; b = 5; c = 6;
        }
        else if (equipmentList.value == 3) //tnt
        {
            a = 7; b = 8; c = 9;
        }
        else if (equipmentList.value == 4) //hand drill
        {
            a = 10; b = 10; c = 10;
        }
        else if (equipmentList.value == 5) //plasma drill
        {
            a = 11; b = 12; c = 13;
        }
        #endregion
        if (equipmentList.value == 0)
        {
            if (iManager.eqLevel[equipmentList.value] == 10)
            {
                upgradeType[0].SetActive(true);
                coinLogo.SetActive(false);
                upgradeType[1].SetActive(false);
                coinText[0].SetActive(false);
                upgradeBtn.SetActive(false);
                TMPro.TextMeshProUGUI tmp = GameObject.Find("coinRequirement").GetComponent<TMPro.TextMeshProUGUI>();
                tmp.text = "Max lvl reached!";
            }
            else
            {
                upgradeType[0].SetActive(true);
                coinText[0].SetActive(true);
                coinLogo.SetActive(true);
                upgradeBtn.SetActive(true);
            }
        }
        else if (equipmentList.value == 4)
        {
            if (iManager.eqLevel[equipmentList.value] == 10)
            {
                upgradeType[0].SetActive(true);
                coinLogo.SetActive(false);
                upgradeType[1].SetActive(false);
                coinText[0].SetActive(false);
                upgradeBtn.SetActive(false);
                TMPro.TextMeshProUGUI tmp = GameObject.Find("coinRequirement").GetComponent<TMPro.TextMeshProUGUI>();
                tmp.text = "Max lvl reached!";
            }
            else
            {
                upgradeType[0].SetActive(true);
                coinText[0].SetActive(true);
                coinLogo.SetActive(true);
                upgradeBtn.SetActive(true);
            }
        }
        else if (equipmentList.value != 0)
        {
            upgradeType[0].SetActive(false);
            upgradeType[1].SetActive(false);
            if (iManager.eqLevel[equipmentList.value] == 10)
            {
                upgradeType[1].SetActive(true);
                materialOnScreen.sprite = materialImagesSprite[a];
                materialRequirement.text = iManager.oreCollection[a].ToString() + " / 1000";
                if (equipmentList.value == 1)
                {
                    materialName.text = "Coal";
                }
                else if (equipmentList.value == 2)
                {
                    materialName.text = "Gold";
                }
                else if (equipmentList.value == 3)
                {
                    materialName.text = "High Purity Ruby";
                }
                else if (equipmentList.value == 5)
                {
                    materialName.text = "High Purity Sapphire";
                }
                coinText[1].SetActive(true);
                upgradeBtn.SetActive(true);
            }
            else if (iManager.eqLevel[equipmentList.value] == 20)
            {
                upgradeType[1].SetActive(true);
                materialOnScreen.sprite = materialImagesSprite[b];
                materialRequirement.text = iManager.oreCollection[b].ToString() + " / 1000";
                if (equipmentList.value == 1)
                {
                    materialName.text = "Copper";
                }
                else if (equipmentList.value == 2)
                {
                    materialName.text = "Ruby";
                }
                else if (equipmentList.value == 3)
                {
                    materialName.text = "Sapphire";
                }
                else if (equipmentList.value == 5)
                {
                    materialName.text = "Diamond";
                }
                coinText[1].SetActive(true);
                upgradeBtn.SetActive(true);
            }
            else if (iManager.eqLevel[equipmentList.value] == 29)
            {
                upgradeType[1].SetActive(true);
                materialOnScreen.sprite = materialImagesSprite[c];
                materialRequirement.text = iManager.oreCollection[c].ToString() + " / 1000";
                if (equipmentList.value == 1)
                {
                    materialName.text = "Iron";
                }
                else if (equipmentList.value == 2)
                {
                    materialName.text = "Titanium";
                }
                else if (equipmentList.value == 3)
                {
                    materialName.text = "Emerald";
                }
                else if (equipmentList.value == 5)
                {
                    materialName.text = "Alexandrite";
                }
                coinText[1].SetActive(true);
                upgradeBtn.SetActive(true);
            }
            else if (iManager.eqLevel[equipmentList.value] == 30)
            {
                upgradeType[0].SetActive(true);
                coinLogo.SetActive(false);
                TMPro.TextMeshProUGUI tmp = GameObject.Find("coinRequirement").GetComponent<TMPro.TextMeshProUGUI>();
                tmp.text = "Max lvl reached!";
                coinText[0].SetActive(false);
                upgradeBtn.SetActive(false);
            }
            else
            {
                upgradeType[0].SetActive(true);
                coinText[0].SetActive(true);
                coinLogo.SetActive(true);
                upgradeBtn.SetActive(true);
            }   
        }

        eqLevel.text = iManager.eqLevel[equipmentList.value].ToString();
    }
    public void UpdateOreMerchant()
    {
        for (int temp = 0; temp < oreDisplay.Length; temp++)
        {
            oreDisplay[temp].sprite = materialImagesSprite[temp];
        }
        oreName[0].text = "Stone";
        oreName[1].text = "Coal";
        oreName[2].text = "Copper";
        oreName[3].text = "Iron";
        oreName[4].text = "Gold";
        oreName[5].text = "Ruby";
        oreName[6].text = "Titanium";
        oreName[7].text = "High Purity Ruby";
        oreName[8].text = "Sapphire";
        oreName[9].text = "Emerald";
        oreName[10].text = "Dwarfnium";
        oreName[11].text = "High Purity Sapphire";
        oreName[12].text = "Diamond";
        oreName[13].text = "Alexandrite";
    }
    public void MerchantChange()
    {
        for (int temp = 0; temp < 3; temp++)
        {
            if (merchantType.value == temp)
            {
                merchantScreen[temp].SetActive(true);
            }
            else merchantScreen[temp].SetActive(false);
        }
    }
    void RefreshText()
    {
        for (int temp = 0; temp < oreCollection.Length; temp++)
        {
            oreCollection[temp].text = iManager.oreCollection[temp].ToString();
        }

        for (int temp = 0; temp < oreSellValue.Length; temp++)
        {
            oreSellValue[temp].text = oreSell[temp].ToString();
        }                 
    }
}
