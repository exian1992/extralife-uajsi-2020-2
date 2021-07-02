using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System;

public class MapManager : MonoBehaviour
{
    AllManager allManager;
    IdleManager iManager;
    HireNPCManager npcManager;
    QuestManager qManager;

    public string toWhatShop;
    public GameObject[] managerChecker, musicChecker;
    public TMPro.TextMeshProUGUI coin;
    public GameObject settings;
    public AudioSource music;

    //notification
    [SerializeField] Sprite mapNotifYes, mapNotifNo;
    public Image mapIcon;
    public GameObject notification, npcNotif, chiefNotif, questChiefNotif;
    //villageChief
    public GameObject questScreen, chiefScreen;
    //hireNPC
    public GameObject npcScreen, npcSelect, mapTimeSelect;

    public GameObject credit;
    
    private void Start()
    {
        Application.targetFrameRate = -1;

        mapNotifNo = Resources.Load<Sprite>("mapNotifNo");
        mapNotifYes = Resources.Load<Sprite>("mapNotifYes");

        //map manager gameobject check
        managerChecker = GameObject.FindGameObjectsWithTag("mManager"); //mManager = mapManager
        if (managerChecker.Length == 2)
        {
            if (managerChecker[0].GetComponent<MapManager>().toWhatShop != null)
            {
                managerChecker[1].GetComponent<MapManager>().toWhatShop = managerChecker[0].GetComponent<MapManager>().toWhatShop;
            }
            Destroy(managerChecker[0]);
            DontDestroyOnLoad(managerChecker[1]);
        }
        else
        {
            DontDestroyOnLoad(managerChecker[0]);
        }
        musicChecker = GameObject.FindGameObjectsWithTag("audio");

        if (musicChecker.Length == 2 && musicChecker[0].GetComponent<AudioSource>().clip == musicChecker[1].GetComponent<AudioSource>().clip)
        {
            Destroy(musicChecker[1]);            
        }
        else if (musicChecker.Length == 2)
        {
            Destroy(musicChecker[0]);
            DontDestroyOnLoad(musicChecker[1]);
        }
        else DontDestroyOnLoad(musicChecker[0]);
    }
    private void Update()
    {
        allManager = GameObject.Find("AllManager").GetComponent<AllManager>();
        iManager = GameObject.Find("IdleManager").GetComponent<IdleManager>();
        npcManager = GameObject.Find("HireNPCManager").GetComponent<HireNPCManager>();
        qManager = GameObject.Find("QuestManager").GetComponent<QuestManager>();

        coin = GameObject.Find("CoinText").GetComponent<TMPro.TextMeshProUGUI>();
        coin.text = iManager.coin.ToString();        

        //back key
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                allManager.SaveAllProgress();

            if (SceneManager.GetActiveScene().name == "Waterfall" ||
                SceneManager.GetActiveScene().name == "Cave" ||
                SceneManager.GetActiveScene().name == "DeepCave" ||
                SceneManager.GetActiveScene().name == "EarthMantle" ||
                SceneManager.GetActiveScene().name == "DwarfVillage" ||
                SceneManager.GetActiveScene().name == "EarthCore")
            {
                SceneManager.LoadScene("Map");
            }
            else if (SceneManager.GetActiveScene().name == "Village")
            {
                if (npcScreen.activeSelf)
                {
                    if (npcSelect.activeSelf)
                    {
                        npcScreen.SetActive(false);
                    }
                    else if (mapTimeSelect.activeSelf)
                    {
                        npcManager.TimeMapReset();
                        npcSelect.SetActive(true);
                        mapTimeSelect.SetActive(false);
                    }
                }
                //"are u sure?" thing
                else Application.Quit();
            }
            else if (SceneManager.GetActiveScene().name == "Shop" ||
                     SceneManager.GetActiveScene().name == "Map")
            {
                SceneManager.LoadScene("Village");
            }
            else if (SceneManager.GetActiveScene().name == "VillageChief")
            {
                if (!chiefScreen.activeSelf)
                {
                    questScreen.SetActive(false);
                    chiefScreen.SetActive(true);
                }
                else SceneManager.LoadScene("Village");
            }
            else Debug.Log("no statement yet...");
            }
        }

        //setting menu close when clicked outside setting area
        if (Input.GetMouseButtonUp(0) && 
            (EventSystem.current.currentSelectedGameObject == null || 
            !(EventSystem.current.currentSelectedGameObject.name == "audio" || 
            EventSystem.current.currentSelectedGameObject.name == "googlePlay" || 
            EventSystem.current.currentSelectedGameObject.name == "credits" ||
            EventSystem.current.currentSelectedGameObject.name == "settingArea" ||
            EventSystem.current.currentSelectedGameObject.name == "Setting" ||
            EventSystem.current.currentSelectedGameObject.name == "creditsImage")))
        {
            credit.GetComponent<SimpleFade>().Deactivate();
            credit.SetActive(false);
            settings.SetActive(false);
        }

        //notification checker
        if (SceneManager.GetActiveScene().name == "Shop" ||
            SceneManager.GetActiveScene().name == "Map" ||
            SceneManager.GetActiveScene().name == "VillageChief")
        {
            if (SceneManager.GetActiveScene().name == "VillageChief")
            {
                //quest in chief house
                if (!questScreen.activeSelf)
                {
                    if (qManager.activeEQuest.IsReached(qManager.questProgress[0]) && !qManager.eComplete)
                    {
                        questChiefNotif.SetActive(true);
                    }
                    else if (qManager.activeIQuest.IsReached(qManager.questProgress[1]) && !qManager.iComplete)
                    {
                        questChiefNotif.SetActive(true);
                    }
                    else if (qManager.activeHQuest.IsReached(qManager.questProgress[2]) && !qManager.hComplete)
                    {
                        questChiefNotif.SetActive(true);
                    }
                    else questChiefNotif.SetActive(false);
                }

                //normal notif
                if (npcManager.npc1Running && (DateTime.Now.Ticks >= npcManager.npc1.Ticks || npcManager.speedUp1))
                {
                    notification.SetActive(true);
                }
                else if (npcManager.npc2Running && (DateTime.Now.Ticks >= npcManager.npc2.Ticks || npcManager.speedUp2))
                {
                    notification.SetActive(true);
                }
                else if (npcManager.npc3Running && (DateTime.Now.Ticks >= npcManager.npc3.Ticks || npcManager.speedUp3))
                {
                    notification.SetActive(true);
                }
                else notification.SetActive(false);                
            }
            else if (npcManager.npc1Running && (DateTime.Now.Ticks >= npcManager.npc1.Ticks || npcManager.speedUp1))
            {
                notification.SetActive(true);
            }
            else if (npcManager.npc2Running && (DateTime.Now.Ticks >= npcManager.npc2.Ticks || npcManager.speedUp2))
            {
                notification.SetActive(true);
            }
            else if (npcManager.npc3Running && (DateTime.Now.Ticks >= npcManager.npc3.Ticks || npcManager.speedUp3))
            {
                notification.SetActive(true);
            }
            //daily quest
            else if (qManager.activeEQuest.IsReached(qManager.questProgress[0]) && !qManager.eComplete && SceneManager.GetActiveScene().name != "VillageChief")
            {
                notification.SetActive(true);
            }
            else if (qManager.activeIQuest.IsReached(qManager.questProgress[1]) && !qManager.iComplete && SceneManager.GetActiveScene().name != "VillageChief")
            {
                notification.SetActive(true);
            }
            else if (qManager.activeHQuest.IsReached(qManager.questProgress[2]) && !qManager.hComplete && SceneManager.GetActiveScene().name != "VillageChief")
            {
                notification.SetActive(true);
            }
            else notification.SetActive(false);
        }
        else if (SceneManager.GetActiveScene().name == "Village")
        {
            //hireNPC
            if (npcManager.npc1Running && (DateTime.Now.Ticks >= npcManager.npc1.Ticks || npcManager.speedUp1))
            {
                npcNotif.SetActive(true);
            }
            else if (npcManager.npc2Running && (DateTime.Now.Ticks >= npcManager.npc2.Ticks || npcManager.speedUp2))
            {
                npcNotif.SetActive(true);
            }
            else if (npcManager.npc3Running && (DateTime.Now.Ticks >= npcManager.npc3.Ticks || npcManager.speedUp3))
            {
                npcNotif.SetActive(true);
            }
            else npcNotif.SetActive(false);

            //villageChief
            if (qManager.activeEQuest.IsReached(qManager.questProgress[0]) && !qManager.eComplete)
            {
                chiefNotif.SetActive(true);
            }
            else if (qManager.activeIQuest.IsReached(qManager.questProgress[1]) && !qManager.iComplete)
            {
                chiefNotif.SetActive(true);
            }
            else if (qManager.activeHQuest.IsReached(qManager.questProgress[2]) && !qManager.hComplete)
            {
                chiefNotif.SetActive(true);
            }
            else chiefNotif.SetActive(false);
        }
        else if (!(SceneManager.GetActiveScene().name == "Village"))
        {
            if (npcManager.npc1Running && (DateTime.Now.Ticks >= npcManager.npc1.Ticks || npcManager.speedUp1))
            {
                mapIcon.sprite = mapNotifYes;
                Debug.Log("shit should work");
            }
            else if (npcManager.npc2Running && (DateTime.Now.Ticks >= npcManager.npc2.Ticks || npcManager.speedUp2))
            {
                mapIcon.sprite = mapNotifYes;
            }
            else if (npcManager.npc3Running && (DateTime.Now.Ticks >= npcManager.npc3.Ticks || npcManager.speedUp3))
            {
                mapIcon.sprite = mapNotifYes;
            }
            
            //daily quest
            else if (qManager.activeEQuest.IsReached(qManager.questProgress[0]) && !qManager.eComplete && SceneManager.GetActiveScene().name != "VillageChief")
            {
                mapIcon.sprite = mapNotifYes;
            }
            else if (qManager.activeIQuest.IsReached(qManager.questProgress[1]) && !qManager.iComplete && SceneManager.GetActiveScene().name != "VillageChief")
            {
                mapIcon.sprite = mapNotifYes;
            }
            else if (qManager.activeHQuest.IsReached(qManager.questProgress[2]) && !qManager.hComplete && SceneManager.GetActiveScene().name != "VillageChief")
            {
                mapIcon.sprite = mapNotifYes;
            }
            else mapIcon.sprite = mapNotifNo;
        }
    }
    public void MoveScene(string sceneName)
    {
        allManager.SaveAllProgress();

        if (SceneManager.GetActiveScene().name == "Village")
        {
            if (sceneName == "Merchant")
            {
                SceneManager.LoadScene("Shop");
                toWhatShop = "merchant";
            }
            else if (sceneName == "Blacksmith")
            {
                SceneManager.LoadScene("Shop");
                toWhatShop = "blacksmith";
            }
            else
            {
                SceneManager.LoadScene(sceneName);
            }
        }
        else SceneManager.LoadScene(sceneName);
    }
    public void Settings()
    {
        if (settings.activeSelf)
        {
            credit.SetActive(false);
            credit.GetComponent<SimpleFade>().Deactivate();            
            settings.SetActive(false);
        }
        else
        {
            settings.SetActive(true);
        }
    }
    public void AudioSetting()
    {
        if (music.volume == 0) music.volume = 1;
        else music.volume = 0;
    }
    public void Credits()
    {
        if (credit.activeSelf)
        {
            credit.GetComponent<SimpleFade>().Deactivate();
            credit.SetActive(false);
        }
        else
        {
            credit.SetActive(true);
            credit.GetComponent<SimpleFade>().Activate();
        }
    }    
}
