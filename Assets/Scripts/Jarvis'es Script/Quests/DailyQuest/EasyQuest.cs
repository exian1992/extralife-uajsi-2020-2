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
    public bool IsReached(float current)
    {
        return (current >= requiredAmount);
    }
}
