using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using Random = UnityEngine.Random;

public class QuestManager : MonoBehaviour
{
    GameData data;

    GameObject gameManager;
    GameManager gManager;
    public QuestInfo[] questInfo;
    public QuestInfo currentActiveQuest;
    public bool isThereQuest;

    public Text description;
    public Text coinRewardValue;
    public int randomQuest = 0;

    public Text currentQuest;

    public GameObject claimReward;
    public GameObject notification;

    //daily quest
    public EasyQuest[] eQuestList;
    public IntermediateQuest[] iQuestList;
    public HardQuest[] hQuestList;

    public EasyQuest activeEQuest;
    public IntermediateQuest activeIQuest;
    public HardQuest activeHQuest;
    public int dQuestE = 0, dQuestI = 0, dQuestH = 0;
    public bool eComplete, iComplete, hComplete;

    public Text eQuestText, iQuestText, hQuestText;
    public Text eProgress, iProgress, hProgress;
    public Slider eSlider, iSlider, hSlider;
    public Button eClaim, iClaim, hClaim;

    private void Start()
    {
        //string path = "D:/SaveFile/questData.uwansummoney";
        string path = Application.persistentDataPath + "/questData.uwansummoney";
        if (!File.Exists(path))
        {
            dQuestE = Random.Range(0, 3);
            activeEQuest = eQuestList[dQuestE];
            dQuestI = Random.Range(0, 3);
            activeIQuest = iQuestList[dQuestI];
            dQuestH = Random.Range(0, 3);
            activeHQuest = hQuestList[dQuestH];
            SaveSystem.SaveQuestState(this);
        }
        else
        { 
            LoadQuestState();
            activeEQuest = eQuestList[dQuestE];
            activeIQuest = iQuestList[dQuestI];
            activeHQuest = hQuestList[dQuestH];
        }

        if (isThereQuest)
        {
            currentActiveQuest = questInfo[randomQuest];
            currentQuest.text = currentActiveQuest.questDescription;
            QuestCompleteCheck();
        }
        gameManager = GameObject.Find("GameManager");
        gManager = gameManager.GetComponent<GameManager>();
    }
    private void Update()
    {
        description.text = questInfo[randomQuest].questDescription;
        coinRewardValue.text = questInfo[randomQuest].coinReward.ToString();
        if (currentActiveQuest == null)
        {
            isThereQuest = false;
            currentQuest.text = "unassigned quest"; 
        }

        #region Daily quest refreshText
        eQuestText.text = activeEQuest.questDescription;
        iQuestText.text = activeIQuest.questDescription;
        hQuestText.text = activeHQuest.questDescription;
        if (activeEQuest.currentAmount <= activeEQuest.requiredAmount)
        {
            eProgress.text = activeEQuest.currentAmount + " / " + activeEQuest.requiredAmount;
            eSlider.value = activeEQuest.currentAmount / activeEQuest.requiredAmount;
        }
        else
        {
            eProgress.text = activeEQuest.requiredAmount + " / " + activeEQuest.requiredAmount;
            eSlider.value = 1f;
        }
        if (activeIQuest.currentAmount <= activeIQuest.requiredAmount)
        {
            iProgress.text = activeIQuest.currentAmount + " / " + activeIQuest.requiredAmount;
            iSlider.value = activeIQuest.currentAmount / activeIQuest.requiredAmount;
        }
        else
        {
            iProgress.text = activeIQuest.requiredAmount + " / " + activeIQuest.requiredAmount;
            iSlider.value = 1f;
        }
        if (activeIQuest.currentAmount <= activeIQuest.requiredAmount)
        {
            hProgress.text = activeHQuest.currentAmount + " / " + activeHQuest.requiredAmount;
            hSlider.value = activeHQuest.currentAmount / activeHQuest.requiredAmount;
        }
        else
        {
            hProgress.text = activeHQuest.requiredAmount + " / " + activeHQuest.requiredAmount;
            hSlider.value = 1f;
        }
        #endregion
    }
    public void AcceptQuest()
    {
        //int questTotal = currentActiveQuest.Length;
        if (currentActiveQuest != null)
        {
            Debug.Log("you have an ongoing quest!");
        }
        else
        {
            currentActiveQuest = questInfo[randomQuest];
            currentQuest.text = currentActiveQuest.questDescription;
            isThereQuest = true;
        }        
    }
    public void ClaimQuestReward()
    {
        currentActiveQuest.currentAmount = 0;

        gManager.coin += currentActiveQuest.coinReward;
        notification.SetActive(false);
        claimReward.SetActive(false);
        currentActiveQuest = null;
        randomQuest = Random.Range(0, questInfo.Length);
        isThereQuest = false;
    }
    public void LoadQuestState()
    {
        data = SaveSystem.LoadQuestState();
        randomQuest = data.randomQuest;
        isThereQuest = data.isThereQuest;

        dQuestE = data.dQuestE;
        dQuestI = data.dQuestI;
        dQuestH = data.dQuestH;
        eComplete = data.eComplete;
        iComplete = data.iComplete;
        hComplete = data.hComplete;
    }
    private void OnApplicationQuit()
    {
        SaveSystem.SaveQuestState(this);
    }
    public void QuestCompleteCheck()
    {
        if (isThereQuest)
        {
            if (currentActiveQuest.IsReached())
            {
                claimReward.SetActive(true);
                notification.SetActive(true);
            }
        }
        if (activeEQuest.IsReached() && !eComplete)
        {
            eClaim.interactable = true;
        }
        if (activeIQuest.IsReached() && !iComplete)
        {
            iClaim.interactable = true;
        }
        if (activeHQuest.IsReached() && !hComplete)
        {
            hClaim.interactable = true;
        }
    }
    public void RefreshQuest()
    {
        activeEQuest.currentAmount = 0;
        activeIQuest.currentAmount = 0;
        activeHQuest.currentAmount = 0;

        //random manager with eqLvl
        if (data.eqLvl == 1)
        {
            dQuestE = Random.Range(0, 3);
            dQuestI = Random.Range(0, 3);
            dQuestH = Random.Range(0, 3);
        }
        else if (data.eqLvl == 2)
        {
            dQuestE = Random.Range(0, 5);
            dQuestI = Random.Range(0, 5);
            dQuestH = Random.Range(0, 5);
        }
        else if (data.eqLvl == 3)
        {
            dQuestE = Random.Range(0, 7);
            dQuestI = Random.Range(0, 7);
            dQuestH = Random.Range(0, 7);
        }
        else
        {
            dQuestE = Random.Range(0, eQuestList.Length);
            dQuestI = Random.Range(0, iQuestList.Length);
            dQuestH = Random.Range(0, hQuestList.Length);
        }
        
        activeEQuest = eQuestList[dQuestE];
        activeIQuest = iQuestList[dQuestI];
        activeHQuest = hQuestList[dQuestH];
        eComplete = false;
        iComplete = false;
        hComplete = false;
    }
    public void ClaimEQuest()
    {
        gManager.coin += activeEQuest.coinReward;
        eComplete = true;
        eClaim.interactable = false;
    }
    public void ClaimIQuest()
    {
        gManager.coin += activeIQuest.coinReward;
        iComplete = true;
        iClaim.interactable = false;
    }
    public void ClaimHQuest()
    {
        gManager.coin += activeHQuest.coinReward;
        hComplete = true;
        hClaim.interactable = false;
    }
}
