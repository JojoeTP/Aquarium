using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveGameSystemManager : MonoBehaviour
{
    public static SaveGameSystemManager inst;

    public SaveGameData gameData;

    public bool isLoad {get; private set;}

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
        gameData = new SaveGameData();
    }

    public void SaveGame()
    {
        SaveGameData newSaveGameData = new SaveGameData();
        newSaveGameData.participateID = ItemManager.Inst.ParticipateId;
        newSaveGameData.position = GetPlayerPositionTypeListOfFloat();
        newSaveGameData.playerStamina = PlayerManager.inst.PlayerMovement.PlayerStamina;
        newSaveGameData.playerItem = GetPlayerItemInInventoryTypeOfDic();

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
}
