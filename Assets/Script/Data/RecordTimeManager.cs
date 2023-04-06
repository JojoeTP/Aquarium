using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif

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

    public void ClearPickUpItemTimeData()
    {
        PickUpItemTime _pickUpItemTime = new PickUpItemTime();

        _pickUpItemTime.SaveJSON("PickUpItemTimeData/PickUpItemTime.json",false);
    }

    public void SavePickUpItemTimeData(string key,ItemTimeData itemData)
    {
        pickUpItemTime.participateID = ItemManager.Inst.ParticipateId;
        if(!pickUpItemTime.gettingItemTime.TryAdd(key,itemData))
        {
            pickUpItemTime.gettingItemTime.Remove(key);
            pickUpItemTime.gettingItemTime.Add(key,itemData);
        }

        pickUpItemTime.SaveJSON("PickUpItemTimeData/PickUpItemTime.json",false);
    }

    public void LoadPickUpItemTimeData()
    {
        pickUpItemTime = PickUpItemTime.LoadJson();
    }

    public void OpenDataFolder()
    {
#if UNITY_EDITOR
        var filePath = Application.dataPath + "/../../Documents/Liberate/PickUpItemTimeData/PickUpItemTime.json";
        EditorUtility.RevealInFinder(filePath);
#else
        Application.OpenURL(Application.persistentDataPath + "/Liberate/PickUpItemTimeData/");
#endif
    }
}

public class PickUpItemTime
{
    public int participateID;
    public Dictionary<string,ItemTimeData> gettingItemTime = new Dictionary<string, ItemTimeData>();

    public void SaveJSON(string fileName,bool toStreamingAssets = false)
    {
        JSONHelper.SaveJSONObject(fileName,this,toStreamingAssets);
    }

    static public PickUpItemTime LoadJson()
    {
        //TODO: Maybe gonna use LoadJSONAsObject() if it doesn't works.
        var pickUpItemTimeData = JSONHelper.LoadUserJSONAsObject<PickUpItemTime>("PickUpItemTimeData/PickUpItemTime.json"); 
        if (pickUpItemTimeData == null)
        {
            pickUpItemTimeData = new PickUpItemTime();
        }
        return pickUpItemTimeData;
    }
}

public struct ItemTimeData
{
    public string effectName;
    public float time;

}