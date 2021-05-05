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
    public string[] materialNames;
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
        if (iManager.oreCollection[0] != 0 || iManager.oreCollection[1] != 0 || iManager.oreCollection[2] != 0 || iManager.oreCollection[3] != 0 || iManager.oreCollection[4] != 0 || iManager.oreCollection[5] != 0 || iManager.oreCollection[6] != 0)
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
                #endregion
                iManager.coin += oreSell[temp] * price;
                iManager.oreCollection[temp] -= oreSell[temp];
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
            iManager.coin += iManager.oreCollection[temp] * price;
            iManager.oreCollection[temp] = 0;
            #region Stone selling quest
            /*if (questManager.isThereQuest)
            {
                if (questManager.currentActiveQuest.questType == QuestType.SellStone)
                {
                    questManager.currentActiveQuest.Increase(iManager.oreCollection[temp]);
                }
            }
            if (questManager.activeEQuest.questType == QuestType.SellStone)
            {
                questManager.activeEQuest.Increase(iManager.oreCollection[temp]);
            }
            if (questManager.activeIQuest.questType == QuestType.SellStone)
            {
                questManager.activeIQuest.Increase(iManager.oreCollection[temp]);
            }
            if (questManager.activeHQuest.questType == QuestType.SellStone)
            {
                questManager.activeHQuest.Increase(iManager.oreCollection[temp]);
            }*/
            #endregion
            #region Coal selling quest
            /*if (questManager.isThereQuest)
            {
                if (questManager.currentActiveQuest.questType == QuestType.SellCoal)
                {
                    questManager.currentActiveQuest.Increase(iManager.oreCollection[temp]);
                }
            }
            if (questManager.activeEQuest.questType == QuestType.SellCoal)
            {
                questManager.activeEQuest.Increase(iManager.oreCollection[temp]);
            }
            if (questManager.activeIQuest.questType == QuestType.SellCoal)
            {
                questManager.activeIQuest.Increase(iManager.oreCollection[temp]);
            }
            if (questManager.activeHQuest.questType == QuestType.SellCoal)
            {
                questManager.activeHQuest.Increase(iManager.oreCollection[temp]);
            }*/
            #endregion
            #region Copper selling quest
            /*if (questManager.isThereQuest)
            {
                if (questManager.currentActiveQuest.questType == QuestType.SellCopper)
                {
                    questManager.currentActiveQuest.Increase(iManager.oreCollection[temp]);
                }
            }
            if (questManager.activeEQuest.questType == QuestType.SellCopper)
            {
                questManager.activeEQuest.Increase(iManager.oreCollection[temp]);
            }
            if (questManager.activeIQuest.questType == QuestType.SellCopper)
            {
                questManager.activeIQuest.Increase(iManager.oreCollection[temp]);
            }
            if (questManager.activeHQuest.questType == QuestType.SellCopper)
            {
                questManager.activeHQuest.Increase(iManager.oreCollection[temp]);
            }*/
            #endregion
            #region Iron selling quest
            /*if (questManager.isThereQuest)
            {
                if (questManager.currentActiveQuest.questType == QuestType.SellIron)
                {
                    questManager.currentActiveQuest.Increase(iManager.oreCollection[temp]);
                }
            }
            if (questManager.activeEQuest.questType == QuestType.SellIron)
            {
                questManager.activeEQuest.Increase(iManager.oreCollection[temp]);
            }
            if (questManager.activeIQuest.questType == QuestType.SellIron)
            {
                questManager.activeIQuest.Increase(iManager.oreCollection[temp]);
            }
            if (questManager.activeHQuest.questType == QuestType.SellIron)
            {
                questManager.activeHQuest.Increase(iManager.oreCollection[temp]);
            }*/
            #endregion
            #region Gold selling quest
            /*if (questManager.isThereQuest)
            {
                if (questManager.currentActiveQuest.questType == QuestType.SellGold)
                {
                    questManager.currentActiveQuest.Increase(iManager.oreCollection[temp]);
                }
            }
            if (questManager.activeEQuest.questType == QuestType.SellGold)
            {
                questManager.activeEQuest.Increase(iManager.oreCollection[temp]);
            }
            if (questManager.activeIQuest.questType == QuestType.SellGold)
            {
                questManager.activeIQuest.Increase(iManager.oreCollection[temp]);
            }
            if (questManager.activeHQuest.questType == QuestType.SellGold)
            {
                questManager.activeHQuest.Increase(iManager.oreCollection[temp]);
            }*/
            #endregion
            #region Ruby selling quest
            /*if (questManager.isThereQuest)
            {
                if (questManager.currentActiveQuest.questType == QuestType.SellRuby)
                {
                    questManager.currentActiveQuest.Increase(iManager.oreCollection[temp]);
                }
            }
            if (questManager.activeEQuest.questType == QuestType.SellRuby)
            {
                questManager.activeEQuest.Increase(iManager.oreCollection[temp]);
            }
            if (questManager.activeIQuest.questType == QuestType.SellRuby)
            {
                questManager.activeIQuest.Increase(iManager.oreCollection[temp]);
            }
            if (questManager.activeHQuest.questType == QuestType.SellRuby)
            {
                questManager.activeHQuest.Increase(iManager.oreCollection[temp]);
            }*/
            #endregion
            #region Titanium selling quest
            /*if (questManager.isThereQuest)
            {
                if (questManager.currentActiveQuest.questType == QuestType.SellTitanium)
                {
                    questManager.currentActiveQuest.Increase(iManager.oreCollection[temp]);
                }
            }
            if (questManager.activeEQuest.questType == QuestType.SellTitanium)
            {
                questManager.activeEQuest.Increase(iManager.oreCollection[temp]);
            }
            if (questManager.activeIQuest.questType == QuestType.SellTitanium)
            {
                questManager.activeIQuest.Increase(iManager.oreCollection[temp]);
            }
            if (questManager.activeHQuest.questType == QuestType.SellTitanium)
            {
                questManager.activeHQuest.Increase(iManager.oreCollection[temp]);
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
            #region Enough ore checking
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
