using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "HireNPC")]
[System.Serializable]
public class HireNPC : ScriptableObject
{
    public string npcName;
    public string assignToMap;

    public bool completion;
    //time management
    //timeEnd

    //function to refresh time between present(System.DateTime.Now) and timeEnd(time) for checking the completion(bool)
    //example to get time data + example function
    //var over = DateTime.Parse("2019/04/01 17:09:10") <-- timeEnd should be a string, i think, for the parameter

    //if (dateAndTimeVar >= over)
    //{
        //print(dateAndTimeVar);
    //}
}
