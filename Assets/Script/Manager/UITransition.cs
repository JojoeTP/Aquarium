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

    public void PlayOverlayTransition()
    {
        overlayTransition.SetTrigger("FadeInOut");
    }
}
