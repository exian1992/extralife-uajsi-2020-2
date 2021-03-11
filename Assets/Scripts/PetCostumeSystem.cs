using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PetCostumeSystem : MonoBehaviour
{
    GameObject pManager, gManager, cManager;
    PetManager petManager;
    GameManager gameManager;
    CostumeManager costumeManager;

    //pet equip screen
    public int petId; //0 = doge, 1 = goldentick, etc etc
    public PetInfo currentPetOnScreen;
    public Image currentPetSprite;
    public Text petDescription;
    public Text petName;

    //costume equip screen
    public int costumeId; //0 = klein, 1 = lynn, 2 = brook, etc etc
    public CostumeInfo currentCostumeOnScreen;
    public Image currentCostumeSprite;
    public Text costumeDescription;
    public Text costumeName;

    //UI selector
    public GameObject petScreen, costumeScreen;
    public GameObject equip, unequipSelected, locked;
    public Text switchText, usText;
    Button usButton;

    private void Start()
    {
        pManager = GameObject.Find("PetManager");
        petManager = pManager.GetComponent<PetManager>();
        gManager = GameObject.Find("GameManager");
        gameManager = gManager.GetComponent<GameManager>();
        cManager = GameObject.Find("CostumeManager");
        costumeManager = cManager.GetComponent<CostumeManager>();
        usButton = unequipSelected.GetComponent<Button>();

        #region Pet Check
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
        #endregion
        #region Costume Check
        if (costumeManager.costumeEquipped)
        {
            currentCostumeOnScreen = costumeManager.currentActiveCostume;
            for (costumeId = 0; costumeId < costumeManager.allCostumesList.Length; costumeId++)
            {
                if (currentCostumeOnScreen.costumeName == costumeManager.allCostumesList[costumeId].costumeName)
                {
                    break;
                }
            }
        }
        else
        {
            costumeId = 0;
            currentCostumeOnScreen = costumeManager.allCostumesList[costumeId];
        }
        #endregion
        RefreshText();
    }
    private void Update()
    {
        #region Pet Selection UI
        if (petScreen.activeSelf)
        {
            usText.text = "Unequip";
            usButton.interactable = true;
            currentPetOnScreen = petManager.allPetsList[petId];

            //equip button manager
            if (!petManager.allPetsList[petId].purchaseStatus)
            {
                unequipSelected.SetActive(false);
                equip.SetActive(false);
                locked.SetActive(true);
            }
            else if (petManager.allPetsList[petId].isActive)
            {
                unequipSelected.SetActive(true);
                equip.SetActive(false);
                locked.SetActive(false);
            }
            else if (!petManager.allPetsList[petId].isActive)
            {
                unequipSelected.SetActive(false);
                equip.SetActive(true);
                locked.SetActive(false);
            }
            else Debug.Log("somethings wrong here...");
        }
        #endregion

        #region Costume Selection UI
        else if (costumeScreen.activeSelf)
        {
            usText.text = "Equipped";
            usButton.interactable = false;
            currentCostumeOnScreen = costumeManager.allCostumesList[costumeId];

            //equip button manager
            if (costumeManager.allCostumesList[costumeId].isActive && !costumeManager.otherCostumeUnlocked)
            {
                unequipSelected.SetActive(false);
                equip.SetActive(false);
                locked.SetActive(false);
            }
            else if (!costumeManager.allCostumesList[costumeId].purchaseStatus)
            {
                unequipSelected.SetActive(false);
                equip.SetActive(false);
                locked.SetActive(true);
            }
            else if (costumeManager.allCostumesList[costumeId].isActive)
            {
                unequipSelected.SetActive(true);
                equip.SetActive(false);
                locked.SetActive(false);
            }
            else if (!costumeManager.allCostumesList[costumeId].isActive)
            {
                unequipSelected.SetActive(false);
                equip.SetActive(true);
                locked.SetActive(false);
            }
            else Debug.Log("somethings wrong here...");
        }
        #endregion

        RefreshText();
    }
    void RefreshText()
    {
        if (petScreen.activeSelf)
        {
            currentPetSprite.sprite = currentPetOnScreen.spriteSource;
            petDescription.text = currentPetOnScreen.petDescription;
            petName.text = currentPetOnScreen.petName;
        }
        else if (costumeScreen.activeSelf)
        {
            currentCostumeSprite.sprite = currentCostumeOnScreen.spriteSource;
            costumeDescription.text = currentCostumeOnScreen.costumeDescription;
            costumeName.text = currentCostumeOnScreen.costumeName;
        }
        else Debug.Log("it wont refresh");

    }
    public void NextPetCostume()
    {
        if (petScreen.activeSelf)
        {
            if (petId == petManager.allPetsList.Length - 1)
            {
                petId = 0;
            }
            else petId++;
        }
        else if (costumeScreen.activeSelf)
        {
            if (costumeId == costumeManager.allCostumesList.Length - 1)
            {
                costumeId = 0;
            }
            else costumeId++;
        }

    }
    public void PreviousPetCostume()
    {
        if (petScreen.activeSelf)
        {
            if (petId == 0)
            {
                petId = petManager.allPetsList.Length - 1;
            }
            else petId--;
        }
        else if (costumeScreen.activeSelf)
        {
            if (costumeId == 0)
            {
                costumeId = costumeManager.allCostumesList.Length - 1;
            }
            else costumeId--;
        }

    }
    public void EquipPet()
    {
        if (petScreen.activeSelf)
        {
            if (petManager.currentActivePet != null) petManager.currentActivePet.isActive = false;
            petManager.currentActivePet = petManager.allPetsList[petId];
            petManager.allPetsList[petId].isActive = true;
            petManager.petEquipped = true;
            petManager.petId = petId;
        }
        else if (costumeScreen.activeSelf)
        {
            if (costumeManager.currentActiveCostume != null) costumeManager.currentActiveCostume.isActive = false;
            costumeManager.currentActiveCostume = costumeManager.allCostumesList[costumeId];
            costumeManager.allCostumesList[costumeId].isActive = true;
            costumeManager.costumeEquipped = true;
            costumeManager.costumeId = costumeId;
        }
        
    }
    public void UnequipPet()
    {
        if (petScreen.activeSelf)
        {
            petManager.petEquipped = false;
            petManager.currentActivePet = null;
            petManager.allPetsList[petId].isActive = false;
        }
        else if (costumeScreen.activeSelf)
        {
            costumeManager.costumeEquipped = false;
            costumeManager.currentActiveCostume = null;
            costumeManager.allCostumesList[costumeId].isActive = false;
        }
    }
    public void Back()
    {
        SceneManager.LoadScene("MainGameplay");
        gameManager.SaveAllProgress();
        Destroy(GameObject.Find("AllManager"));
    }
    public void SwitchScreen()
    {
        if (petScreen.activeSelf)
        {
            costumeScreen.SetActive(true);
            petScreen.SetActive(false);
            switchText.text = "Pet";
        }
        else if (costumeScreen.activeSelf)
        {
            costumeScreen.SetActive(false);
            petScreen.SetActive(true);
            switchText.text = "Costume";
        }
        else Debug.Log("somehtings wrong");
    }
}
