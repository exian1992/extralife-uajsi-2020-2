using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    //IdleManager
    public int eqLvl = 1, coin = 0, stone, coal, copper, iron, gold, ruby, titanium;
    public int[] oreCollection = new int[16];
    //public bool isLoaded = true;

    public int[] eqLevel = new int[6];
    public float[] eqAttack = new float[6];
    public int[] eqBasePrice = new int[6];
    public int[] eqLatestPrice = new int[6];

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

        oreCollection[0] = gManager.map1OreCollection[0];
        oreCollection[1] = gManager.map1OreCollection[1];
        oreCollection[2] = gManager.map1OreCollection[2];
        oreCollection[3] = gManager.map1OreCollection[3];

        coin = gManager.coin;
    }
    public GameData (IdleManager iManager)
    {
        //ore
        for (int i = 0; i < iManager.oreCollection.Length; i++)
        {
            oreCollection[i] = iManager.oreCollection[i];
        }

        //equipment
        for (int i = 0; i < iManager.eqLevel.Length; i++)
        {
            eqLevel[i] = iManager.eqLevel[i];
            eqAttack[i] = iManager.eqAttack[i];
            eqBasePrice[i] = iManager.eqBasePrice[i];
            eqLatestPrice[i] = iManager.eqLatestPrice[i];
        }

        coin = iManager.coin;

        //isLoaded = iManager.isLoaded;
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
