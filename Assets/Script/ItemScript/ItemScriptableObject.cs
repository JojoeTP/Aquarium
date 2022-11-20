using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemData
{
    public string ItemName;
    public string ItemDescription;
    public Sprite ItemSprite;
    public Vector3 ItemPosition;
    public Vector3 ItemScale;
    public ITEMTYPE ItemType;
}

[CreateAssetMenu(fileName = "New Item" , menuName = "Item")]
public class ItemScriptableObject : ScriptableObject
{
    public ItemData itemData;
    public int pageNumber;
}
