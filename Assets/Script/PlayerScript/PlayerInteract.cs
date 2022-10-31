using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    [Header("Hiding Setting")]
    public float InteractRadius;
    public Vector3 InteractOffset;

    public void Interacting()
    {
        if(CanHiding())
            ToggleHiding();
        
        CanEnterDoor();

        if(TalkWithNPC())
            StartDialogue();
    }

    public void OnInteract(InputValue value)
    {
        if(value.isPressed)
        {
            Interacting();
        }
    }

    bool TalkWithNPC(){
        foreach(var n in Physics2D.OverlapCircleAll(transform.position + InteractOffset,InteractRadius))
        {
            if(n.GetComponent<TalkWithNPC>() != null)
            {
                return true;
            }
        }
        return false;
    }
    void StartDialogue(){
        DialogueManager.inst.Invoke("StartDialogue",0);
        return;
    }

    bool CanHiding()
    {
        foreach(var n in Physics2D.OverlapCircleAll(transform.position + InteractOffset,InteractRadius))
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
                n.GetComponent<DoorSystem>().EnterDoor(this.transform);
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
            PlayerManager.inst.playerSprite.SetActive(false);
            GetComponent<Collider2D>().enabled = false;
        }
        else if(PlayerManager.inst.isHide)
        {
            PlayerManager.inst.isHide = false;
            PlayerManager.inst.playerSprite.SetActive(true);
            GetComponent<Collider2D>().enabled = true;
        } 

        return;
    }

    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position + InteractOffset,InteractRadius);    
    }
}
