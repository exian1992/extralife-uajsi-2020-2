using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PetSystem : MonoBehaviour
{
    GameObject pManager, gManager;
    PetManager petManager;
    GameManager gameManager;

    public int petId; //0 = doge, 1 = goldentick etc etc
    public PetInfo currentPetOnScreen;
    public Image currentPetSprite;
    public Text petDescription;
    public Text petName;

    public GameObject equip, unequip, locked;

    private void Start()
    {
        pManager = GameObject.Find("PetManager");
        petManager = pManager.GetComponent<PetManager>();
        gManager = GameObject.Find("GameManager");
        gameManager = gManager.GetComponent<GameManager>();

        if (petManager.petEquipped)
        {
            currentPetOnScreen = petManager.currentActivePet;
            for (petId = 0; petId < petManager.allPetsList.Length; petId++)
            {
                if (currentPetOnScreen.petName == petManager.allPetsList[petId].petName)
                {
                    break;
                }
            }
        }
        else
        {
            petId = 0;
            currentPetOnScreen = petManager.allPetsList[petId];
        }
        RefreshText();
    }
    private void Update()
    {
        currentPetOnScreen = petManager.allPetsList[petId];
        RefreshText();

        //equip button manager
        if (!petManager.allPetsList[petId].purchaseStatus)
        {
            unequip.SetActive(false);
            equip.SetActive(false);
            locked.SetActive(true);
        }
        else if (petManager.allPetsList[petId].isActive)
        {
            unequip.SetActive(true);
            equip.SetActive(false);
            locked.SetActive(false);
        }
        else if (!petManager.allPetsList[petId].isActive)
        {
            unequip.SetActive(false);
            equip.SetActive(true);
            locked.SetActive(false);
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
        if(petId == petManager.allPetsList.Length - 1)
        {
            petId = 0;
        }
        else petId++;
    }
    public void PreviousPet()
    {
        if (petId == 0)
        {
            petId = petManager.allPetsList.Length - 1;
        }
        else petId--;
    }
    public void EquipPet()
    {
        if(petManager.currentActivePet != null) petManager.currentActivePet.isActive = false;
        petManager.currentActivePet = petManager.allPetsList[petId];
        petManager.allPetsList[petId].isActive = true;
        petManager.petEquipped = true;
        petManager.petId = petId;
    }
    public void UnequipPet()
    {
        petManager.petEquipped = false;
        petManager.currentActivePet = null;
        petManager.allPetsList[petId].isActive = false;
    }
    public void Back()
    {
        SceneManager.LoadScene("MainGameplay");
        gameManager.SaveAllProgress();
        Destroy(GameObject.Find("AllManager"));
    }
}
