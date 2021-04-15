using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour
{
    public string toWhatShop;
    public GameObject[] managerChecker;
    private void Start()
    {
        managerChecker = GameObject.FindGameObjectsWithTag("mManager");
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
    public void Waterfall()
    {
        SceneManager.LoadScene("Waterfall");
        Destroy(managerChecker[0]);
    }
    public void Cave()
    {
        SceneManager.LoadScene("Cave");
        Destroy(managerChecker[0]);
    }
    public void Village()
    {
        SceneManager.LoadScene("Village");
    }
    public void Merchant()
    {
        SceneManager.LoadScene("Shop");
        toWhatShop = "merchant";
    }
    public void Blacksmith()
    {
        SceneManager.LoadScene("Shop");
        toWhatShop = "blacksmith";
    }
}
