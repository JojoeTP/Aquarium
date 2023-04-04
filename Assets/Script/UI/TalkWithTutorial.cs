using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TalkWithTutorial : MonoBehaviour
{
    public UnityEvent triggerEvents;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponent<PlayerManager>() != null){
            triggerEvents.Invoke();
        }
    }
}
