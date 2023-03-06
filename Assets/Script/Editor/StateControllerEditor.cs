using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(StateController))]
public class StateControllerEditor : Editor
{
    public void OnSceneGUI()
    {
        var t = target as StateController;
        var tr = t.transform.position;
        var offset = t.stateLableOffset;
        string stateName;
        if(t.currentState == null)
            stateName = "null";
        else
            stateName = t.currentState.name;
        
        Handles.Label(tr + offset,stateName);
    }
}
