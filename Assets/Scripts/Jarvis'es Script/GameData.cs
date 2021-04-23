using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    //GameManager or IdleManager
    public int eqLvl = 1, stone = 0, coal = 0, copper = 0, iron = 0, gold = 0, ruby = 0, titanium = 0, coin = 0;
    public float attSpd = 1f;
    public int[] map12OreChance = new int[4];
    public int[] map3OreChance = new int[3];
    public string lastMap = "Waterfall";
    public bool isLoaded = true;

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

        map12OreChance[0] = gManager.map1OreChance[0];
        map12OreChance[1] = gManager.map1OreChance[1];
        map12OreChance[2] = gManager.map1OreChance[2];
        map12OreChance[3] = gManager.map1OreChance[3];

        coin = gManager.coin;
    }
    public GameData (IdleManager iManager)
    {
        eqLvl = iManager.eqLvl;
        attSpd = iManager.defaultMiningPower;

        stone = iManager.map12OreCollection[0];
        coal = iManager.map12OreCollection[1];
        copper = iManager.map12OreCollection[2];
        iron = iManager.map12OreCollection[3];
        gold = iManager.map3OreCollection[0];
        ruby = iManager.map3OreCollection[1];
        titanium = iManager.map3OreCollection[2];

        //map12
        map12OreChance[0] = iManager.map12OreChance[0];
        map12OreChance[1] = iManager.map12OreChance[1];
        map12OreChance[2] = iManager.map12OreChance[2];
        map12OreChance[3] = iManager.map12OreChance[3];

        //map3
        map3OreChance[0] = iManager.map3OreChance[0];
        map3OreChance[1] = iManager.map3OreChance[1];
        map3OreChance[2] = iManager.map3OreChance[2];

        coin = iManager.coin;

        lastMap = iManager.lastMap;
        isLoaded = iManager.isLoaded;
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
