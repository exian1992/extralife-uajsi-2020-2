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

    public QuestInfo[] questInfo;
    public QuestInfo currentActiveQuest;
    public bool isThereQuest;

    public Text description;
    public Text coinRewardValue;
    public int randomQuest = 0;

    public Text currentQuest;

    public GameObject claimReward;
    public GameObject notification;

    private void Start()
    {
        string path = "D:/SaveFile/questData.uwansummoney";
        if (!File.Exists(path))
        {
            SaveSystem.SaveQuestState(this);
        }
        else LoadQuestState();
        if (isThereQuest)
        {
            currentActiveQuest = questInfo[randomQuest];
            currentQuest.text = currentActiveQuest.questDescription;
            QuestCompleteCheck();
        }

        description.text = questInfo[randomQuest].questDescription;
        coinRewardValue.text = questInfo[randomQuest].coinReward.ToString();
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
        GameObject temp = GameObject.Find("GameManager");
        GameManager mTemp = temp.GetComponent<GameManager>();

        mTemp.coin += currentActiveQuest.coinReward;
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
    }
    private void OnApplicationQuit()
    {
        SaveSystem.SaveQuestState(this);
    }
    public void QuestCompleteCheck()
    {
        if (currentActiveQuest.IsReached())
        {
            claimReward.SetActive(true);
            notification.SetActive(true);
        }
    }
}
