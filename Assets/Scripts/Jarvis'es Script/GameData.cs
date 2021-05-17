using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class GameData
{
    //IdleManager
    public int coin, stone, coal, copper, iron, gold, ruby, titanium;
    public int[] oreCollection = new int[16];
    //public bool isLoaded = true;

    public int[] eqLevel = new int[6];
    public float[] eqAttack = new float[6];
    public int[] eqBasePrice = new int[6];
    public int[] eqLatestPrice = new int[6];

    //QuestManager
    public int dQuestE, dQuestI, dQuestH;
    public bool eComplete, iComplete, hComplete;
    public float qProgress; //for main quest
    public float[] questProgress = new float[3];

    //PetManager
    public int currentActivePetId = 0;
    public bool isItEquippedPet = false;

    //CostumeManager
    public int currentActiveCostumeId = 0;
    public bool isItEquippedCostume = false;
    public bool otherCostumeUnlocked = false;

    //hireNPC
    public DateTime npc1, npc2;
    public bool npc1Running, npc2Running;
    public bool speedUp1, speedUp2;
    public GameData(GameManager gManager)
    {

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
        dQuestE = qManager.dQuestE;
        dQuestI = qManager.dQuestI;
        dQuestH = qManager.dQuestH;

        eComplete = qManager.eComplete;
        iComplete = qManager.iComplete;
        hComplete = qManager.hComplete;

        questProgress[0] = qManager.questProgress[0];
        questProgress[1] = qManager.questProgress[1];
        questProgress[2] = qManager.questProgress[2];
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
    public GameData(HireNPCManager npcManager)
    {
        npc1 = npcManager.npc1;
        npc2 = npcManager.npc2;
        npc1Running = npcManager.npc1Running;
        npc2Running = npcManager.npc2Running;
        speedUp1 = npcManager.speedUp1;
        speedUp2 = npcManager.speedUp2;
    }
}
