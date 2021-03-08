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
    public QuestInfo[] currentActiveQuest;
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
            currentActiveQuest[0] = questInfo[randomQuest];
            currentQuest.text = currentActiveQuest[0].questDescription;
            QuestCompleteCheck();
        }

        description.text = questInfo[randomQuest].questDescription;
        coinRewardValue.text = questInfo[randomQuest].coinReward.ToString();
    }
    private void Update()
    {
        description.text = questInfo[randomQuest].questDescription;
        coinRewardValue.text = questInfo[randomQuest].coinReward.ToString();
        if (currentActiveQuest[0] == null)
        {
            isThereQuest = false;
            currentQuest.text = "unassigned quest"; 
        }
    }
    public void AcceptQuest()
    {
        //int questTotal = currentActiveQuest.Length;
        if (currentActiveQuest[0] != null)
        {
            Debug.Log("you have an ongoing quest!");
        }
        else
        {
            currentActiveQuest[0] = questInfo[randomQuest];
            currentQuest.text = currentActiveQuest[0].questDescription;
            isThereQuest = true;
        }        
    }
    public void ClaimQuestReward()
    {
        currentActiveQuest[0].currentAmount = 0;
        GameObject temp = GameObject.Find("GameManager");
        GameManager mTemp = temp.GetComponent<GameManager>();

        mTemp.coin += currentActiveQuest[0].coinReward;
        notification.SetActive(false);
        claimReward.SetActive(false);
        Array.Clear(currentActiveQuest, 0, currentActiveQuest.Length);
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
        if (currentActiveQuest[0].IsReached())
        {
            claimReward.SetActive(true);
            notification.SetActive(true);
        }
    }
}
