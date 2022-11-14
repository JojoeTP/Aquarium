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
        Item item;
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
        }
    }

    void LoadItemDataJson()
    {
        itemEffectData = ItemEffectInfo.LoadItemEffectJSON(participateID);
    }

    // int RandomItemEffect()
    // {
    //     var randNum = Random.RandomRange(0,items.Count);
    //     return randNum;
    // }   
}
