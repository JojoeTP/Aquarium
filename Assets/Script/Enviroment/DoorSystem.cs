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
        // if(entity.GetComponent<PlayerManager>() != null)
        // {
            if(CheckCondition())
                entity.position = new Vector3(connectDoor.position.x,entity.position.y,0);

            print("connectDoor " + connectDoor.position);
            print("entity " +  entity.position);
        // }
        // else
        // {
        //     Debug.Log("Before " + entity.position);
        //     entity.position = new Vector2(connectDoor.position.x,entity.position.y);
        //     Debug.Log("After " + entity.position);
        // }

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
