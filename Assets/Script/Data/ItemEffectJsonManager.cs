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
