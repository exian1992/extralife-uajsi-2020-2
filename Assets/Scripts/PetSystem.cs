using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PetSystem : MonoBehaviour
{
    GameObject temp;
    PetManager pManager;

    public int petId; //0 = doge, 1 = goldentick etc etc
    public PetInfo currentPetOnScreen;
    public Image currentPetSprite;
    public Text petDescription;
    public Text petName;

    public GameObject equip, unequip;

    private void Start()
    {
        temp = GameObject.Find("PetManager");
        pManager = temp.GetComponent<PetManager>();

        if (pManager.petEquipped)
        {
            currentPetOnScreen = pManager.currentActivePet;
            for (petId = 0; petId < pManager.allPetsList.Length; petId++)
            {
                if (currentPetOnScreen.petName != pManager.allPetsList[petId].petName)
                {
                    petId++;
                }
            }
        }
        else
        {
            petId = 0;
            currentPetOnScreen = pManager.allPetsList[petId];
        }
        RefreshText();
    }
    private void Update()
    {
        currentPetOnScreen = pManager.allPetsList[petId];
        RefreshText();

        //equip button manager
        if (pManager.allPetsList[petId].isActive)
        {
            unequip.SetActive(true);
            equip.SetActive(false);
        }
        else if (!pManager.allPetsList[petId].isActive)
        {
            unequip.SetActive(false);
            equip.SetActive(true);
        }
        else Debug.Log("somethings wrong here...");
    }
    void RefreshText()
    {
        currentPetSprite.sprite = currentPetOnScreen.spriteSource;
        petDescription.text = currentPetOnScreen.petDescription;
        petName.text = currentPetOnScreen.petName;
    }
    public void NextPet()
    {
        if(petId == pManager.allPetsList.Length - 1)
        {
            petId = 0;
        }
        else petId++;
    }
    public void PreviousPet()
    {
        if (petId == 0)
        {
            petId = pManager.allPetsList.Length - 1;
        }
        else petId--;
    }
    public void EquipPet()
    {
        if(pManager.currentActivePet != null) pManager.currentActivePet.isActive = false;
        pManager.currentActivePet = pManager.allPetsList[petId];
        pManager.allPetsList[petId].isActive = true;
        pManager.petEquipped = true;
    }
    public void UnequipPet()
    {
        pManager.petEquipped = false;
        pManager.currentActivePet = null;
        pManager.allPetsList[petId].isActive = false;
    }
    public void Back()
    {
        SceneManager.LoadScene("MainGameplay");
    }
}
