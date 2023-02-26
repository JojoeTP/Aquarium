using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInventory : MonoBehaviour
{
    public Dictionary<string,string> PlayerItemDictionary {get {return playerItemDictionary;} set {playerItemDictionary = value;}}
    Dictionary<string,string> playerItemDictionary = new Dictionary<string, string>();
    
    public void AddItem(ItemScriptableObject item)
    {
        playerItemDictionary.Add(item.name,item.itemData.ItemID);
    }
}
