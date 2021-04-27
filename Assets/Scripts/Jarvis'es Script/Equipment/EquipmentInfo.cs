using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EquipmentInfo")]
[System.Serializable]
public class EquipmentInfo : ScriptableObject
{
    [SerializeField] string EquipmentName;
    public int eqLvl;
    public float att;

    //Upgrade stuff
    public string[] oreRequirementName;
    public int[] oreRequirementValue;
    public int basePrice;
    public int latestPrice;

    public int GetLevelInfo()
    {
        return eqLvl;
    }
    public float GetAttInfo()
    {
        return att;
    }
}
