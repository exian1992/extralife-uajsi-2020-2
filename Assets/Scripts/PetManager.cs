using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class PetManager : MonoBehaviour
{
    GameData data;

    public PetInfo[] allPetsList;
    public PetInfo currentActivePet;
    public int petId;
    public bool petEquipped;

    public bool isItLoaded;

    public void Start()
    { 
        string saveData = "D:/SaveFile/petData.uwansummoney";
        if (!File.Exists(saveData))
        {
            SaveSystem.SavePetManager(this);
        }
        if (isItLoaded == false)
        {
            LoadData();
            isItLoaded = true;
            if (allPetsList[petId].isActive)
            {
                currentActivePet = allPetsList[petId];
            }
        }
    }
    void LoadData()
    {
        data = SaveSystem.LoadPetManager();
        if (!allPetsList[data.currentActivePetId].isActive)
        {
            petId = 0;
        }
        else
        {
            petId = data.currentActivePetId;
        }
        petEquipped = data.isItEquipped;
    }
    public void Back()
    {

    }
    private void OnApplicationQuit()
    {
        isItLoaded = false;
    }
}
