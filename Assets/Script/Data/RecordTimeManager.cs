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

#if UNITY_EDITOR
        _pickUpItemTime.SaveJSON("PickUpItemTimeData/PickUpItemTime.json",false);
#else
        _pickUpItemTime.SaveJSON("PickUpItemTimeData/PickUpItemTime.json",false);
#endif
    }

    public void SavePickUpItemTimeData(string key,ItemTimeData itemData)
    {
        pickUpItemTime.participateID = ItemManager.Inst.ParticipateId;
        pickUpItemTime.gettingItemTime.Add(key,itemData);

#if UNITY_EDITOR
        pickUpItemTime.SaveJSON("PickUpItemTimeData/PickUpItemTime.json",false);
#else
        pickUpItemTime.SaveJSON("PickUpItemTimeData/PickUpItemTime.json",false);
#endif

    }

    public void OpenDataFolder()
    {
#if UNITY_EDITOR
        var filePath = Application.dataPath + "/../../Documents/Aquarium/PickUpItemTimeData/PickUpItemTime.json";
        EditorUtility.RevealInFinder(filePath);
#else
        Application.OpenURL(Application.persistentDataPath + "/Aquarium/PickUpItemTimeData/");
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
        //load when continue play game

        var pickUpItemTimeData = JSONHelper.LoadJSONAsObject<PickUpItemTime>("PickUpItemTimeData/PickUpItemTime.json");
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