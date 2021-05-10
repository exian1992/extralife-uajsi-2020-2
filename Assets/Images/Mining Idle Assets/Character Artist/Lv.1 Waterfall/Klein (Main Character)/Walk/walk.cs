using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.EventSystems;

public class walk : MonoBehaviour
{
    private float speed = 30f;
    public Animator anim;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(2 * Time.deltaTime * speed, 0, 0);

        if (Input.GetMouseButtonDown(0) && EventSystem.current.currentSelectedGameObject == null && anim.GetBool("Mine") == false)
        {
            SFX.PlaySound("Waterfall");
            anim.SetBool("Mine", true);
        }
        else
        {
            anim.SetBool("Mine", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.CompareTag("stopPoint"))
        {
            anim.SetBool("Down", true);            
            speed = 0;
            anim.SetBool("Idle", true);
        }
    }    
}
