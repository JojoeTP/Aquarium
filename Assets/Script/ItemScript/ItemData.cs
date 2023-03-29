using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemData
{
    public string ItemID;
    public string ItemName;
    public string ItemDescription;
    public Sprite ItemSprite;
    public Sprite InventoryItemSprite;
    public Vector3 ItemPosition;
    public Vector3 ItemScale;
    public ITEMTYPE ItemType;
    public string dialogueItemId;
}
