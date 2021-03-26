using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShopSystem : MonoBehaviour
{
    GameManager gManager;
    QuestManager questManager;
    PetManager petManager;
    CostumeManager costumeManager;

    //item collection
    public Text[] oreCollection;
    public Text eqLevel;
    public Text coinValue;

    //collection Variable
    public int[] oreSell;
    public Text[] oreSellValue;

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
    public GameObject mScreen, bScreen;
    public Text mbChangeText;

    //advance weapon    
    public AdvanceWeapon currentLevel;
    public GameObject materialLocation;
    public Sprite[] materialImagesSprite; //0 stone, 1 coal, 2 bronze, 3 iron
    public Image materialOnScreen;
    public Text materialRequirement;
    public Text coinNeeded;

    //for click delay
    int i, j, k, l;
    void Start()
    {
        #region GameObject Initiation
        gManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        questManager = GameObject.Find("QuestManager").GetComponent<QuestManager>();
        petManager = GameObject.Find("PetManager").GetComponent<PetManager>();
        costumeManager = GameObject.Find("CostumeManager").GetComponent<CostumeManager>();

        buyDogeBtn = buyDoge.GetComponent<Button>();
        buyTickBtn = buyTick.GetComponent<Button>();

        buyLynnBtn = buyLynn.GetComponent<Button>();
        buyBrookBtn = buyBrook.GetComponent<Button>();
        #endregion
        
        RefreshText();
    }
    private void Update()
    {
        RefreshText();

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
    public void ConfirmSell()
    {
        if (oreSell[0] > 0) //stone
        {
            gManager.coin += oreSell[0] * 1;
            gManager.map1OreCollection[0] -= oreSell[0];
            #region Stone selling quest
            if (questManager.isThereQuest)
            {
                if (questManager.currentActiveQuest.questType == QuestType.SellStone)
                {
                    questManager.currentActiveQuest.Increase(oreSell[0]);
                }
            }
            if (questManager.activeEQuest.questType == QuestType.SellStone)
            {
                questManager.activeEQuest.Increase(oreSell[0]);
            }
            if (questManager.activeIQuest.questType == QuestType.SellStone)
            {
                questManager.activeIQuest.Increase(oreSell[0]);
            }
            if (questManager.activeHQuest.questType == QuestType.SellStone)
            {
                questManager.activeHQuest.Increase(oreSell[0]);
            }
            #endregion

            oreSell[0] = 0;
        }
        if (oreSell[1] > 0) //coal
        {
            gManager.coin += oreSell[1] * 5;
            gManager.map1OreCollection[1] -= oreSell[1];
            #region Coal selling quest
            if (questManager.isThereQuest)
            {
                if (questManager.currentActiveQuest.questType == QuestType.SellCoal)
                {
                    questManager.currentActiveQuest.Increase(oreSell[1]);
                }
            }
            if (questManager.activeEQuest.questType == QuestType.SellCoal)
            {
                questManager.activeEQuest.Increase(oreSell[1]);
            }
            if (questManager.activeIQuest.questType == QuestType.SellCoal)
            {
                questManager.activeIQuest.Increase(oreSell[1]);
            }
            if (questManager.activeHQuest.questType == QuestType.SellCoal)
            {
                questManager.activeHQuest.Increase(oreSell[1]);
            }
            #endregion

            oreSell[1] = 0;
        }
        if (oreSell[2] > 0) //bronze
        {
            gManager.coin += oreSell[2] * 10;
            gManager.map1OreCollection[2] -= oreSell[2];
            #region Bronze selling quest
            if (questManager.isThereQuest)
            {
                if (questManager.currentActiveQuest.questType == QuestType.SellBronze)
                {
                    questManager.currentActiveQuest.Increase(oreSell[2]);
                }
            }
            if (questManager.activeEQuest.questType == QuestType.SellBronze)
            {
                questManager.activeEQuest.Increase(oreSell[2]);
            }
            if (questManager.activeIQuest.questType == QuestType.SellBronze)
            {
                questManager.activeIQuest.Increase(oreSell[2]);
            }
            if (questManager.activeHQuest.questType == QuestType.SellBronze)
            {
                questManager.activeHQuest.Increase(oreSell[2]);
            }
            #endregion

            oreSell[2] = 0;
        }
        if (oreSell[3] > 0) //iron
        {
            gManager.coin += oreSell[3] * 25;
            gManager.map1OreCollection[3] -= oreSell[3];
            #region Iron selling quest
            if (questManager.isThereQuest)
            {
                if (questManager.currentActiveQuest.questType == QuestType.SellIron)
                {
                    questManager.currentActiveQuest.Increase(oreSell[3]);
                }
            }
            if (questManager.activeEQuest.questType == QuestType.SellIron)
            {
                questManager.activeEQuest.Increase(oreSell[3]);
            }
            if (questManager.activeIQuest.questType == QuestType.SellIron)
            {
                questManager.activeIQuest.Increase(oreSell[3]);
            }
            if (questManager.activeHQuest.questType == QuestType.SellIron)
            {
                questManager.activeHQuest.Increase(oreSell[3]);
            }
            #endregion

            oreSell[3] = 0;
        }
    }
        #region StoneInc
        public void StoneIncDown()
        {
            StartCoroutine("StoneIncDelay");
        }
        public void StoneIncUp()
        {
            StopCoroutine("StoneIncDelay");
            i = 0;
        }
        IEnumerator StoneIncDelay()
        {
            for (i = 0; i > -1; i++)
            {
                if (i < 3) oreSell[0] += 1;
                else if ((gManager.map1OreCollection[0] - oreSell[0]) < 10) oreSell[0] = gManager.map1OreCollection[0];
                else if (i < 5) oreSell[0] += 10;
                else if ((gManager.map1OreCollection[0] - oreSell[0]) < 100) oreSell[0] = gManager.map1OreCollection[0];
                else if (i < 8) oreSell[0] += 100;
                else if ((gManager.map1OreCollection[0] - oreSell[0]) < 1000) oreSell[0] = gManager.map1OreCollection[0];
                else oreSell[0] += 1000;
                yield return new WaitForSeconds(0.2f);            
            }
        }
        #endregion
        #region StoneDec
        public void StoneDecDown()
        {
            StartCoroutine("StoneDecDelay");
        }
        public void StoneDecUp()
        {
            StopCoroutine("StoneDecDelay");
            i = 0;
        }
        IEnumerator StoneDecDelay()
        {
            for (i = 0; i > -1; i++)
            {
                if (i < 3) oreSell[0] -= 1;
                else if (oreSell[0] < 10) oreSell[0] = 0;
                else if (i < 5) oreSell[0] -= 10;
                else if (oreSell[0] < 100) oreSell[0] = 0;
                else if (i < 8) oreSell[0] -= 100;
                else if (oreSell[0] < 1000) oreSell[0] = 0;
                else oreSell[0] -= 1000;
                yield return new WaitForSeconds(0.2f);            
            }
        }
        #endregion
        #region CoalInc
        public void CoalIncDown()
        {
            StartCoroutine("CoalIncDelay");
        }
        public void CoalIncUp()
        {
            StopCoroutine("CoalIncDelay");
            j = 0;
        }
        IEnumerator CoalIncDelay()
        {
            for (j = 0; j > -1; j++)
            {
                if (j < 3) oreSell[1] += 1;
                else if ((gManager.map1OreCollection[1] - oreSell[1]) < 10) oreSell[1] = gManager.map1OreCollection[1];
                else if (j < 5) oreSell[1] += 10;
                else if ((gManager.map1OreCollection[1] - oreSell[1]) < 100) oreSell[1] = gManager.map1OreCollection[1];
                else if (i < 8) oreSell[1] += 100;
                else if ((gManager.map1OreCollection[1] - oreSell[1]) < 1000) oreSell[1] = gManager.map1OreCollection[1];
                else oreSell[1] += 1000;
                yield return new WaitForSeconds(0.2f);            
            }
        }
        #endregion
        #region CoalDec
        public void CoalDecDown()
        {
            StartCoroutine("CoalDecDelay");
        }
        public void CoalDecUp()
        {
            StopCoroutine("CoalDecDelay");
            j = 0;
        }
        IEnumerator CoalDecDelay()
        {
            for (j = 0; j > -1; j++)
            {
                if (j < 3) oreSell[1] -= 1;
                else if (oreSell[1] < 10) oreSell[1] = 0;
                else if (j < 5) oreSell[1] -= 10;
                else if (oreSell[1] < 100) oreSell[1] = 0;
                else if (i < 8) oreSell[1] -= 100;
                else if (oreSell[1] < 1000) oreSell[1] = 0;
                else oreSell[1] -= 1000;
                yield return new WaitForSeconds(0.2f);            
            }
        }
        #endregion
        #region BronzeInc
        public void BronzeIncDown()
        {
            StartCoroutine("BronzeIncDelay");
        }
        public void BronzeIncUp()
        {
            StopCoroutine("BronzeIncDelay");
            k = 0;
        }
        IEnumerator BronzeIncDelay()
        {
            for (k = 0; k > -1; k++)
            {
                if (k < 3) oreSell[2] += 1;
                else if ((gManager.map1OreCollection[2] - oreSell[2]) < 10) oreSell[2] = gManager.map1OreCollection[2];
                else if (k < 5) oreSell[2] += 10;
                else if ((gManager.map1OreCollection[2] - oreSell[2]) < 100) oreSell[2] = gManager.map1OreCollection[2];
                else if (i < 8) oreSell[2] += 100;
                else if ((gManager.map1OreCollection[2] - oreSell[2]) < 1000) oreSell[2] = gManager.map1OreCollection[2];
                else oreSell[2] += 1000;
                yield return new WaitForSeconds(0.2f);            
            }
        }
        #endregion
        #region BronzeDec
        public void BronzeDecDown()
        {
            StartCoroutine("BronzeDecDelay");
        }
        public void BronzeDecUp()
        {
            StopCoroutine("BronzeDecDelay");
            k = 0;
        }
        IEnumerator BronzeDecDelay()
        {
            for (k = 0; k > -1; k++)
            {
                if (k < 3) oreSell[2] -= 1;
                else if (oreSell[2] < 10) oreSell[2] = 0;
                else if (k < 5) oreSell[2] -= 10;
                else if (oreSell[2] < 100) oreSell[2] = 0;
                else if (i < 8) oreSell[2] -= 100;
                else if (oreSell[2] < 1000) oreSell[2] = 0;
                else oreSell[2] -= 1000;
                yield return new WaitForSeconds(0.2f);            
            }
        }
        #endregion
        #region IronInc
        public void IronIncDown()
        {
            StartCoroutine("IronIncDelay");
        }
        public void IronIncUp()
        {
            StopCoroutine("IronIncDelay");
            l = 0;
        }
        IEnumerator IronIncDelay()
        {
            for (l = 0; l > -1; l++)
            {
                if (l < 3) oreSell[3] += 1;
                else if ((gManager.map1OreCollection[3] - oreSell[3]) < 10) oreSell[3] = gManager.map1OreCollection[3];
                else if (l < 5) oreSell[3] += 10;
                else if ((gManager.map1OreCollection[3] - oreSell[3]) < 100) oreSell[3] = gManager.map1OreCollection[3];
                else if (i < 8) oreSell[3] += 100;
                else if ((gManager.map1OreCollection[3] - oreSell[3]) < 1000) oreSell[3] = gManager.map1OreCollection[3];
                else oreSell[3] += 1000;
                yield return new WaitForSeconds(0.2f);            
            }
        }
        #endregion
        #region IronDec
        public void IronDecDown()
        {
            StartCoroutine("IronDecDelay");
        }
        public void IronDecUp()
        {
            StopCoroutine("IronDecDelay");
            l = 0;
        }
        IEnumerator IronDecDelay()
        {
            for (l = 0; l > -1; l++)
            {
                if (l < 3) oreSell[3] -= 1;
                else if (oreSell[3] < 10) oreSell[3] = 0;
                else if (l < 5) oreSell[3] -= 10;
                else if (oreSell[3] < 100) oreSell[3] = 0;
                else if (i < 8) oreSell[3] -= 100;
                else if (oreSell[3] < 1000) oreSell[3] = 0;
                else oreSell[3] -= 1000;
                yield return new WaitForSeconds(0.2f);            
            }
        }
    #endregion
    #endregion
    #region UI Switching
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
    public void BlacksmithMerchantSwitch()
    {
        if (mScreen.activeSelf)
        {
            mbChangeText.text = "Merchant";
            bScreen.SetActive(true);
            mScreen.SetActive(false);

            AdvanceInfoRefresh();
        }
        else
        {
            mbChangeText.text = "Blacksmith";
            bScreen.SetActive(false);
            mScreen.SetActive(true);
        }
    }
    #endregion
    public void ConfirmAdvance()
    {
        bool enough = false;

        if (gManager.eqLvl == 10 && gManager.map1OreCollection[0] >= currentLevel.oreRequirementValue[0] && gManager.coin >= currentLevel.latestPrice)
        {
            enough = true;
        }
        else if (gManager.eqLvl == 20 && gManager.map1OreCollection[1] >= currentLevel.oreRequirementValue[1] && gManager.coin >= currentLevel.latestPrice)
        {
            enough = true;
        }
        else if (gManager.eqLvl == 30 && gManager.map1OreCollection[2] >= currentLevel.oreRequirementValue[2] && gManager.coin >= currentLevel.latestPrice)
        {
            enough = true;
        }
        else if (gManager.eqLvl == 40 && gManager.map1OreCollection[3] >= currentLevel.oreRequirementValue[3] && gManager.coin >= currentLevel.latestPrice)
        {
            enough = true;
        }
        else if (gManager.coin >= currentLevel.latestPrice && gManager.eqLvl != 10 && gManager.eqLvl != 20 && gManager.eqLvl != 30 && gManager.eqLvl != 40)
        {
            enough = true;        
        } 
        else
        {
            enough = false;
        }
        if (enough)
        {
            gManager.eqLvl++;
            gManager.coin -= currentLevel.latestPrice;
        }
        else
        {
            Debug.Log("Not enough material(s)/coin!");
        }

        if (enough && (gManager.eqLvl != 10 || gManager.eqLvl != 20 || gManager.eqLvl != 30))
        {
            if (gManager.eqLvl == 11 || gManager.eqLvl == 21 || gManager.eqLvl == 31)
            {
                materialLocation.SetActive(false);
                currentLevel.latestPrice += (currentLevel.latestPrice * 50 / 100);
                gManager.ChanceChecker();
            }
            else currentLevel.latestPrice += (currentLevel.basePrice * 50 / 100);
            if (gManager.eqLvl == 41)
            {
                Debug.Log("Max lvl reached!");
            }
        }
        
        else if (enough)
        {
            currentLevel.latestPrice += (currentLevel.latestPrice * 50 / 100);
        }

        AdvanceInfoRefresh();
    }
    void AdvanceInfoRefresh()
    {
        coinNeeded.text = gManager.coin + " / " + currentLevel.latestPrice.ToString();
        if (gManager.eqLvl == 10)
        {
            materialLocation.SetActive(true);
            materialOnScreen.sprite = materialImagesSprite[0];
            materialRequirement.text = gManager.map1OreCollection[0].ToString() + " / " + currentLevel.oreRequirementValue[0].ToString();
        }
        else if (gManager.eqLvl == 20)
        {
            materialLocation.SetActive(true);
            materialOnScreen.sprite = materialImagesSprite[1];
            materialRequirement.text = gManager.map1OreCollection[1].ToString() + " / " + currentLevel.oreRequirementValue[1].ToString();
        }
        else if (gManager.eqLvl == 30)
        {
            materialLocation.SetActive(true);
            materialOnScreen.sprite = materialImagesSprite[2];
            materialRequirement.text = gManager.map1OreCollection[2].ToString() + " / " + currentLevel.oreRequirementValue[2].ToString();
        }
        else if (gManager.eqLvl == 40)
        {
            materialLocation.SetActive(true);
            materialOnScreen.sprite = materialImagesSprite[3];
            materialRequirement.text = gManager.map1OreCollection[3].ToString() + " / " + currentLevel.oreRequirementValue[3].ToString();
        }
        else if (gManager.eqLvl == 41)
        {
            materialLocation.SetActive(false);
            Text temp = GameObject.Find("materialText").GetComponent<Text>();
            temp.text = "Max lvl reached!";
            GameObject.Find("coinNeeded").SetActive(false);
            GameObject.Find("advanceBtn").SetActive(false);
        }
    }
    void RefreshText()
    {
        oreCollection[0].text = gManager.map1OreCollection[0].ToString();
        oreCollection[1].text = gManager.map1OreCollection[1].ToString();
        oreCollection[2].text = gManager.map1OreCollection[2].ToString();
        oreCollection[3].text = gManager.map1OreCollection[3].ToString();

        oreSellValue[0].text = oreSell[0].ToString();
        oreSellValue[1].text = oreSell[1].ToString();
        oreSellValue[2].text = oreSell[2].ToString();
        oreSellValue[3].text = oreSell[3].ToString();

        eqLevel.text = gManager.eqLvl.ToString();
        coinValue.text = gManager.coin.ToString();
    }
}
