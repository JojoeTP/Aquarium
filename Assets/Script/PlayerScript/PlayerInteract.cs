using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [Header("Hiding Setting")]
    public float InteractRadius;

    [Header("Door")]
    Transform warpPos;

    public void Interacting()
    {
        if(CanHiding())
            ToggleHiding();
        
        if(CanEnterDoor())
        EnterDoor(warpPos);
    }

    bool CanHiding()
    {
        foreach(var n in Physics2D.OverlapCircleAll(transform.position,InteractRadius))
        {
            if(n.GetComponent<HidingSpot>() != null)
            {
                transform.position = new Vector2(n.transform.position.x,transform.position.y);
                return true;
            }
        }

        return false;
    }

    
    bool CanEnterDoor()
    {
        foreach(var n in Physics2D.OverlapCircleAll(transform.position,InteractRadius))
        {
            if(n.GetComponent<DoorSystem>() != null)
            {
                warpPos = n.GetComponent<DoorSystem>().connectDoor;
                return true;
            }
        }

        return false;
    }

    void ToggleHiding()
    {
        if(!PlayerManager.inst.isHide)
        {
            PlayerManager.inst.isHide = true;
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
        }
        else if(PlayerManager.inst.isHide)
        {
            PlayerManager.inst.isHide = false;
            GetComponent<SpriteRenderer>().enabled = true;
            GetComponent<Collider2D>().enabled = true;
        } 

        return;
    }

    void EnterDoor(Transform pos)
    {
        transform.position = new Vector2(pos.position.x,transform.position.y);
        return;
    }


    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position,InteractRadius);    
    }
}
