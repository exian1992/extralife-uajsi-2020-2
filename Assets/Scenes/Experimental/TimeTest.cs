using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TimeTest : MonoBehaviour
{
    public TMPro.TextMeshProUGUI currentTime, timeCompare;
    public TMPro.TextMeshProUGUI endTime;
    public Button completeCheck;
    DateTime end;
    bool running;

    // Update is called once per frame
    void Update()
    {
        currentTime.text = DateTime.Now.ToString();

        if (DateTime.Now.Ticks >= end.Ticks)
        {
            running = false;
            completeCheck.interactable = true;
        }
        else completeCheck.interactable = false;

        if (running)
        {
            TimeSpan comparison = end - DateTime.Now;
            timeCompare.text = new DateTime(comparison.Ticks).ToString("HH:mm:ss");
        }        
    }

    public void SetTime()
    {
        running = true;
        end = DateTime.Now.AddSeconds(10);
        endTime.text = end.ToString();
    }
}
