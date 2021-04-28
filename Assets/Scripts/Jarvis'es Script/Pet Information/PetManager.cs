using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

[System.Serializable]
public class PetManager : MonoBehaviour
{
    GameData data;

    public PetInfo[] allPetsList;
    public PetInfo currentActivePet;
    public int petId;
    public bool petEquipped;

    public bool isItLoaded;

    public SpriteRenderer pet;

    public void Start()
    { 
        //string saveData = "D:/SaveFile/petData.uwansummoney";
        string saveData = Application.persistentDataPath + "/petData.uwansummoney";
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
                pet.sprite = currentActivePet.spriteSource;
            }
            else pet.sprite = null;
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
        petEquipped = data.isItEquippedPet;
    }
    private void OnApplicationQuit()
    {
        isItLoaded = false;
    }
}
