using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShopSystem : MonoBehaviour
{
    //GameManager gManager;
    IdleManager iManager;
    QuestManager questManager;
    PetManager petManager;
    CostumeManager costumeManager;
    public MapManager mapManager;

    public TMPro.TextMeshProUGUI eqLevel;
    public Sprite[] materialImagesSprite; //0 stone, 1 coal, 2 bronze, 3 iron

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
        #region GameObject Initiation
        //gManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        iManager = GameObject.Find("IdleManager").GetComponent<IdleManager>();
        //questManager = GameObject.Find("QuestManager").GetComponent<QuestManager>();
        //petManager = GameObject.Find("PetManager").GetComponent<PetManager>();
        //costumeManager = GameObject.Find("CostumeManager").GetComponent<CostumeManager>();
        mapManager = GameObject.Find("MapManager").GetComponent<MapManager>();

        buyDogeBtn = buyDoge.GetComponent<Button>();
        buyTickBtn = buyTick.GetComponent<Button>();

        buyLynnBtn = buyLynn.GetComponent<Button>();
        buyBrookBtn = buyBrook.GetComponent<Button>();
        #endregion

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
        RefreshText();

        #region Ore Merchant UI Conditioning
        //normal sell
        if (oreSell[0] != 0 || oreSell[1] != 0 || oreSell[2] != 0 || oreSell[3] != 0 || oreSell[4] != 0 || oreSell[5] != 0 || oreSell[6] != 0)
        {
            sellButton.interactable = true;
        }
        else sellButton.interactable = false;

        //sell all
        if (iManager.mapOreCollection[0] != 0 || iManager.mapOreCollection[1] != 0 || iManager.mapOreCollection[2] != 0 || iManager.mapOreCollection[3] != 0 || iManager.mapOreCollection[4] != 0 || iManager.mapOreCollection[5] != 0 || iManager.mapOreCollection[6] != 0)
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
            if (oreSell[temp] > 0 && oreSell[temp] <= iManager.mapOreCollection[temp])
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
                #endregion
                iManager.coin += oreSell[temp] * price;
                iManager.mapOreCollection[temp] -= oreSell[temp];
                #region Stone selling quest
                /*if (questManager.isThereQuest)
                {
                    if (questManager.currentActiveQuest.questType == QuestType.SellStone)
                    {
                        questManager.currentActiveQuest.Increase(oreSell[temp]);
                    }
                }
                if (questManager.activeEQuest.questType == QuestType.SellStone)
                {
                    questManager.activeEQuest.Increase(oreSell[temp]);
                }
                if (questManager.activeIQuest.questType == QuestType.SellStone)
                {
                    questManager.activeIQuest.Increase(oreSell[temp]);
                }
                if (questManager.activeHQuest.questType == QuestType.SellStone)
                {
                    questManager.activeHQuest.Increase(oreSell[temp]);
                }*/
                #endregion
                #region Coal selling quest
                /*if (questManager.isThereQuest)
                {
                    if (questManager.currentActiveQuest.questType == QuestType.SellCoal)
                    {
                        questManager.currentActiveQuest.Increase(oreSell[temp]);
                    }
                }
                if (questManager.activeEQuest.questType == QuestType.SellCoal)
                {
                    questManager.activeEQuest.Increase(oreSell[temp]);
                }
                if (questManager.activeIQuest.questType == QuestType.SellCoal)
                {
                    questManager.activeIQuest.Increase(oreSell[temp]);
                }
                if (questManager.activeHQuest.questType == QuestType.SellCoal)
                {
                    questManager.activeHQuest.Increase(oreSell[temp]);
                }*/
                #endregion
                #region Copper selling quest
                /*if (questManager.isThereQuest)
                {
                    if (questManager.currentActiveQuest.questType == QuestType.SellCopper)
                    {
                        questManager.currentActiveQuest.Increase(oreSell[temp]);
                    }
                }
                if (questManager.activeEQuest.questType == QuestType.SellCopper)
                {
                    questManager.activeEQuest.Increase(oreSell[temp]);
                }
                if (questManager.activeIQuest.questType == QuestType.SellCopper)
                {
                    questManager.activeIQuest.Increase(oreSell[temp]);
                }
                if (questManager.activeHQuest.questType == QuestType.SellCopper)
                {
                    questManager.activeHQuest.Increase(oreSell[temp]);
                }*/
                #endregion
                #region Iron selling quest
                /*if (questManager.isThereQuest)
                {
                    if (questManager.currentActiveQuest.questType == QuestType.SellIron)
                    {
                        questManager.currentActiveQuest.Increase(oreSell[temp]);
                    }
                }
                if (questManager.activeEQuest.questType == QuestType.SellIron)
                {
                    questManager.activeEQuest.Increase(oreSell[temp]);
                }
                if (questManager.activeIQuest.questType == QuestType.SellIron)
                {
                    questManager.activeIQuest.Increase(oreSell[temp]);
                }
                if (questManager.activeHQuest.questType == QuestType.SellIron)
                {
                    questManager.activeHQuest.Increase(oreSell[temp]);
                }*/
                #endregion
                #region Gold selling quest
                /*if (questManager.isThereQuest)
                {
                    if (questManager.currentActiveQuest.questType == QuestType.SellGold)
                    {
                        questManager.currentActiveQuest.Increase(oreSell[temp]);
                    }
                }
                if (questManager.activeEQuest.questType == QuestType.SellGold)
                {
                    questManager.activeEQuest.Increase(oreSell[temp]);
                }
                if (questManager.activeIQuest.questType == QuestType.SellGold)
                {
                    questManager.activeIQuest.Increase(oreSell[temp]);
                }
                if (questManager.activeHQuest.questType == QuestType.SellGold)
                {
                    questManager.activeHQuest.Increase(oreSell[temp]);
                }*/
                #endregion
                #region Ruby selling quest
                /*if (questManager.isThereQuest)
                {
                    if (questManager.currentActiveQuest.questType == QuestType.SellRuby)
                    {
                        questManager.currentActiveQuest.Increase(oreSell[temp]);
                    }
                }
                if (questManager.activeEQuest.questType == QuestType.SellRuby)
                {
                    questManager.activeEQuest.Increase(oreSell[temp]);
                }
                if (questManager.activeIQuest.questType == QuestType.SellRuby)
                {
                    questManager.activeIQuest.Increase(oreSell[temp]);
                }
                if (questManager.activeHQuest.questType == QuestType.SellRuby)
                {
                    questManager.activeHQuest.Increase(oreSell[temp]);
                }*/
                #endregion
                #region Titanium selling quest
                /*if (questManager.isThereQuest)
                {
                    if (questManager.currentActiveQuest.questType == QuestType.SellTitanium)
                    {
                        questManager.currentActiveQuest.Increase(oreSell[temp]);
                    }
                }
                if (questManager.activeEQuest.questType == QuestType.SellTitanium)
                {
                    questManager.activeEQuest.Increase(oreSell[temp]);
                }
                if (questManager.activeIQuest.questType == QuestType.SellTitanium)
                {
                    questManager.activeIQuest.Increase(oreSell[temp]);
                }
                if (questManager.activeHQuest.questType == QuestType.SellTitanium)
                {
                    questManager.activeHQuest.Increase(oreSell[temp]);
                }*/
                #endregion

                oreSell[temp] = 0;
            }            
        }
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
            #endregion
            iManager.coin += iManager.mapOreCollection[temp] * price;
            iManager.mapOreCollection[temp] = 0;
            #region Stone selling quest
            /*if (questManager.isThereQuest)
            {
                if (questManager.currentActiveQuest.questType == QuestType.SellStone)
                {
                    questManager.currentActiveQuest.Increase(iManager.mapOreCollection[temp]);
                }
            }
            if (questManager.activeEQuest.questType == QuestType.SellStone)
            {
                questManager.activeEQuest.Increase(iManager.mapOreCollection[temp]);
            }
            if (questManager.activeIQuest.questType == QuestType.SellStone)
            {
                questManager.activeIQuest.Increase(iManager.mapOreCollection[temp]);
            }
            if (questManager.activeHQuest.questType == QuestType.SellStone)
            {
                questManager.activeHQuest.Increase(iManager.mapOreCollection[temp]);
            }*/
            #endregion
            #region Coal selling quest
            /*if (questManager.isThereQuest)
            {
                if (questManager.currentActiveQuest.questType == QuestType.SellCoal)
                {
                    questManager.currentActiveQuest.Increase(iManager.mapOreCollection[temp]);
                }
            }
            if (questManager.activeEQuest.questType == QuestType.SellCoal)
            {
                questManager.activeEQuest.Increase(iManager.mapOreCollection[temp]);
            }
            if (questManager.activeIQuest.questType == QuestType.SellCoal)
            {
                questManager.activeIQuest.Increase(iManager.mapOreCollection[temp]);
            }
            if (questManager.activeHQuest.questType == QuestType.SellCoal)
            {
                questManager.activeHQuest.Increase(iManager.mapOreCollection[temp]);
            }*/
            #endregion
            #region Copper selling quest
            /*if (questManager.isThereQuest)
            {
                if (questManager.currentActiveQuest.questType == QuestType.SellCopper)
                {
                    questManager.currentActiveQuest.Increase(iManager.mapOreCollection[temp]);
                }
            }
            if (questManager.activeEQuest.questType == QuestType.SellCopper)
            {
                questManager.activeEQuest.Increase(iManager.mapOreCollection[temp]);
            }
            if (questManager.activeIQuest.questType == QuestType.SellCopper)
            {
                questManager.activeIQuest.Increase(iManager.mapOreCollection[temp]);
            }
            if (questManager.activeHQuest.questType == QuestType.SellCopper)
            {
                questManager.activeHQuest.Increase(iManager.mapOreCollection[temp]);
            }*/
            #endregion
            #region Iron selling quest
            /*if (questManager.isThereQuest)
            {
                if (questManager.currentActiveQuest.questType == QuestType.SellIron)
                {
                    questManager.currentActiveQuest.Increase(iManager.mapOreCollection[temp]);
                }
            }
            if (questManager.activeEQuest.questType == QuestType.SellIron)
            {
                questManager.activeEQuest.Increase(iManager.mapOreCollection[temp]);
            }
            if (questManager.activeIQuest.questType == QuestType.SellIron)
            {
                questManager.activeIQuest.Increase(iManager.mapOreCollection[temp]);
            }
            if (questManager.activeHQuest.questType == QuestType.SellIron)
            {
                questManager.activeHQuest.Increase(iManager.mapOreCollection[temp]);
            }*/
            #endregion
            #region Gold selling quest
            /*if (questManager.isThereQuest)
            {
                if (questManager.currentActiveQuest.questType == QuestType.SellGold)
                {
                    questManager.currentActiveQuest.Increase(iManager.mapOreCollection[temp]);
                }
            }
            if (questManager.activeEQuest.questType == QuestType.SellGold)
            {
                questManager.activeEQuest.Increase(iManager.mapOreCollection[temp]);
            }
            if (questManager.activeIQuest.questType == QuestType.SellGold)
            {
                questManager.activeIQuest.Increase(iManager.mapOreCollection[temp]);
            }
            if (questManager.activeHQuest.questType == QuestType.SellGold)
            {
                questManager.activeHQuest.Increase(iManager.mapOreCollection[temp]);
            }*/
            #endregion
            #region Ruby selling quest
            /*if (questManager.isThereQuest)
            {
                if (questManager.currentActiveQuest.questType == QuestType.SellRuby)
                {
                    questManager.currentActiveQuest.Increase(iManager.mapOreCollection[temp]);
                }
            }
            if (questManager.activeEQuest.questType == QuestType.SellRuby)
            {
                questManager.activeEQuest.Increase(iManager.mapOreCollection[temp]);
            }
            if (questManager.activeIQuest.questType == QuestType.SellRuby)
            {
                questManager.activeIQuest.Increase(iManager.mapOreCollection[temp]);
            }
            if (questManager.activeHQuest.questType == QuestType.SellRuby)
            {
                questManager.activeHQuest.Increase(iManager.mapOreCollection[temp]);
            }*/
            #endregion
            #region Titanium selling quest
            /*if (questManager.isThereQuest)
            {
                if (questManager.currentActiveQuest.questType == QuestType.SellTitanium)
                {
                    questManager.currentActiveQuest.Increase(iManager.mapOreCollection[temp]);
                }
            }
            if (questManager.activeEQuest.questType == QuestType.SellTitanium)
            {
                questManager.activeEQuest.Increase(iManager.mapOreCollection[temp]);
            }
            if (questManager.activeIQuest.questType == QuestType.SellTitanium)
            {
                questManager.activeIQuest.Increase(iManager.mapOreCollection[temp]);
            }
            if (questManager.activeHQuest.questType == QuestType.SellTitanium)
            {
                questManager.activeHQuest.Increase(iManager.mapOreCollection[temp]);
            }*/
            #endregion

            oreSell[temp] = 0;
        }
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
            if (oreSell[oreNumber] >= iManager.mapOreCollection[oreNumber]) oreSell[oreNumber] = iManager.mapOreCollection[oreNumber];
            else if (i < 3) oreSell[oreNumber] += 1;
            else if ((iManager.mapOreCollection[oreNumber] - oreSell[oreNumber]) < 10) oreSell[oreNumber] = iManager.mapOreCollection[oreNumber];
            else if (i < 5) oreSell[oreNumber] += 10;
            else if ((iManager.mapOreCollection[oreNumber] - oreSell[oreNumber]) < 100) oreSell[oreNumber] = iManager.mapOreCollection[oreNumber];
            else if (i < 8) oreSell[oreNumber] += 100;
            else if ((iManager.mapOreCollection[oreNumber] - oreSell[oreNumber]) < 1000) oreSell[oreNumber] = iManager.mapOreCollection[oreNumber];
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

        if (iManager.coin >= iManager.equipmentList[equipmentList.value].latestPrice)
        {
            if (iManager.equipmentList[equipmentList.value].eqLvl != 10 && iManager.equipmentList[equipmentList.value].eqLvl != 20 && iManager.equipmentList[equipmentList.value].eqLvl != 29)
            {
                enough = true;
            }
            #region Enough ore checking
            else if (equipmentList.value == 1)
            {
                if (iManager.mapOreCollection[1] >= iManager.equipmentList[1].oreRequirementValue[0] && iManager.equipmentList[1].eqLvl == 10)
                {
                    enough = true;
                    iManager.mapOreCollection[1] -= iManager.equipmentList[1].oreRequirementValue[0];
                }
                else if (iManager.mapOreCollection[2] >= iManager.equipmentList[1].oreRequirementValue[1] && iManager.equipmentList[1].eqLvl == 20)
                {
                    enough = true;
                    iManager.mapOreCollection[2] -= iManager.equipmentList[1].oreRequirementValue[1];
                }
                else if (iManager.mapOreCollection[3] >= iManager.equipmentList[1].oreRequirementValue[2] && iManager.equipmentList[1].eqLvl == 29)
                {
                    enough = true;
                    iManager.mapOreCollection[3] -= iManager.equipmentList[1].oreRequirementValue[2];
                }
            }
            else if (equipmentList.value == 2)
            {
                if (iManager.mapOreCollection[4] >= iManager.equipmentList[2].oreRequirementValue[0] && iManager.equipmentList[2].eqLvl == 10)
                {
                    enough = true;
                    iManager.mapOreCollection[4] -= iManager.equipmentList[2].oreRequirementValue[0];
                }
                else if (iManager.mapOreCollection[5] >= iManager.equipmentList[2].oreRequirementValue[1] && iManager.equipmentList[2].eqLvl == 20)
                {
                    enough = true;
                    iManager.mapOreCollection[5] -= iManager.equipmentList[2].oreRequirementValue[1];
                }
                else if (iManager.mapOreCollection[6] >= iManager.equipmentList[2].oreRequirementValue[2] && iManager.equipmentList[2].eqLvl == 29)
                {
                    enough = true;
                    iManager.mapOreCollection[6] -= iManager.equipmentList[2].oreRequirementValue[2];
                }
            }
            #endregion
        }

        if (enough)
        {
            StartCoroutine("BrookAnim");

            //Equipment value go up (level, attack power)
            iManager.equipmentList[equipmentList.value].eqLvl++;
            iManager.equipmentList[equipmentList.value].att += 0.15f;
            iManager.coin -= iManager.equipmentList[equipmentList.value].latestPrice;

            //Equipment pricing
            if (enough && (iManager.equipmentList[equipmentList.value].eqLvl != 10 || iManager.equipmentList[equipmentList.value].eqLvl != 20 || iManager.equipmentList[equipmentList.value].eqLvl != 29))
            {
                if (iManager.equipmentList[equipmentList.value].eqLvl == 11 || iManager.equipmentList[equipmentList.value].eqLvl == 21 || iManager.equipmentList[equipmentList.value].eqLvl == 29)
                {
                    iManager.equipmentList[equipmentList.value].latestPrice += (iManager.equipmentList[equipmentList.value].latestPrice * 50 / 100);
                }
                else iManager.equipmentList[equipmentList.value].latestPrice += (iManager.equipmentList[equipmentList.value].basePrice * 50 / 100);
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
        coinRequirement[0].text = iManager.coin + " / " + iManager.equipmentList[equipmentList.value].latestPrice.ToString();
        coinRequirement[1].text = iManager.coin + " / " + iManager.equipmentList[equipmentList.value].latestPrice.ToString();

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
        #endregion
        if (equipmentList.value == 0)
        {
            if (iManager.equipmentList[equipmentList.value].eqLvl == 10)
            {
                upgradeType[0].SetActive(true);
                coinLogo.SetActive(false);
                upgradeType[1].SetActive(false);
                coinText[0].SetActive(false);
                upgradeBtn.SetActive(false);
                TMPro.TextMeshProUGUI tmp = GameObject.Find("coinRequirement").GetComponent<TMPro.TextMeshProUGUI>();
                tmp.text = "Max lvl reached!";
            }
        }
        else if (equipmentList.value != 0)
        {
            upgradeType[0].SetActive(false);
            upgradeType[1].SetActive(false);
            if (iManager.equipmentList[equipmentList.value].eqLvl == 10)
            {
                upgradeType[1].SetActive(true);
                materialOnScreen.sprite = materialImagesSprite[a];
                materialRequirement.text = iManager.mapOreCollection[a].ToString() + " / " + iManager.equipmentList[equipmentList.value].oreRequirementValue[0].ToString();
                materialName.text = iManager.equipmentList[equipmentList.value].oreRequirementName[equipmentList.value];
                coinText[1].SetActive(true);
                upgradeBtn.SetActive(true);
                GameObject.Find("upgradeText").GetComponent<TMPro.TextMeshProUGUI>().text = "Advance";
            }
            else if (iManager.equipmentList[equipmentList.value].eqLvl == 20)
            {
                upgradeType[1].SetActive(true);
                materialOnScreen.sprite = materialImagesSprite[b];
                materialRequirement.text = iManager.mapOreCollection[b].ToString() + " / " + iManager.equipmentList[equipmentList.value].oreRequirementValue[1].ToString();
                materialName.text = iManager.equipmentList[equipmentList.value].oreRequirementName[equipmentList.value];
                coinText[1].SetActive(true);
                upgradeBtn.SetActive(true);
                GameObject.Find("upgradeText").GetComponent<TMPro.TextMeshProUGUI>().text = "Advance";
            }
            else if (iManager.equipmentList[equipmentList.value].eqLvl == 29)
            {
                upgradeType[1].SetActive(true);
                materialOnScreen.sprite = materialImagesSprite[c];
                materialRequirement.text = iManager.mapOreCollection[c].ToString() + " / " + iManager.equipmentList[equipmentList.value].oreRequirementValue[2].ToString();
                materialName.text = iManager.equipmentList[equipmentList.value].oreRequirementName[equipmentList.value];
                coinText[1].SetActive(true);
                upgradeBtn.SetActive(true);
                GameObject.Find("upgradeText").GetComponent<TMPro.TextMeshProUGUI>().text = "Advance";
            }
            else if (iManager.equipmentList[equipmentList.value].eqLvl == 30)
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
                GameObject.Find("upgradeText").GetComponent<TMPro.TextMeshProUGUI>().text = "Upgrade";
            }   
        }

        eqLevel.text = iManager.equipmentList[equipmentList.value].eqLvl.ToString();
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
            oreCollection[temp].text = iManager.mapOreCollection[temp].ToString();
        }

        for (int temp = 0; temp < oreSellValue.Length; temp++)
        {
            oreSellValue[temp].text = oreSell[temp].ToString();
        }                 
    }
}
