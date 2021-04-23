using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour
{
    public IdleManager iManager;
    public string toWhatShop;
    public GameObject[] managerChecker;
    public Text coin;
    private void Start()
    {
        iManager = GameObject.Find("IdleManager").GetComponent<IdleManager>();

        if (!(SceneManager.GetActiveScene().name == "Shop"))
        {
            managerChecker = GameObject.FindGameObjectsWithTag("mManager"); //mManager = mapManager
            if (managerChecker.Length == 2)
            {
                Destroy(managerChecker[0]);
                DontDestroyOnLoad(managerChecker[1]);
            }
            else
            {
                DontDestroyOnLoad(managerChecker[0]);
            }
        }
    }
    private void Update()
    {
        coin = GameObject.Find("CoinText").GetComponent<Text>();
        coin.text = iManager.coin.ToString();

        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (SceneManager.GetActiveScene().name == "Waterfall" ||
                    SceneManager.GetActiveScene().name == "Cave" ||
                    SceneManager.GetActiveScene().name == "DeepCave")
                {
                    //"are u sure?" thing
                    Application.Quit();
                }
                else if (SceneManager.GetActiveScene().name == "Village")
                {
                    SceneManager.LoadScene("Map");
                }
                else if (SceneManager.GetActiveScene().name == "Shop")
                {
                    iManager.SaveAllProgress();
                    SceneManager.LoadScene("Village");
                }
                else if (SceneManager.GetActiveScene().name == "Map")
                {
                    SceneManager.LoadScene(iManager.lastMap);
                }
            }
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
        else if (SceneManager.GetActiveScene().name == "Waterfall" ||
                 SceneManager.GetActiveScene().name == "Cave" ||
                 SceneManager.GetActiveScene().name == "DeepCave")
        {
            iManager.lastMap = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(sceneName);
        }
        else SceneManager.LoadScene(sceneName);
    }
}
