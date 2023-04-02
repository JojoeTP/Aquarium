using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveGameSystemManager : MonoBehaviour
{
    public static SaveGameSystemManager inst;

    public SaveGameData gameData;

    public bool isLoad;

    void Awake() 
    {
        if(inst != null)
            return;

        inst = this;
    }

    void Start()
    {
        
    }

    public void StartNewGame()
    {
        isLoad = false;
        PlayerPrefs.SetInt("IsSaved",0);
        gameData = new SaveGameData();
    }

    public void SaveGame()
    {
        SaveGameData newSaveGameData = new SaveGameData();
        newSaveGameData.participateID = ItemManager.Inst.ParticipateId;
        newSaveGameData.position = GetPlayerPositionTypeListOfFloat();
        newSaveGameData.playerStamina = PlayerManager.inst.PlayerMovement.PlayerStamina;
        newSaveGameData.playerItem = GetPlayerItemInInventoryTypeOfDic();

        newSaveGameData.isMap1Done = ActionEventManager.inst.isMap1Done;
        newSaveGameData.isMap2Done = ActionEventManager.inst.isMap2Done;
        newSaveGameData.isMap3Done = ActionEventManager.inst.isMap3Done;
        newSaveGameData.isMap4Done = ActionEventManager.inst.isMap4Done;

        PlayerPrefs.SetInt("IsSaved",1);
        newSaveGameData.SaveJSON();
    }

    public void LoadGame()
    {
        isLoad = true;

        gameData = SaveGameData.LoadSaveGameDataJSON();
        RecordTimeManager.Inst.LoadPickUpItemTimeData();
        
    }

    

    List<float> GetPlayerPositionTypeListOfFloat()
    {  
        var playerPostionList = new List<float>();
        var playerPostion = PlayerManager.inst.transform.position;

        playerPostionList.Add(playerPostion.x);
        playerPostionList.Add(playerPostion.y);
        playerPostionList.Add(playerPostion.z);

        return playerPostionList;
    }

    Dictionary<string,string> GetPlayerItemInInventoryTypeOfDic()
    {
        var playerItemDictionary = new Dictionary<string, string>();

        foreach(var item in PlayerManager.inst.PlayerInventory.PlayerItemDictionary)
        {
            playerItemDictionary.Add(item.Key,item.Value);
        }

        return playerItemDictionary;
    }

    public void SetIsMapDone()
    {
        ActionEventManager.inst.isMap1Done = gameData.isMap1Done;
        ActionEventManager.inst.isMap2Done = gameData.isMap2Done;
        ActionEventManager.inst.isMap3Done = gameData.isMap3Done;
        ActionEventManager.inst.isMap4Done = gameData.isMap4Done;
    }
}
