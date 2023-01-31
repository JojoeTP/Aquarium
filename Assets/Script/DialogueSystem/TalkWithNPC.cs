using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class NPCDialogueCondition{
    public bool isTalk = false;
    public ItemScriptableObject conditionItem;
    public string correctDialogueID;
    public UnityEvent triggerEvents;
}
public class TalkWithNPC : MonoBehaviour
{
    [SerializeField] bool isCutScene;
    public string startWithDialogueId;
    public UnityEvent triggerEvents;
    public List<NPCDialogueCondition> NPCList = new List<NPCDialogueCondition>();
    public void SetDialogueID()
    {
        foreach (var n in NPCList)
        {
            if (n.conditionItem == null)
            {
                print("null item");
                return;
            }

            if (CheckCondition(n))
            {
                if (n.isTalk == false)
                {
                    ChangeDialogueId(n.correctDialogueID);
                }
                triggerEvents = n.triggerEvents;
                n.isTalk = true;
                return;
            }
            else
            {
                print("incorrect item");
            }
        }
    }
    bool CheckCondition(NPCDialogueCondition NPCData)
    {
        if(PlayerManager.inst.PlayerInventory.PlayerItemDictionary.ContainsValue(NPCData.conditionItem.itemData.ItemID))
        {
            return true;
        }
        return false;
    }

    public void ChangeDialogueId(string changeDialogueId)
    {
        startWithDialogueId = changeDialogueId;
    }

    public void SetActiveFalse()
    {
        this.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isCutScene)
        {
           if (other.GetComponent<PlayerManager>() != null)
           {
                //PlayerEnterDoor(other.transform);
                // Need Fade Black Animation
                PlayerManager.inst.PlayerInteract.Interacting();
           }
        }
    }
}
