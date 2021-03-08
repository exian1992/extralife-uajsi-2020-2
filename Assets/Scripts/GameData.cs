using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int eqLvl = 1, stone = 0, coal = 0, bronze = 0, iron = 0;
    public float attSpd = 1f;

    public int stoneChance = 20, coalChance = 0, bronzeChance = 0, ironChance = 0, coin = 0;

    public GameData (GameManager gManager)
    {
        eqLvl = gManager.eqLvl;
        attSpd = gManager.attSpd;

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
}
