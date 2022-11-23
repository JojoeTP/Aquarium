using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    [Header("Hiding Setting")]
    public Vector2 InteractSize;
    public Vector3 InteractOffset;

    public void Interacting()
    {
        IsObjectOverlapPlayer();
    }

    public void OnInteract(InputValue value)
    {
        if(value.isPressed)
        {
            Interacting();
        }
    }

    void IsObjectOverlapPlayer()
    {
        foreach(var n in Physics2D.OverlapCapsuleAll(transform.position+InteractOffset,InteractSize,CapsuleDirection2D.Vertical,0))
        {
            if(CanHiding(n.transform))
            {
                ToggleHiding();
            }

            if(CanEnterDoor(n.transform))
            {
                UITransition.inst.PlayOverlayTransition();
                n.GetComponent<DoorSystem>().EnterDoor(this.transform);
            }

            if(TalkWithNPC(n.transform))
            {
                StartDialogue();
            }
                
            if(CanGetItem(n.transform))
            {
                GetItem(n.GetComponent<Item>());
            }                
        }
    }

    bool TalkWithNPC(Transform overlap){
        if(overlap.GetComponent<TalkWithNPC>() != null)
        {
            return true;
        }
        return false;
    }
    void StartDialogue(){
        DialogueManager.inst.Invoke("StartDialogue",0);
        return;
    }

    bool CanHiding(Transform overlap)
    {
        if(overlap.GetComponent<HidingSpot>() != null)
        {
            transform.position = new Vector2(overlap.transform.position.x,transform.position.y);
            return true;
        }

        return false;
    }

    bool CanGetItem(Transform overlap)
    {
        if(overlap.GetComponent<Item>() != null)
        {
            return true;
        }

        return false;
    }

    void GetItem(Item item)
    {
        PlayerManager.inst.playerInventory.itemList.Add(item.item);
        RecordTimeManager.Inst.SavePickUpItemTimeData(item.item.itemData.ItemName,item.GetPickUpTime());
        item.RunPickUpEvent();
        item.gameObject.SetActive(false);
    }

    
    bool CanEnterDoor(Transform overlap)
    {
        if(overlap.GetComponent<DoorSystem>() != null)
        {
            return true;
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
        Gizmos.DrawWireSphere(transform.position + new Vector3(0,InteractSize.y/2,0) + InteractOffset,InteractSize.x/2);  
        Gizmos.DrawWireSphere(transform.position - new Vector3(0,InteractSize.y/2,0) + InteractOffset,InteractSize.x/2);  
        Gizmos.DrawWireCube(transform.position + InteractOffset,InteractSize);  
    }
}
