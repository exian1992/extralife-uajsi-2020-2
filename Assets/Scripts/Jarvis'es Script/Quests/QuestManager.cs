using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;
using TMPro;

public class QuestManager : MonoBehaviour
{
    GameData data;
    IdleManager iManager;

    public GameObject questScreen;

    //daily quest
    public EasyQuest[] eQuestList;
    public IntermediateQuest[] iQuestList;
    public HardQuest[] hQuestList;

    public EasyQuest activeEQuest;
    public IntermediateQuest activeIQuest;
    public HardQuest activeHQuest;

    public float[] questProgress;

    public int dQuestE, dQuestI, dQuestH;
    public bool eComplete, iComplete, hComplete;
    public TextMeshProUGUI eQuestText, iQuestText, hQuestText;
    public TextMeshProUGUI eProgress, iProgress, hProgress;
    public Slider eSlider, iSlider, hSlider;
    public Button eClaim, iClaim, hClaim;
    public Button eRefresh, iRefresh, hRefresh;
    
    private void Start()
    {
        string path = Application.persistentDataPath + "/questData.uwansummoney";
        if (!File.Exists(path))
        {
            RefreshQuest();
            SaveProgress();
        }
        else
        {
            LoadQuestState();
            activeEQuest = eQuestList[dQuestE];
            activeIQuest = iQuestList[dQuestI];
            activeHQuest = hQuestList[dQuestH];
        }        
    }
    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "VillageChief" && questScreen.activeSelf)
        {
            QuestCompleteCheck();

            //temporary refresh quest button
            if (eComplete)
            {
                eRefresh.interactable = false;
            }
            else eRefresh.interactable = true;
            if (iComplete)
            {
                iRefresh.interactable = false;
            }
            else iRefresh.interactable = true;
            if (hComplete)
            {
                hRefresh.interactable = false;
            }
            else hRefresh.interactable = true;

            RefreshText();
        }

        iManager = GameObject.Find("IdleManager").GetComponent<IdleManager>();
    }
    public void LoadQuestState()
    {
        data = SaveSystem.LoadQuestState();

        dQuestE = data.dQuestE;
        dQuestI = data.dQuestI;
        dQuestH = data.dQuestH;

        eComplete = data.eComplete;
        iComplete = data.iComplete;
        hComplete = data.hComplete;

        questProgress[0] = data.questProgress[0];
        questProgress[1] = data.questProgress[1];
        questProgress[2] = data.questProgress[2];
    }
    public void QuestCompleteCheck()
    {
        //easy
        if (activeEQuest.IsReached(questProgress[0]) && !eComplete)
        {
            eClaim.interactable = true;
        }
        else eClaim.interactable = false;
        //intermediate
        if (activeIQuest.IsReached(questProgress[1]) && !iComplete)
        {
            iClaim.interactable = true;
        }
        else iClaim.interactable = false;
        //hard
        if (activeHQuest.IsReached(questProgress[2]) && !hComplete)
        {
            hClaim.interactable = true;
        }
        else hClaim.interactable = false;
    }
    public void ResetEQuest()
    {
        questProgress[0] = 0;
        dQuestE = Random.Range(0, eQuestList.Length);
        activeEQuest = eQuestList[dQuestE];
        eComplete = false;
    }
    public void ResetIQuest()
    {
        questProgress[1] = 0;
        dQuestI = Random.Range(0, iQuestList.Length);
        activeIQuest = iQuestList[dQuestI];
        iComplete = false;
    }
    public void ResetHQuest()
    {
        questProgress[2] = 0;
        dQuestH = Random.Range(0, hQuestList.Length);
        activeHQuest = hQuestList[dQuestH];
        hComplete = false;
    }
    public void RefreshQuest()
    {
        questProgress[0] = 0;
        questProgress[1] = 0;
        questProgress[2] = 0;

        //random manager with eqLvl
        //conditioning with eq lvl (missing)
        dQuestE = Random.Range(0, eQuestList.Length);
        dQuestI = Random.Range(0, iQuestList.Length);
        dQuestH = Random.Range(0, hQuestList.Length);
        
        activeEQuest = eQuestList[dQuestE];
        activeIQuest = iQuestList[dQuestI];
        activeHQuest = hQuestList[dQuestH];

        eComplete = false;
        iComplete = false;
        hComplete = false;
    }
    public void ClaimEQuest()
    {
        iManager.coin += activeEQuest.coinReward;
        eComplete = true;
        eClaim.interactable = false;
        SaveSystem.SaveQuestState(this);
    }
    public void ClaimIQuest()
    {
        iManager.coin += activeIQuest.coinReward;
        iComplete = true;
        iClaim.interactable = false;
        SaveSystem.SaveQuestState(this);
    }
    public void ClaimHQuest()
    {
        iManager.coin += activeHQuest.coinReward;
        hComplete = true;
        hClaim.interactable = false;
        SaveSystem.SaveQuestState(this);
    }
    void RefreshText()
    {
        eQuestText.text = activeEQuest.questDescription;
        iQuestText.text = activeIQuest.questDescription;
        hQuestText.text = activeHQuest.questDescription;

        //Easy
        if (questProgress[0] <= activeEQuest.requiredAmount)
        {
            eProgress.text = questProgress[0] + " / " + activeEQuest.requiredAmount;
            eSlider.value = questProgress[0] / activeEQuest.requiredAmount;
        }
        else
        {
            eProgress.text = activeEQuest.requiredAmount + " / " + activeEQuest.requiredAmount;
            eSlider.value = 1f;
        }
        //Intermediate
        if (questProgress[1] <= activeIQuest.requiredAmount)
        {
            iProgress.text = questProgress[1] + " / " + activeIQuest.requiredAmount;
            iSlider.value = questProgress[1] / activeIQuest.requiredAmount;
        }
        else
        {
            iProgress.text = activeIQuest.requiredAmount + " / " + activeIQuest.requiredAmount;
            iSlider.value = 1f;
        }
        //Hard
        if (questProgress[2] <= activeHQuest.requiredAmount)
        {
            hProgress.text = questProgress[2] + " / " + activeHQuest.requiredAmount;
            hSlider.value = questProgress[2] / activeHQuest.requiredAmount;
        }
        else
        {
            hProgress.text = activeHQuest.requiredAmount + " / " + activeHQuest.requiredAmount;
            hSlider.value = 1f;
        }
    }
    public void SaveProgress()
    {
        SaveSystem.SaveQuestState(this);
    }
}
