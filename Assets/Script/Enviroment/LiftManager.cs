using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LiftManager : MonoBehaviour
{
    public static LiftManager inst;
    Transform playerPosition;
    bool playerInteractAtSide;
    [Header("System")]
    public Transform[] connectFloorsLeftSide;
    public Transform[] connectFloorsRightSide;
    public ItemScriptableObject conditionItem;

    [Header("UI")]
    public GameObject Canvas_Lift;
    [SerializeField] GameObject[] floor_Buttons;
    [SerializeField] GameObject floor5_Button;

    public bool CheckCondition()
    {
        if (conditionItem == null)
            return true;

        if (PlayerManager.inst.PlayerInventory.PlayerItemDictionary.ContainsValue(conditionItem.itemData.ItemID))
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
        ShowLiftButton(lift);
        if (CheckCondition())
        {
            floor5_Button.SetActive(true);
        }
        else
        {
            floor5_Button.SetActive(false);
        }
    }
    void ShowLiftButton(Lift lift)
    {
        foreach (GameObject n in floor_Buttons)
        {
            n.GetComponent<Button>().interactable = true;
        }
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
        else if (lift.currentFloor == Lift.CurrentFloor.Floor4)
        {
            floor_Buttons[3].GetComponent<Button>().interactable = false;
        }
    }

    public void EnterDoor(Transform entity , Transform connectFloorLeftSide , Transform connectFloorRightSide , bool isLeftSide)
    {
        Vector3 nextPosition;
        if (isLeftSide == true)
        {
            nextPosition = new Vector3(connectFloorLeftSide.position.x, connectFloorLeftSide.position.y , 0);
        }
        else
        {
            nextPosition = new Vector3(connectFloorRightSide.position.x, connectFloorRightSide.position.y, 0);
        }

        entity.position = nextPosition;
    }

    public void UpdatePlayerPosition(Transform newPosition)
    {
        playerPosition = newPosition;
    }
    public void PlayerInteractAtSide(bool newSide)
    {
        playerInteractAtSide = newSide;
    }
    public void GoToFloor1()
    {
        EnterDoor(playerPosition, connectFloorsLeftSide[0] , connectFloorsRightSide[0] , playerInteractAtSide);
        CloseCanvas_TransitionBlack_PlayerState();
    }
    public void GoToFloor2()
    {
        EnterDoor(playerPosition, connectFloorsLeftSide[1], connectFloorsRightSide[1], playerInteractAtSide);
        CloseCanvas_TransitionBlack_PlayerState();
    }
    public void GoToFloor3()
    {
        EnterDoor(playerPosition, connectFloorsLeftSide[2], connectFloorsRightSide[2], playerInteractAtSide);
        CloseCanvas_TransitionBlack_PlayerState();
    }
    public void GoToFloor4()
    {
        EnterDoor(playerPosition, connectFloorsLeftSide[3], null, playerInteractAtSide);
        CloseCanvas_TransitionBlack_PlayerState();
    }

    void CloseCanvas_TransitionBlack_PlayerState()
    {
        Canvas_Lift.SetActive(false);
        UITransition.inst.DoorTransitionIn();
        SetPlayerToNoneState();
    }

    public void CloseLiftPannel()
    {
        Canvas_Lift.SetActive(false);
        SetPlayerToNoneState();
    }

    void SetPlayerToNoneState()
    {
        if (PlayerManager.inst.playerState != PlayerManager.PLAYERSTATE.ENTERDOOR)
            return;

        PlayerManager.inst.playerState = PlayerManager.PLAYERSTATE.NONE;
    }
}
