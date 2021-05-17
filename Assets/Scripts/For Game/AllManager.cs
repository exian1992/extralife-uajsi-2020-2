using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllManager : MonoBehaviour
{
    IdleManager iManager;
    HireNPCManager npcManager;
    QuestManager qManager;

    void Update()
    {
        iManager = GameObject.Find("IdleManager").GetComponent<IdleManager>();
        npcManager = GameObject.Find("HireNPCManager").GetComponent<HireNPCManager>();
        qManager = GameObject.Find("QuestManager").GetComponent<QuestManager>();
        //pManager = GameObject.Find("PetManager");
        //petManager = pManager.GetComponent<PetManager>();
        //cManager = GameObject.Find("CostumeManager");
        //costumeManager = cManager.GetComponent<CostumeManager>();
    }
    public void SaveAllProgress()
    {
        iManager.SaveProgress();
        qManager.SaveProgress();
        npcManager.SaveProgress();
    }
    private void OnApplicationQuit()
    {
        SaveAllProgress();
    }
}
