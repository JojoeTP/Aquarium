using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        labyrinthMapMaterial.SetInt("_HideMainTex",1);
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
