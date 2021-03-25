using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AdvanceWeapon")]
[System.Serializable]
public class AdvanceWeapon : ScriptableObject
{
    public string toolName;
    public int upgradeLevel;

    public string[] oreRequirementName;
    public int[] oreRequirementValue;
    public int coinNeeded;
}
