using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EasyQuest")]
[System.Serializable]
public class EasyQuest : ScriptableObject
{
    public string questDescription;
    public int coinReward;

    //quest detail
    public QuestType questType;

    public float requiredAmount;
    public float currentAmount;

    public void Increase(float a)
    {
        currentAmount += a;
    }
    public bool IsReached()
    {
        return (currentAmount >= requiredAmount);
    }
}
