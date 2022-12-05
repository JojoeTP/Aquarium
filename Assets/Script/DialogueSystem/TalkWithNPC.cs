using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public struct NPCDialogueCondition{
    public ItemScriptableObject conditionItem;
    public string correctDialogueID;
    public UnityEvent triggerEvents;
}
public class TalkWithNPC : MonoBehaviour
{
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
                startWithDialogueId = n.correctDialogueID;
                triggerEvents = n.triggerEvents;
                return;
            }
        }
    }
    bool CheckCondition(NPCDialogueCondition NPCData)
    {
        foreach (var item in PlayerManager.inst.playerInventory.itemList)
        {
            if (item.itemData == NPCData.conditionItem.itemData)
                return true;
        }
        return false;
    }


}
