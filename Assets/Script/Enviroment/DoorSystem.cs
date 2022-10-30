using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSystem : MonoBehaviour
{
    [Header("System")]
    public Transform connectDoor;
    public ItemScriptableObject conditionItem;

    public bool isPlayerUseItBefore;
    public bool canEnemyEnter;

    public void EnterDoor(Transform entity)
    {   
        Vector3 nextPostion = new Vector3(connectDoor.position.x,(connectDoor.position.y - (transform.position.y - entity.position.y)),0);
        
        if(CheckCondition())
        {
            entity.position = nextPostion;
        }
    }

    bool CheckCondition()
    {
        if(conditionItem == null)
            return true;
            
        foreach (var item in PlayerManager.inst.playerInventory.itemList)
        {
            if(item.itemData.Id == conditionItem.itemData.Id)
                return true;
        }
        return false;
    }

    public bool RandomChanceToEnter()
    {
        var rand = Random.Range(0f,100f);
        if(isPlayerUseItBefore && rand > 30f)
        {
            return true;
        }
        else if(!isPlayerUseItBefore && rand > 70f)
        {
            return true;
        }

        return false;
    }
}
