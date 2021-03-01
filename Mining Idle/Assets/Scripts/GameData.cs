using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int eqLvl = 1, stone = 0, coal = 0, bronze = 0, iron = 0;
    public float attSpd = 1f;

    public GameData (GameManager gManager)
    {
        eqLvl = gManager.eqLvl;
        attSpd = gManager.attackSpeed;

        stone = gManager.stone;
        coal = gManager.coal;
        bronze = gManager.bronze;
        iron = gManager.iron;
    }
}
