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
            if(TalkWithNPC(n.transform))
            {
                n.GetComponent<TalkWithNPC>().SetDialogueID();
                StartDialogue(n.GetComponent<TalkWithNPC>());
                return;
            }
            if(CanHiding(n.transform))
            {
                ToggleHiding(n.GetComponent<HidingSpot>());
                n.GetComponent<HidingSpot>().ChangeSprite();
                return;
            }

            if(CanEnterDoor(n.transform))
            {
                enteringDoor = n.GetComponent<DoorSystem>();
                if(!enteringDoor.isLockedDoor)
                    EnterDoor();
                return;
            }

                
            if(CanGetItem(n.transform))
            {
                GetItem(n.GetComponent<Item>());
                return;
            }

            if (CanEnterLift(n.transform))
            {
                ShowSelectionFloor(n.GetComponent<Lift>());
                return;
            }
        }
    }

    void ShowSelectionFloor(Lift lift)
    {
        if (PlayerManager.inst.playerState != PlayerManager.PLAYERSTATE.NONE)
            return;

        PlayerManager.inst.playerState = PlayerManager.PLAYERSTATE.ENTERDOOR;

        LiftManager.inst.UpdatePlayerPosition(this.transform);
        LiftManager.inst.PlayerInteractAtSide(lift.leftSide);
        LiftManager.inst.ShowSelectionFloor(lift);
    }
    bool CanEnterLift(Transform overlap)
    {
        if (overlap.GetComponent<Lift>() != null)
        {
            return true;
        }

        return false;
    }
    bool TalkWithNPC(Transform overlap){
        if(overlap.GetComponent<TalkWithNPC>() != null)
        {
            return true;
        }
        return false;
    }

    public void StartDialogue(TalkWithNPC NPC)
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
            if(overlap.GetComponent<Item>().itemObject.itemData.conditionItem != null)
            {
                if(!PlayerManager.inst.PlayerInventory.PlayerItemDictionary.ContainsValue(overlap.GetComponent<Item>().itemObject.itemData.conditionItem.itemData.ItemID))
                {
                    DialogueManager.inst.currentDialogue = overlap.GetComponent<Item>().itemObject.itemData.cannotPickUpDialogueItemId;
                    DialogueManager.inst.StartDialogue();
                    return false;
                }
                else
                    return true;
            }

            return true;
        }

        return false;
    }


    void GetItem(Item item)
    {
        // PlayerManager.inst.playerInventory.itemList.Add(item.item);
        if(PlayerManager.inst.playerState == PlayerManager.PLAYERSTATE.GETTINGITEM)
            return;

        PlayerManager.inst.playerState = PlayerManager.PLAYERSTATE.CONVERSATION;
        if (item.itemObject.itemData.dialogueItemId == "")
        {
            item.gameObject.SetActive(false);
            PlayerManager.inst.playerState = PlayerManager.PLAYERSTATE.NONE;
        }
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
        if(overlap.TryGetComponent<DoorSystem>(out var door))
        {
            if(door.CheckCondition())
                return true;
        }

        return false;
    }

    public void EnterDoor()
    {
        if(PlayerManager.inst.playerState != PlayerManager.PLAYERSTATE.NONE)
            return;

        UITransition.inst.DoorTransitionIn();
        
        PlayerManager.inst.playerState = PlayerManager.PLAYERSTATE.ENTERDOOR;
    }

    public void ExitDoor()
    {
        PlayerManager.inst.playerState = PlayerManager.PLAYERSTATE.NONE;

        if (enteringDoor != null)
        {
            enteringDoor.PlayerEnterDoor(this.transform);
            enteringDoor.OnDoorEvent();
        }
    }

    void ToggleHiding(HidingSpot hidingSpot)
    {
        if(PlayerManager.inst.playerState == PlayerManager.PLAYERSTATE.NONE)
        {
            foreach(var n in PlayerManager.inst.playerSprite)
            {
                n.SetActive(false);
            }

            // GetComponent<Collider2D>().enabled = false;
            PlayerManager.inst.playerState = PlayerManager.PLAYERSTATE.HIDING;
            PlayerManager.inst.PlayerLight.ToggleLight(false);
            hidingSpot.OnEnterHidingEvent();
        }
        else if(PlayerManager.inst.playerState == PlayerManager.PLAYERSTATE.HIDING)
        {
            if(AiJunitorController.inst.CannotExitHiding)
                return;

            foreach(var n in PlayerManager.inst.playerSprite)
            {
                n.SetActive(true);
            }
            // GetComponent<Collider2D>().enabled = true;
            PlayerManager.inst.playerState = PlayerManager.PLAYERSTATE.NONE;

            if(PlayerManager.inst.PlayerLight.isLight)
                    PlayerManager.inst.PlayerLight.ToggleLight(true);
            
            hidingSpot.OnHidingEvent();
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
