using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ore : MonoBehaviour
{
    [SerializeField] string oreName;
    [SerializeField] float oreHeatlh;
    [SerializeField] bool oreActive = false;

    GameObject manager;
    GameManager gManager;

    //MSH ADA MASALAH DISINI, MSLH DAMAGE YG GBS KE INPUT PAS PAKE VOID
    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("gManager");
        gManager = manager.GetComponent<GameManager>();
    }

    public string GetName()
    {
        return oreName;
    }
    public float GetOreHealth()
    {
        return oreHeatlh;
    }
    public void OreDamage()
    {
        oreHeatlh -= gManager.Damage();
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
