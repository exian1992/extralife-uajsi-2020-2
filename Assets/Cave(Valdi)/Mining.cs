using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.EventSystems;

public class Mining : MonoBehaviour
{
    public Animator anim;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && EventSystem.current.currentSelectedGameObject == null)
        {
            anim.SetBool("Mine", true);
        }
        else
        {
            anim.SetBool("Mine", false);
        }
    }
}
