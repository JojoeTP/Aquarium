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
    AIRedHoodTransitionScript redHoodTransitionScript;
    AIJunitorTransitionScript junitorTransitionScript;

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
        redHoodTransitionScript = transitionScript.GetComponent<AIRedHoodTransitionScript>(); 
        junitorTransitionScript = transitionScript.GetComponent<AIJunitorTransitionScript>(); 
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
        redHoodTransitionScript.AIRedHoodTransitionIn();
    }

    public void JunitorTransitionIn()
    {
        junitorTransitionScript.AIJunitorTransitionIn();
    }
    
    public void WhiteTransitionIn()
    {
        cutSceneTransition.WhiteTransitionIn();
    }
}
