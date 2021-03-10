using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{

    public Animator transition;
    public float time = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (CrossPlatformInputManager.GetButtonDown("Merchant"))
        {
           // LoadNextLevel();
            SceneManager.LoadScene("Shop(UI)");
        }

        if (CrossPlatformInputManager.GetButtonDown("Village"))
        {
            SceneManager.LoadScene("Village");
        }
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene("Shop(UI)");
        //StartCoroutine(LoadLevel("Shop(UI)"));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(time);

        SceneManager.LoadScene(levelIndex);

    }
}
