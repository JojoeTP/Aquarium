using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ActionEventManager : MonoBehaviour
{
    public static ActionEventManager inst;

    [Header("Puzzle")]
    [SerializeField] SpriteRenderer labyrinthENDSpriteRenderer;
    [SerializeField] Sprite dark_LabyrinthSprite;
    [HideInInspector]
    public bool isPuzzleDone = false;
    

    void Awake() 
    {
        inst = this;
    }

#region ItemActionEvent
    public void OnPickUpLabyrinthCoin()
    {
        isPuzzleDone = true;
        labyrinthENDSpriteRenderer.sprite = dark_LabyrinthSprite;
        //ปิดเสียงด้วย
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


            if (GUILayout.Button("Test Labyrinth END"))
            {
                actionActive.OnPickUpLabyrinthCoin();
            }
        }
    }
#endif
