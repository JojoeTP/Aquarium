using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RecordTimeManager : MonoBehaviour
{
    public static RecordTimeManager Inst;

    [HideInInspector]
    public float getInRoomTime = 0;

    public PickUpItemTime pickUpItemTime = new PickUpItemTime();

    private void Awake() 
    {
        Inst = this;   
    }

    public void GetPickUpItemTimeData(string key,float time)
    {
        pickUpItemTime.gettingItemTime.Add(key,time.ToString("0.00"));
        pickUpItemTime.SaveJSON(false);
    }
}

public class PickUpItemTime
{
    public Dictionary<string,string> gettingItemTime = new Dictionary<string, string>();

    public void SaveJSON(bool toStreamingAssets = false)
    {
        JSONHelper.SaveJSONObject("PickUpItemTimeData/PickUpItemTime.json",this,toStreamingAssets);
    }

    static public PickUpItemTime LoadJson()
    {
        //load when continue play game

        var pickUpItemTimeData = JSONHelper.LoadJSONAsObject<PickUpItemTime>("PickUpItemTimeData/PickUpItemTime.json");
        if (pickUpItemTimeData == null)
        {
            pickUpItemTimeData = new PickUpItemTime();
        }
        return pickUpItemTimeData;
    }
}