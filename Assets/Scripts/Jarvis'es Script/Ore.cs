using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ore : MonoBehaviour
{
    [SerializeField] string oreName;
    [SerializeField] float oreHeatlh;

    GameObject manager;
    //GameManager gManager;
    IdleManager iManager;

    void Start()
    {
        //manager = GameObject.FindGameObjectWithTag("gManager");
        //gManager = manager.GetComponent<GameManager>();

        manager = GameObject.Find("IdleManager");
        iManager = manager.GetComponent<IdleManager>();
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
        oreHeatlh -= iManager.Damage();
        //oreHeatlh -= gManager.Damage();
    }
}
