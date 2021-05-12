using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TimeTest : MonoBehaviour
{
    public TMPro.TextMeshProUGUI currentTime;
    public TMPro.TextMeshProUGUI[] endTime;
    public Button completeCheck;
    DateTime end;

    // Update is called once per frame
    void Update()
    {
        currentTime.text = DateTime.Now.ToString();

        if (DateTime.Now.Ticks >= end.Ticks)
        {

            completeCheck.interactable = true;
        }
        else completeCheck.interactable = false;

    }

    public void SetTime()
    {
        end = DateTime.Now.AddSeconds(10);
        endTime[0].text = end.ToString();
        endTime[1].text = end.Ticks.ToString();
    }
}
