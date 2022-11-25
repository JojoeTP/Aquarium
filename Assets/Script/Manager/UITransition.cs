using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITransition : MonoBehaviour
{
    public static UITransition inst;
    
    [SerializeField] Animator overlayTransition;

    private void Awake() 
    {
        inst = this;    
    }

    public void PlayOverlayTransitionIn()
    {
        overlayTransition.SetTrigger("TransitionIn");
    }
    
    public void PlayOverlayTransitionOut()
    {
        overlayTransition.SetTrigger("TransitionOut");
    }
}
