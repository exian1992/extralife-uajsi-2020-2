using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    //GameManager
    public int eqLvl = 1, stone = 0, coal = 0, bronze = 0, iron = 0;
    public float attSpd = 1f;
    public int stoneChance = 20, coalChance = 0, bronzeChance = 0, ironChance = 0, coin = 0;

    //QuestManager
    public int randomQuest = 0;
    public bool isThereQuest = false;
    public int dQuestE = 0, dQuestI = 1, dQuestH = 2;
    public bool eComplete, iComplete, hComplete;

    //PetManager
    public int currentActivePetId = 0;
    public bool isItEquippedPet = false;

    //CostumeManager
    public int currentActiveCostumeId = 0;
    public bool isItEquippedCostume = false;
    public bool otherCostumeUnlocked = false;
    public GameData (GameManager gManager)
    {
        eqLvl = gManager.eqLvl;
        attSpd = gManager.defaultMiningPower;

        stone = gManager.stone;
        coal = gManager.coal;
        bronze = gManager.bronze;
        iron = gManager.iron;

        stoneChance = gManager.stoneChance;
        coalChance = gManager.coalChance;
        bronzeChance = gManager.bronzeChance;
        ironChance = gManager.ironChance;

        coin = gManager.coin;
    }
    public GameData (QuestManager qManager)
    {
        randomQuest = qManager.randomQuest;
        isThereQuest = qManager.isThereQuest;

        dQuestE = qManager.dQuestE;
        dQuestI = qManager.dQuestI;
        dQuestH = qManager.dQuestH;
        eComplete = qManager.eComplete;
        iComplete = qManager.iComplete;
        hComplete = qManager.hComplete;
    }
    public GameData (PetManager pManager)
    {
        if (pManager.petEquipped)
        {
            currentActivePetId = pManager.petId;
        }
        isItEquippedPet = pManager.petEquipped;
    }
    public GameData(CostumeManager cManager)
    {
        if (cManager.costumeEquipped)
        {
            currentActiveCostumeId = cManager.costumeId;
        }
        otherCostumeUnlocked = cManager.otherCostumeUnlocked;
        isItEquippedCostume = cManager.costumeEquipped;
    }
}
