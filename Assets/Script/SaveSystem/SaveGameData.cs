using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class SaveGameData
{
    public int participateID;

    public List<float> position = new List<float>();
    
    public float playerStamina;

    public Dictionary<string,string> playerItem = new Dictionary<string, string>();

    public bool isMap1Done;
    public bool isMap2Done;
    public bool isMap3Done;
    public bool isMap4Done;


    //Item in inventory

    public void SaveJSON(bool toStreamingAssets = false)
    {
        JSONHelper.SaveJSONObject("GameData/SaveGameData.json",this,toStreamingAssets);
    }

    static public SaveGameData LoadSaveGameDataJSON()
    {
        var saveGameData = JSONHelper.LoadUserJSONAsObject<SaveGameData>("GameData/SaveGameData.json");
        if (saveGameData == null)
        {
            saveGameData = new SaveGameData();
        }
        return saveGameData;
    }

    public Vector3 GetPlayerPosition()
    {
        Vector3 newPos = new Vector3(position[0],position[1],position[2]);
        return newPos;
    }

    public float GetPlayerParticipateID()
    {
        return participateID;
    }

    public float GetPlayerStamina()
    {
        return playerStamina;
    }

    public Dictionary<string,string> GetPlayerItem()
    {
        return playerItem;
    }
    
    public bool IsMap1Done()
    {
        return isMap1Done;
    }

    public bool IsMap2Done()
    {
        return isMap2Done;
    }

    public bool IsMap3Done()
    {
        return isMap3Done;
    }

    public bool IsMap4Done()
    {
        return isMap4Done;
    }
}
