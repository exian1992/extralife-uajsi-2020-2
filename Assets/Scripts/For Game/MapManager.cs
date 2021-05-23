using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour
{
    AllManager allManager;
    IdleManager iManager;

    public string toWhatShop;
    public GameObject[] managerChecker, musicChecker;
    public TMPro.TextMeshProUGUI coin;
    public GameObject settings;
    public AudioSource music;

    public GameObject credit;
    
    private void Start()
    {
        Application.targetFrameRate = -1;

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

        coin = GameObject.Find("CoinText").GetComponent<TMPro.TextMeshProUGUI>();
        coin.text = iManager.coin.ToString();

        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                allManager.SaveAllProgress();

                if (SceneManager.GetActiveScene().name == "Waterfall" ||
                    SceneManager.GetActiveScene().name == "Cave" ||
                    SceneManager.GetActiveScene().name == "DeepCave" ||
                    SceneManager.GetActiveScene().name == "EarthMantle" ||
                    SceneManager.GetActiveScene().name == "DwarfVillage")
                {                    
                    SceneManager.LoadScene("Map");
                }
                else if (SceneManager.GetActiveScene().name == "Village")
                {
                    //"are u sure?" thing
                    Application.Quit();
                }
                else if (SceneManager.GetActiveScene().name == "Shop" ||
                         SceneManager.GetActiveScene().name == "Map" ||
                         SceneManager.GetActiveScene().name == "VillageChief")
                {                    
                    SceneManager.LoadScene("Village");
                }             
            }
        }

        //setting menu close when clicked outside setting area
        if (Input.GetMouseButtonUp(0) && 
            (EventSystem.current.currentSelectedGameObject == null || 
            !(EventSystem.current.currentSelectedGameObject.name == "audio" || 
            EventSystem.current.currentSelectedGameObject.name == "googlePlay" || 
            EventSystem.current.currentSelectedGameObject.name == "credits" ||
            EventSystem.current.currentSelectedGameObject.name == "settingArea" ||
            EventSystem.current.currentSelectedGameObject.name == "Setting")))
        {
            credit.GetComponent<SimpleFade>().Deactivate();
            credit.SetActive(false);
            settings.SetActive(false);
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
