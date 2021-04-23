using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class Mining : MonoBehaviour
{

    public Animator anim;
    public GameObject menu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !(menu.activeSelf))//CrossPlatformInputManager.GetButtonDown("Down"))
        {
            anim.SetBool("Mine", false);
            anim.SetBool("Mine", true);
        }
        else if (Input.GetMouseButtonDown(0) && menu.activeSelf)
        {
            anim.SetBool("Mine", false);
        }
        else
        {
            anim.SetBool("Mine", false);
        }
    }
}
