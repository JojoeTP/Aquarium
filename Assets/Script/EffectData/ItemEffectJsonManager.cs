using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEffectJsonManager : MonoBehaviour
{
    public List<ItemEffectInfo> ItemEffectList = new List<ItemEffectInfo>();

    public int index = 0;
    public ItemEffectInfo testLoadItem = new ItemEffectInfo();

    public void Save()
    {
        for(int i = 0; i < ItemEffectList.Count;i++)
        {
            ItemEffectList[i].SaveJSON("EffectData/ItemEffectData" + i +".json",true);
        }
    }
    
}

[Serializable]
public class ItemEffectInfo
{
    public List<ItemEffectSetting> ItemEffectSettingList = new List<ItemEffectSetting>();

    public void SaveJSON(string filename, bool toStreamingAssets = false)
    {
        JSONHelper.SaveJSONObject(filename,this,toStreamingAssets);
    }

    static public ItemEffectInfo LoadItemEffectJSON(int index)
    {
        var itemEffectInfo = JSONHelper.LoadJSONAsObject<ItemEffectInfo>("EffectData/ItemEffectData" + index +".json");
        if (itemEffectInfo == null)
        {
            itemEffectInfo = new ItemEffectInfo();
        }
        return itemEffectInfo;
    }

}

[Serializable]
public struct ItemEffectSetting
{
    public EFFECTTYPE effectTYPE;
    public ITEMTYPE iTEMTYPE;

}

[Flags]
public enum ITEMTYPE
{
    None = 0,
    ITEM1 = 1, 
    ITEM2 = 2,
    ITEM3 = 4,
    ITEM4 = 8,
    ITEM5 = 16,
    ITEM6 = 32,
    ITEM7 = 64,
    ITEM8 = 128,
    ITEM9 = 256,
    ITEM10 = 512,
}

public enum EFFECTTYPE
{
    WINK,
    INVERTCOLOR,
    BUTTON,
    LIGHT,
    OUTLINE,
}
