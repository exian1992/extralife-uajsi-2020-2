using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    //IdleManager
    public int eqLvl = 1, stone = 0, coal = 0, copper = 0, iron = 0, gold = 0, ruby = 0, titanium = 0, coin = 0;
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

        stone = gManager.map1OreCollection[0];
        coal = gManager.map1OreCollection[1];
        copper = gManager.map1OreCollection[2];
        iron = gManager.map1OreCollection[3];

        coin = gManager.coin;
    }
    public GameData (IdleManager iManager)
    {
        stone = iManager.mapOreCollection[0];
        coal = iManager.mapOreCollection[1];
        copper = iManager.mapOreCollection[2];
        iron = iManager.mapOreCollection[3];
        gold = iManager.mapOreCollection[4];
        ruby = iManager.mapOreCollection[5];
        titanium = iManager.mapOreCollection[6];

        coin = iManager.coin;

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
