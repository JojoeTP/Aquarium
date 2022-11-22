using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField] bool isPermutation = false;
    [SerializeField] int participateID;
    ItemEffectInfo itemEffectData;

    [Header("ItemEffect")]
    [SerializeField] Item winkEffectPrefab;
    [SerializeField] Item invertColorEffectPrefab;
    [SerializeField] Item lightEffectPrefab;
    [SerializeField] Item outlineEffectPrefab;
    [SerializeField] Item buttonEffectPrefab;

    [SerializeField] List<ItemScriptableObject> itemData = new List<ItemScriptableObject>();

    void Start()
    {
        if(isPermutation)
            SetUpItemPermutation();
    }

    void Update()
    {
        
    }

    // void SetUpItem()
    // {
    //     foreach(var n in itemData)
    //     {
    //         Item item = Instantiate(items[RandomItemEffect()],n.itemData.ItemPosition,Quaternion.identity);
    //         item.item = n;
    //         item.SetSprite();
    //     }
    // }

    void SetUpItemPermutation()
    {
        Item item = null;
        LoadItemDataJson();

        foreach(var n in itemEffectData.ItemEffectSettingList)
        {
            switch(n.effectTYPE)
            {
                case EFFECTTYPE.WINK :
                    foreach(var m in itemData)
                    {
                        if(n.iTEMTYPE.HasFlag(m.itemData.ItemType))
                        {
                            item = Instantiate(winkEffectPrefab);
                            item.item = m;
                            item.SetItem();
                        }
                    }
                    break;
                case EFFECTTYPE.INVERTCOLOR :
                    foreach(var m in itemData)
                    {
                        if(n.iTEMTYPE.HasFlag(m.itemData.ItemType))
                        {
                            item = Instantiate(invertColorEffectPrefab);
                            item.item = m;
                            item.SetItem();
                        }
                    }
                    break;
                case EFFECTTYPE.LIGHT :
                    foreach(var m in itemData)
                    {
                        if(n.iTEMTYPE.HasFlag(m.itemData.ItemType))
                        {
                            item = Instantiate(lightEffectPrefab);
                            item.item = m;
                            item.SetItem();
                        }
                    }
                    break;
                case EFFECTTYPE.OUTLINE :
                    foreach(var m in itemData)
                    {
                        if(n.iTEMTYPE.HasFlag(m.itemData.ItemType))
                        {
                            item = Instantiate(outlineEffectPrefab);
                            item.item = m;
                            item.SetItem();
                        }
                    }
                    break;
                case EFFECTTYPE.BUTTON :
                    foreach(var m in itemData)
                    {
                        if(n.iTEMTYPE.HasFlag(m.itemData.ItemType))
                        {
                            item = Instantiate(buttonEffectPrefab);
                            item.item = m;
                            item.SetItem();
                        }
                    }
                    break;
            }

            SetItemAction(item);
        }


    }

    void LoadItemDataJson()
    {
        itemEffectData = ItemEffectInfo.LoadItemEffectJSON(participateID);
    }

    void SetItemAction(Item item)
    {
        switch (item.item.itemData.ItemType)
        {
            case ITEMTYPE.ITEM1:
                item.triggerEvents.AddListener( () => 
                    {

                    }
                );
                break;
            case ITEMTYPE.ITEM2:
                item.triggerEvents.AddListener( () => 
                    {

                    }
                );
                break;
            case ITEMTYPE.ITEM3:
                item.triggerEvents.AddListener( () => 
                    {

                    }
                );
                break;
            case ITEMTYPE.ITEM4:
                item.triggerEvents.AddListener( () => 
                    {

                    }
                );
                break;
            case ITEMTYPE.ITEM5:
                item.triggerEvents.AddListener( () => 
                    {

                    }
                );
                break;
            case ITEMTYPE.ITEM6:
                item.triggerEvents.AddListener( () => 
                    {

                    }
                );
                break;
            case ITEMTYPE.ITEM7:
                item.triggerEvents.AddListener( () => 
                    {

                    }
                );
                break;
            case ITEMTYPE.ITEM8:
                item.triggerEvents.AddListener( () => 
                    {

                    }
                );
                break;
            case ITEMTYPE.ITEM9:
                item.triggerEvents.AddListener( () => 
                    {

                    }
                );
                break;
            case ITEMTYPE.ITEM10:
                item.triggerEvents.AddListener( () => 
                    {

                    }
                );
                break;
        }
    }

    // int RandomItemEffect()
    // {
    //     var randNum = Random.RandomRange(0,items.Count);
    //     return randNum;
    // }   

}
