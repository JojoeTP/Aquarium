using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInventory : MonoBehaviour
{
    public Dictionary<string,ItemData> PlayerItemDictionary {get {return playerItemDictionary;}}
    Dictionary<string,ItemData> playerItemDictionary = new Dictionary<string, ItemData>();
    
    public void AddItem(ItemScriptableObject item)
    {
        playerItemDictionary.Add(item.name,item.itemData);
    }
}
