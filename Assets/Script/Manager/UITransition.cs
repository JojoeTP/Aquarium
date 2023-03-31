using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITransition : MonoBehaviour
{
    public static UITransition inst;
    
    [SerializeField] GameObject transitionScript;
    DoorTransitionScript doorTransition;
    CutSceneTransitionScript cutSceneTransition;
    DieTransitionScript dieTransitionScript;
    AIMermaidTransitionScript mermaidTransitionScript;
    AIDirectorTransitionScript directorTransitionScript;

    private void Awake() 
    {
        inst = this;    
    }

    void Start() 
    {
        doorTransition = transitionScript.GetComponent<DoorTransitionScript>(); 
        cutSceneTransition = transitionScript.GetComponent<CutSceneTransitionScript>(); 
        dieTransitionScript = transitionScript.GetComponent<DieTransitionScript>(); 
        mermaidTransitionScript = transitionScript.GetComponent<AIMermaidTransitionScript>(); 
        directorTransitionScript = transitionScript.GetComponent<AIDirectorTransitionScript>(); 
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

    public void MermaidTransitionIn()
    {
        mermaidTransitionScript.AIMermaidTransitionIn();
    }

    public void DirectorTransitionIn()
    {
        directorTransitionScript.AIDirectorTransitionIn();
    }

    public void RedHoodTransitionIn()
    {
        // directorTransitionScript.AIDirectorTransitionIn();
    }

    public void JunitorTransitionIn()
    {
        // directorTransitionScript.AIDirectorTransitionIn();
    }
    
    public void WhiteTransitionIn()
    {
        cutSceneTransition.WhiteTransitionIn();
    }
}
