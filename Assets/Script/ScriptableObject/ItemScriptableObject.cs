using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemData
{
    public string Id;
    public string ItemName;
    public string ItemDescription;
    public Sprite ItemSprite;
}

[CreateAssetMenu(fileName = "New Item" , menuName = "Item")]
public class ItemScriptableObject : ScriptableObject
{
    public ItemData itemData;
    public int pageNumber;
}
