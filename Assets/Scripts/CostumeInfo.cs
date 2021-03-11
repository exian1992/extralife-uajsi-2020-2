using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CostumeInfo")]
[System.Serializable]
public class CostumeInfo : ScriptableObject
{
    public string costumeName;
    [TextArea(4, 1)] public string costumeDescription;
    public StatusTypeCostume statusTypeCostume;
    public float statusValue;
    public Sprite spriteSource;
    public bool isActive;
    public bool purchaseStatus;
}

public enum StatusTypeCostume
{
    PowerUp,
    SpeedUp,
    None
}
