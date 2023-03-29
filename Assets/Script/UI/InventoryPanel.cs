using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using TMPro;

public class InventoryPanel : MonoBehaviour
{
    public Canvas Canvas {get {return canvas;}}
    Canvas canvas;

    [Header("ItemDetailPanel")]
    [SerializeField] Image itemDetail_Image;
    [SerializeField] TMP_Text itemDetail_Name;
    [SerializeField] TMP_Text itemDetail_Infomation;

    [Header("ItemSlotPanel")]
    [SerializeField] Transform itemSlotTemplate;
    [SerializeField] Transform itemSlotParent;

    
    void Awake()
    {
        canvas = GetComponent<Canvas>();        
    }

    void GenerateItemSlot()
    {
        ClearChildren(itemSlotParent);

        itemSlotTemplate.gameObject.SetActive(false);

        var playerItemDictionary = PlayerManager.inst.PlayerInventory.PlayerItemDictionary;

        for(int itemIndex = 0; itemIndex < playerItemDictionary.Count; itemIndex++)
        {
            var clone = Instantiate(itemSlotTemplate) as RectTransform;
            clone.SetParent(itemSlotParent,false);
            clone.gameObject.SetActive(true);
            clone.name = clone.name.Replace("*","");
            
            var _itemIndex = itemIndex;


            var itemData = ItemManager.Inst.GetItemByID(playerItemDictionary.ElementAt(_itemIndex).Value);

            clone.GetComponent<Toggle>().onValueChanged.AddListener((b) => 
            {
                if(b)
                {
                    OnClickItem(itemData);
                }
            });

            var itemImage = clone.Find("Image").GetComponent<Image>();

            if(itemData.InventoryItemSprite != null)
                itemImage.sprite = itemData.InventoryItemSprite;
            else
                itemImage.sprite = itemData.ItemSprite;
            
        }
    }

    public void OnOpenInventory()
    {
        HideItemDetail();
        GenerateItemSlot();
    }

    void HideItemDetail()
    {
        itemDetail_Image.gameObject.SetActive(false);
        itemDetail_Name.gameObject.SetActive(false);
        itemDetail_Infomation.gameObject.SetActive(false);
    }

    void OnClickItem(ItemData item)
    {
        if(item.InventoryItemSprite != null)
            itemDetail_Image.sprite = item.InventoryItemSprite;
        else
            itemDetail_Image.sprite = item.ItemSprite;
        
        itemDetail_Name.text = item.ItemName;
        itemDetail_Infomation.text = item.ItemDescription;

        itemDetail_Image.gameObject.SetActive(true);
        itemDetail_Name.gameObject.SetActive(true);
        itemDetail_Infomation.gameObject.SetActive(true);
    }

    void ClearChildren(Transform parent)
    {
        Transform child = null;
        for(int childIndex = 0; childIndex < parent.transform.childCount; childIndex++)
        {
            child = parent.transform.GetChild(childIndex);
            if(!child.name.StartsWith("*"))
            {
                Destroy(child.gameObject);
            }
        }
    }
}
