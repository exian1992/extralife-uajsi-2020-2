using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "PetInfo")]
[System.Serializable]
public class PetInfo : ScriptableObject
{
    public string petName;
    [TextArea(3, 1)] public string petDescription;
    public StatusTypePet statusTypePet;
    public float statusValue;
    public Sprite spriteSource;
    public bool isActive;
    public bool purchaseStatus;
}

public enum StatusTypePet
{
    PowerUp,
    SpeedUp
}
