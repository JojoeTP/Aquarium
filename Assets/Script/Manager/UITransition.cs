using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITransition : MonoBehaviour
{
    public static UITransition inst;
    
    [SerializeField] DoorTransitionScript doorTransition;
    [SerializeField] CutSceneTransitionScript cutSceneTransition;

    private void Awake() 
    {
        inst = this;    
    }

    public void DoorTransitionIn()
    {
        doorTransition.DoorTransitionIn();
    }

    public void CutSceneTransitionIn()
    {
        cutSceneTransition.CutSceneTransitionIn();
    }

    public void LiftTransitionIn()
    {
        doorTransition.LiftTransitionIn();
    }
}
