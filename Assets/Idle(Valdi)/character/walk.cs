using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.EventSystems;

public class walk : MonoBehaviour
{
    public float speed;
    public Animator anim;
    public GameObject menu;

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("isWalk", true);
        transform.Translate(2 * Time.deltaTime * speed, 0, 0);

        if (Input.GetMouseButtonDown(0) && EventSystem.current.currentSelectedGameObject == null)
        {
            anim.SetBool("Down", false);
        }
        else
        {
            anim.SetBool("Down", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.CompareTag("stopPoint"))
        {
            anim.SetBool("isWalk", false);
            speed = 0;
            anim.SetBool("Mine", true);
        }
    }

    
}
