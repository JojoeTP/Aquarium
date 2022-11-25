using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class DoorSystem : MonoBehaviour
{
    [Header("System")]
    public Transform connectDoor;
    public ItemScriptableObject conditionItem;

    public bool isSpacialDoor;

    public bool isPlayerUseItBefore;
    public bool canEnemyEnter;

    //Call back
    [HideInInspector] public UnityEvent triggerEvent; //Create new event function in actionEventManager script

    public void EnterDoor(Transform entity)
    {   
        if(connectDoor != null)
        {
            Vector3 nextPostion = new Vector3(connectDoor.position.x,(connectDoor.position.y - (transform.position.y - entity.position.y)),0);
            
            if(CheckCondition())
            {
                entity.position = nextPostion;
            }
        }
    }

    public void PlayerEnterDoor(Transform entity)
    {
        TriggerDoorEvent();
        EnterDoor(entity);
    }

    public void EnemyEnterDoor(Transform entity)
    {
        EnterDoor(entity);
    }

    public void TriggerDoorEvent()
    {
        if(CheckCondition())
        {
            triggerEvent?.Invoke();
        }
    }

    bool CheckCondition()
    {
        if(conditionItem == null)
            return true;
            
        foreach (var item in PlayerManager.inst.playerInventory.itemList)
        {
            if(item.itemData == conditionItem.itemData)
                return true;
        }
        return false;
    }

    public bool RandomChanceToEnter()
    {
        var rand = UnityEngine.Random.Range(0f,100f);
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

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(isSpacialDoor)
        {
            if(other.GetComponent<PlayerManager>() != null)
            {
                EnterDoor(other.transform);
            }
        }
    }


    //Make Callback function
}
