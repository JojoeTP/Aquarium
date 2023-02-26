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
        //GetAll data to game data;
        SaveGameData newSaveGameData = new SaveGameData();
        newSaveGameData.participateID = ItemManager.Inst.ParticipateId;
        newSaveGameData.position = GetPlayerPositionTypeListOfFloat();
        newSaveGameData.playerStamina = PlayerManager.inst.PlayerMovement.PlayerStamina;
        newSaveGameData.playerItem = GetPlayerItemInInventoryTypeOfDic();

        newSaveGameData.SaveJSON();
    }
    
    public void LoadGame()
    {
        isLoad = true;

        gameData = SaveGameData.LoadSaveGameDataJSON();
        RecordTimeManager.Inst.LoadPickUpItemTimeData();

        //โหลดมาเเล้วก็ นำ gamedata ที่ได้ ไปใช้ทำอะไรต่อก็ได้ แบบ set ค่าให้ player อะไรพวกนั้น
        //หลักๆตอนนี้คง เปลี่ยนตำแหน่งผู้เล่น
        //เปลี่ยน stamina คนเล่น
        //add item เข้าใน dictonary ของผู้เล่น
        //add เสร็จเเล้วก็กดปิดไอเทมนั้น

        // ดึงค่าได้จาก
        // gameData.GetPlayerItem();
        // gameData.GetPlayerParticipateID();
        // gameData.GetPlayerPosition();
        // gameData.GetPlayerStamina();
        
        //พวก dic หรือ ค่า เปลี่ยนได้เลยนะถ้าจะแก้ แก้ได้เลย เผื่อถ้าเปลี่ยน dic เป็น list เเล้วใช้ได้ดีกว่า
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
