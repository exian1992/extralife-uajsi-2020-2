using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class Mining : MonoBehaviour
{

    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))//CrossPlatformInputManager.GetButtonDown("Mine"))
        {
            anim.SetBool("Mine", true);
        }
        else
        {
            anim.SetBool("Mine", false);
        }
    }
}
