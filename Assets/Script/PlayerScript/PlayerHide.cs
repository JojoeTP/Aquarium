using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHide : MonoBehaviour
{
    [Header("Hiding Setting")]
    public bool isHide = false;
    public float triggerRadius;

    bool CanHide()
    {
        foreach(var n in Physics2D.OverlapCircleAll(transform.position,triggerRadius))
        {
            if(n.GetComponent<HidingSpot>() != null)
                return true;
        }
        return false;
    }

    public void ToggleHiding()
    {
        if(!isHide)
        {
            CanHide();
            isHide = true;
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
        }
        else if(isHide)
        {
            isHide = false;
            GetComponent<SpriteRenderer>().enabled = true;
            GetComponent<Collider2D>().enabled = true;
        } 
    }

    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position,triggerRadius);    
    }
}
