using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LiftManager : MonoBehaviour
{
    public static LiftManager inst;
    Transform playerPosition;
    [Header("System")]
    public Transform[] connectFloors;
    public ItemScriptableObject conditionItem;

    [Header("UI")]
    public GameObject Canvas_Lift;
    [SerializeField] GameObject[] floor_Buttons;
    [SerializeField] GameObject floor5_Button;

    public bool CheckCondition()
    {
        if (conditionItem == null)
            return true;

        if (PlayerManager.inst.PlayerInventory.PlayerItemDictionary.ContainsValue(conditionItem.itemData))
        {
            return true;
        }
        return false;
    }

    private void Awake()
    {
        inst = this;
    }

    public void ShowSelectionFloor(Lift lift)
    {
        Canvas_Lift.SetActive(true);

        for(var i = 0; i < floor_Buttons.Length; i++)
        {
            floor_Buttons[i].GetComponent<Button>().interactable = true;
            if (lift.currentFloor == Lift.CurrentFloor.Floor1)
            {
                floor_Buttons[0].GetComponent<Button>().interactable = false;
            }
            else if (lift.currentFloor == Lift.CurrentFloor.Floor2)
            {
                floor_Buttons[1].GetComponent<Button>().interactable = false;
            }
            else if (lift.currentFloor == Lift.CurrentFloor.Floor3)
            {
                floor_Buttons[2].GetComponent<Button>().interactable = false;
            }
        }

        if (CheckCondition())
        {
            floor5_Button.SetActive(true);
        }
        else
        {
            floor5_Button.SetActive(false);
        }

    }

    public void EnterDoor(Transform entity , Transform connectFloor)
    {
        if (connectFloor != null)
        {
            Vector3 nextPosition = new Vector3(connectFloor.position.x, connectFloor.position.y , 0);

            entity.position = nextPosition;
        }
    }

    public void UpdatePlayerPosition(Transform newPosition)
    {
        playerPosition = newPosition;
    }
    public void GoToFloor1()
    {
        EnterDoor(playerPosition, connectFloors[0]);
        Canvas_Lift.SetActive(false);
        UITransition.inst.DoorTransitionIn();
    }
    public void GoToFloor2()
    {
        EnterDoor(playerPosition, connectFloors[1]);
        Canvas_Lift.SetActive(false);
        UITransition.inst.DoorTransitionIn();
    }
    public void GoToFloor3()
    {
        EnterDoor(playerPosition, connectFloors[2]);
        Canvas_Lift.SetActive(false);
        UITransition.inst.DoorTransitionIn();
    }
}
