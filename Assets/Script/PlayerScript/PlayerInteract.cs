using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class PlayerInteract : MonoBehaviour
{
    [Header("Hiding Setting")]
    public Vector2 InteractSize;
    public Vector3 InteractOffset;

    DoorSystem enteringDoor;

    public DoorSystem EnteringDoor {set {enteringDoor = value;}}

    public event Action<Item> OnOpenItemPopUpUI = delegate {};

    void Start()
    {
        InputSystemManager.Inst.onInteract += OnInteract;    
    }

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
                ToggleHiding(n.GetComponent<HidingSpot>());
            }

            if(CanEnterDoor(n.transform))
            {
                enteringDoor = n.GetComponent<DoorSystem>();
                EnterDoor();
            }

            if(TalkWithNPC(n.transform))
            {
                n.GetComponent<TalkWithNPC>().SetDialogueID();
                StartDialogue(n.GetComponent<TalkWithNPC>());
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

    void StartDialogue(TalkWithNPC NPC)
    {
        if (PlayerManager.inst.playerState == PlayerManager.PLAYERSTATE.CONVERSATION)
        {
            return;
        }
        if (DialogueManager.inst.currentNPC == null)
        {
            DialogueManager.inst.currentNPC = NPC;
            DialogueManager.inst.currentDialogue = NPC.startWithDialogueId;
        }

        DialogueManager.inst.StartDialogue();
        PlayerManager.inst.playerState = PlayerManager.PLAYERSTATE.CONVERSATION;
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
        // PlayerManager.inst.playerInventory.itemList.Add(item.item);
        PlayerManager.inst.playerState = PlayerManager.PLAYERSTATE.CONVERSATION;
        PlayerManager.inst.PlayerInventory.AddItem(item.itemObject);
        OnOpenItemPopUpUI(item);

        SavePickUpItemTime(item);
    }

    void SavePickUpItemTime(Item item)
    {
        ItemTimeData itemData = new ItemTimeData();
        itemData.effectName = ItemManager.Inst.ItemEffectData.ItemEffectSettingList.Find(n => n.iTEMTYPE.HasFlag(item.itemObject.itemData.ItemType)).effectTYPE.ToString();
        itemData.time = item.GetPickUpTime();
        RecordTimeManager.Inst.SavePickUpItemTimeData(item.itemObject.itemData.ItemName,itemData);
    }
    
    bool CanEnterDoor(Transform overlap)
    {
        if(overlap.GetComponent<DoorSystem>() != null)
        {
            return true;
        }

        return false;
    }

    public void EnterDoor()
    {
        if(PlayerManager.inst.playerState != PlayerManager.PLAYERSTATE.NONE)
            return;

        //Don't play if player can't enter door
        UITransition.inst.DoorTransitionIn();
        
        PlayerManager.inst.playerState = PlayerManager.PLAYERSTATE.ENTERDOOR;
    }

    public void ExitDoor()
    {
        PlayerManager.inst.playerState = PlayerManager.PLAYERSTATE.NONE;
        enteringDoor.PlayerEnterDoor(this.transform);
        enteringDoor.OnDoorEvent();
    }

    void ToggleHiding(HidingSpot hidingSpot)
    {
        if(PlayerManager.inst.playerState == PlayerManager.PLAYERSTATE.NONE)
        {
            PlayerManager.inst.playerSprite.SetActive(false);
            GetComponent<Collider2D>().enabled = false;
            PlayerManager.inst.playerState = PlayerManager.PLAYERSTATE.HIDING;
            hidingSpot.OnHidingEvent();
        }
        else if(PlayerManager.inst.playerState == PlayerManager.PLAYERSTATE.HIDING)
        {
            PlayerManager.inst.playerSprite.SetActive(true);
            GetComponent<Collider2D>().enabled = true;
            PlayerManager.inst.playerState = PlayerManager.PLAYERSTATE.NONE;
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
