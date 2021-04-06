using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour
{
    GameObject allManager;
    IdleManager iManager;

    private void Start()
    {
        allManager = GameObject.Find("AllManager");
        iManager = GameObject.Find("IdleManager").GetComponent<IdleManager>();
    }
    public void Map1()
    {
        SceneManager.LoadScene("Idle");
        Destroy(allManager);
    }
    public void GoToVillage()
    {
        SceneManager.LoadScene("Village");
        DontDestroyOnLoad(allManager);
    }
}
