using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITransition : MonoBehaviour
{
    public static UITransition inst;
    
    [SerializeField] DoorTransitionScript doorTransition;

    private void Awake() 
    {
        inst = this;    
    }

    public void DoorTransitionIn()
    {
        doorTransition.DoorTransitionIn();
    }
}
