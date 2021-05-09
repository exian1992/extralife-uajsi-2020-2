using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX : MonoBehaviour
{
    public static AudioClip WaterfallSFX, CaveSFX, DeepCaveSFX;
    static AudioSource AudioSFX;

    // Start is called before the first frame update
    void Start()
    {
        WaterfallSFX = Resources.Load<AudioClip>("Waterfall SFX");
        CaveSFX = Resources.Load<AudioClip>("Cave SFX");
        DeepCaveSFX = Resources.Load<AudioClip>("Deep Cave SFX");

        AudioSFX = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
     
    }

    public static void PlaySound (string clip)
    {
        switch (clip)
        {
            case "Cave":
                AudioSFX.PlayOneShot(CaveSFX);
                break;
            case "Waterfall":
                AudioSFX.PlayOneShot(WaterfallSFX);
                break;
        }
    }
}
