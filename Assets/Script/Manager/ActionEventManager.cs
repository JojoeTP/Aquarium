using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ActionEventManager : MonoBehaviour
{
    public static ActionEventManager inst;

    [Header("Labyrinth")]
    public Material labyrinthMapMaterial;

    private void Awake() 
    {
        inst = this;
    }

#region ItemActionEvent
    public void OnPickUpLabyrinthCoin()
    {
        labyrinthMapMaterial.SetInt("_HideMainTex",1); //เปลี่ยน texture
        //ปิดกำแพงด้วย
    }
#endregion

    public void CutSceneDoor()
    {
        print("CutSceneDoor");
    }

    public void CutSceneHidingSpot()
    {
        print("CutSceneHidingSpot");
    }

    public void TestSaveGame()
    {
        SaveGameSystemManager.inst.SaveGame();
    }
}

#if UNITY_EDITOR
    [CustomEditor(typeof(ActionEventManager))]
    public class ActionEventTester : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            ActionEventManager actionActive = (ActionEventManager)target;

            if (GUILayout.Button("Test Save Game"))
            {
                Debug.Log("SAVE COMPLETE");
                actionActive.TestSaveGame();
            }
        }
    }
#endif
