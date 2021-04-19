using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    //GameManager or IdleManager
    public int eqLvl = 1, stone = 0, coal = 0, copper = 0, iron = 0, coin = 0;
    public float attSpd = 1f;
    public int[] map1OreChance = new int[4];

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

        stone = gManager.map1OreCollection[0];
        coal = gManager.map1OreCollection[1];
        copper = gManager.map1OreCollection[2];
        iron = gManager.map1OreCollection[3];

        map1OreChance[0] = gManager.map1OreChance[0];
        map1OreChance[1] = gManager.map1OreChance[1];
        map1OreChance[2] = gManager.map1OreChance[2];
        map1OreChance[3] = gManager.map1OreChance[3];

        coin = gManager.coin;
    }
    public GameData (IdleManager iManager)
    {
        eqLvl = iManager.eqLvl;
        attSpd = iManager.defaultMiningPower;

        stone = iManager.map1OreCollection[0];
        coal = iManager.map1OreCollection[1];
        copper = iManager.map1OreCollection[2];
        iron = iManager.map1OreCollection[3];

        map1OreChance[0] = iManager.map1OreChance[0];
        map1OreChance[1] = iManager.map1OreChance[1];
        map1OreChance[2] = iManager.map1OreChance[2];
        map1OreChance[3] = iManager.map1OreChance[3];

        coin = iManager.coin;
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
