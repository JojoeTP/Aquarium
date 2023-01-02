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

    void Start()
    {

    }

    void Update()
    {
        
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


            var itemData = playerItemDictionary.ElementAt(_itemIndex).Value;

            clone.GetComponent<Toggle>().onValueChanged.AddListener((b) => 
            {
                if(b)
                {
                    OnClickItem(itemData);
                }
            });

            var itemImage = clone.Find("Image").GetComponent<Image>();
            itemImage.sprite = itemData.ItemSprite;
        }
        
    }

    public void OnOpenInventory()
    {
        GenerateItemSlot();
    }

    void OnClickItem(ItemData item)
    {
        itemDetail_Image.sprite = item.ItemSprite;
        itemDetail_Name.text = item.ItemName;
        itemDetail_Infomation.text = item.ItemDescription;
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
