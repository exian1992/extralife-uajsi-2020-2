using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "QuestInfo")]
[System.Serializable]
public class QuestInfo : ScriptableObject
{
    public bool isActive;
    public string questDescription;
    public int coinReward;
    
    //quest detail
    public QuestType questType;

    public int requiredAmount;
    public int currentAmount;

    public void Increase(int a)
    {
        currentAmount += a;
        Debug.Log(currentAmount);
    }
    public bool IsReached()
    {
        return (currentAmount >= requiredAmount);
    }
}
public enum QuestType
{
    MineStone,
    SellStone,
    Tap
}
