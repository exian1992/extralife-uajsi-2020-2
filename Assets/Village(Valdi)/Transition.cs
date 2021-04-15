using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{
    public IdleManager iManager;
    public Animator transition;
    public float time = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }   
    // Update is called once per frame
    void Update()
    {
        /*if (CrossPlatformInputManager.GetButtonDown("Merchant"))
        {
            SceneManager.LoadScene("Shop");
        }*/
        if (CrossPlatformInputManager.GetButtonDown("Map"))
        {
            SceneManager.LoadScene("Map");
            iManager.SaveAllProgress();
        }
        if (iManager == null)
        {
            iManager = GameObject.Find("IdleManager").GetComponent<IdleManager>();
        }
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(time);

        SceneManager.LoadScene(levelIndex);

    }
}
