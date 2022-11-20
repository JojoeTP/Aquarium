using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ItemEffectJsonManager))]
public class ItemEffectJsonEditor : Editor
{
    public override void OnInspectorGUI() 
    {
        base.DrawDefaultInspector();

        if (GUILayout.Button("Save Item Effect Data")) 
        {
            ItemEffectJsonManager itemEffectManager = (ItemEffectJsonManager)target;

            itemEffectManager.Save();
            Debug.Log("Save");
        }

        if (GUILayout.Button("Load Item Effect Data")) 
        {
            ItemEffectJsonManager itemEffectManager = (ItemEffectJsonManager)target;

            itemEffectManager.testLoadItem = ItemEffectInfo.LoadItemEffectJSON(itemEffectManager.index);
            Debug.Log("load");
        }
    }

    
}

