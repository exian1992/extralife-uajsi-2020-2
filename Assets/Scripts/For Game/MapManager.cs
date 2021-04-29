using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour
{
    public IdleManager iManager;
    public string toWhatShop;
    public GameObject[] managerChecker, musicChecker;
    public Text coin;
    public GameObject settings;
    public AudioSource music;
    
    private void Start()
    {
        Application.targetFrameRate = -1;

        iManager = GameObject.Find("IdleManager").GetComponent<IdleManager>();

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

        //music gameobject check
        musicChecker = GameObject.FindGameObjectsWithTag("audio");
        if (musicChecker.Length == 2)
        {
            Destroy(musicChecker[1]);
            DontDestroyOnLoad(musicChecker[0]);
        }
        else
        {
            DontDestroyOnLoad(musicChecker[0]);
        }

        music = GameObject.Find("Music").GetComponent<AudioSource>();
    }
    private void Update()
    {
        coin = GameObject.Find("CoinText").GetComponent<Text>();
        coin.text = iManager.coin.ToString();

        //if (Application.platform == RuntimePlatform.Android)
        //{
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (SceneManager.GetActiveScene().name == "Waterfall" ||
                    SceneManager.GetActiveScene().name == "Cave" ||
                    SceneManager.GetActiveScene().name == "DeepCave")
                {                    
                    SceneManager.LoadScene("Map");
                }
                else if (SceneManager.GetActiveScene().name == "Village")
                {
                    //"are u sure?" thing
                    Application.Quit();
                }
                else if (SceneManager.GetActiveScene().name == "Shop")
                {
                    iManager.SaveAllProgress();
                    SceneManager.LoadScene("Village");
                }
                else if (SceneManager.GetActiveScene().name == "Map")
                {
                    SceneManager.LoadScene("Village");
                }
            }
        //}

        //setting menu close when clicked outside setting area
        if (Input.GetMouseButtonUp(0) && 
            (EventSystem.current.currentSelectedGameObject == null || 
            !(EventSystem.current.currentSelectedGameObject.name == "audio" || 
            EventSystem.current.currentSelectedGameObject.name == "googlePlay" || 
            EventSystem.current.currentSelectedGameObject.name == "credits" ||
            EventSystem.current.currentSelectedGameObject.name == "settingArea" ||
            EventSystem.current.currentSelectedGameObject.name == "Setting")))
        {
            settings.SetActive(false);
        }
    }
    public void MoveScene(string sceneName)
    {
        iManager = GameObject.Find("IdleManager").GetComponent<IdleManager>();
        iManager.SaveAllProgress();

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
}
