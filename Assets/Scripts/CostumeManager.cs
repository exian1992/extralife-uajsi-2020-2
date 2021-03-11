using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class CostumeManager : MonoBehaviour
{
    GameData data;

    public CostumeInfo[] allCostumesList;
    public CostumeInfo currentActiveCostume;
    public int costumeId;
    public bool costumeEquipped;

    public bool isItLoaded;
    public bool otherCostumeUnlocked;

    public SpriteRenderer costume;

    public void Start()
    {
        string saveData = "D:/SaveFile/costumeData.uwansummoney";
        if (!File.Exists(saveData))
        {
            SaveSystem.SaveCostumeManager(this);
        }
        if (isItLoaded == false)
        {
            LoadData();
            isItLoaded = true;
            if (allCostumesList[costumeId].isActive)
            {
                currentActiveCostume = allCostumesList[costumeId];
                costume.sprite = currentActiveCostume.spriteSource;
            }
            else costume.sprite = null;
        }
    }
    void LoadData()
    {
        data = SaveSystem.LoadCostumeManager();
        if (!allCostumesList[data.currentActiveCostumeId].isActive)
        {
            costumeId = 0;
        }
        else
        {
            costumeId = data.currentActiveCostumeId;
        }
        costumeEquipped = data.isItEquippedCostume;
        otherCostumeUnlocked = data.otherCostumeUnlocked;
    }
    private void OnApplicationQuit()
    {
        isItLoaded = false;
    }
}
