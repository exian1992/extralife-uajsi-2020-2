using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ore : MonoBehaviour
{
    [SerializeField] string oreName;
    [SerializeField] int oreHeatlh;
    [SerializeField] bool oreActive = false;

    public string GetName()
    {
        return oreName;
    }
    public int GetOreHealth()
    {
        return oreHeatlh;
    }
    public void OreDamage()
    {
        oreHeatlh -= 1;
    }
    public bool IsOreActive()
    {
        if (oreActive)
            return true;
        else return false;
    }
    public void MakeOreActive()
    {
        oreActive = true;
    }
}
