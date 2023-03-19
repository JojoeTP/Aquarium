using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITransition : MonoBehaviour
{
    public static UITransition inst;
    
    [SerializeField] DoorTransitionScript doorTransition;
    [SerializeField] CutSceneTransitionScript cutSceneTransition;
    [SerializeField] DieTransitionScript dieTransitionScript;

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

    public void DieTransitionIn()
    {
        dieTransitionScript.DieTransitionIn();
    }
}
