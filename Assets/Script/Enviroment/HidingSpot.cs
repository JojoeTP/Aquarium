using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HidingSpot : MonoBehaviour
{
    public UnityEvent triggerEvents;
    public void OnHidingEvent()
    {
        triggerEvents.Invoke();
    }
}
