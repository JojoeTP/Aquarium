using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HidingSpot : MonoBehaviour
{
    public UnityEvent EntertriggerEvents;
    public UnityEvent triggerEvents;

    [SerializeField] SpriteRenderer HidingSpotObject;
    [SerializeField] Sprite HidingSprite;

    Sprite beforeSprite;
    bool isHiding;
    public void OnEnterHidingEvent()
    {
        EntertriggerEvents.Invoke();
    }

    public void OnHidingEvent()
    {
        triggerEvents.Invoke();
    }

    public void ChangeSprite()
    {
        if(HidingSpotObject != null)
        {
            if(!isHiding)
            {
                beforeSprite = HidingSpotObject.sprite;
                HidingSpotObject.sprite = HidingSprite;
                isHiding = true;

            }
            else
            {
                HidingSpotObject.sprite = beforeSprite;
                isHiding = false;
            }
        }
    }
}
