using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class GameData
{
    //IdleManager
    public int coin, stone, coal, copper, iron, gold, ruby, titanium;
    public int[] oreCollection = new int[14];
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

    //HireNPC
    public DateTime npc1, npc2, npc3;
    public bool npc1Running, npc2Running, npc3Running;
    public bool speedUp1, speedUp2, speedUp3;
    public string[] hour = new string[3];
    public string[] map = new string[3];
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
        npc3 = npcManager.npc3;
        npc1Running = npcManager.npc1Running;
        npc2Running = npcManager.npc2Running;
        npc3Running = npcManager.npc3Running;
        speedUp1 = npcManager.speedUp1;
        speedUp2 = npcManager.speedUp2;
        speedUp3 = npcManager.speedUp3;
        hour[0] = npcManager.hour[0];
        hour[1] = npcManager.hour[1];
        hour[2] = npcManager.hour[2];
        map[0] = npcManager.map[0];
        map[1] = npcManager.map[1];
        map[2] = npcManager.map[2];
    }
}
