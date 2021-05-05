using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleFade : MonoBehaviour
{
    // the image you want to fade, assign in inspector
    public Image credit;

    public void Activate()
    {
        StartCoroutine(FadeImage());
    }
    public void Deactivate()
    {
        credit.color = new Color(255, 255, 255, 0);
    }
    IEnumerator FadeImage()
    {
        for (float i = 0; i <= 1; i += Time.deltaTime)
        {
            credit.color = new Color(255, 255, 255, i);
            yield return null;
        }
    }
    
}
