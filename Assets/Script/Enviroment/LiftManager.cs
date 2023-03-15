using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class LiftManager : MonoBehaviour
{
    public static LiftManager inst;
    Transform playerPosition;
    bool playerInteractAtSide;
    [Header("System")]
    public Transform[] connectFloorsLeftSide;
    public Transform[] connectFloorsRightSide;
    public ItemScriptableObject conditionItem;
    public int selectedFloor;

    [Header("UI")]
    public GameObject Canvas_Lift;
    [SerializeField] GameObject[] floor_Buttons;
    [SerializeField] GameObject floor5_Button;
    [SerializeField] GameObject closeButton;

    void Awake()
    {
        inst = this;
    }

    private void Start()
    {
        AddListenerToButton(GoToFloor5 , CloseLiftPannel);
    }
    public void AddListenerToButton(UnityAction goToFloor5 , UnityAction closeLiftPannel)
    {
        for (int i = 0; i <= 3; i++)
        {
            int index = i;
            floor_Buttons[index].GetComponent<Button>().onClick.RemoveAllListeners();
            floor_Buttons[index].GetComponent<Button>().onClick.AddListener(() => { OnSelectFloor(index);});
        }

        floor5_Button.GetComponent<Button>().onClick.RemoveAllListeners();
        floor5_Button.GetComponent<Button>().onClick.AddListener(() => { OnSelectFloor(5); });

        closeButton.GetComponent<Button>().onClick.RemoveAllListeners();
        closeButton.GetComponent<Button>().onClick.AddListener(closeLiftPannel);
    }

    void OnSelectFloor(int index)
    {
        selectedFloor = index; 
        CloseCanvas_TransitionBlack_PlayerState();
    }

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
        switch (lift.currentFloor)
        {
            case Lift.CurrentFloor.Floor1:
                floor_Buttons[0].GetComponent<Button>().interactable = false;
                break;
            case Lift.CurrentFloor.Floor2:
                floor_Buttons[1].GetComponent<Button>().interactable = false;
                break;
            case Lift.CurrentFloor.Floor3:
                floor_Buttons[2].GetComponent<Button>().interactable = false;
                break;
            case Lift.CurrentFloor.Floor4:
                floor_Buttons[3].GetComponent<Button>().interactable = false;
                break;
            case Lift.CurrentFloor.Floor5:
                floor5_Button.GetComponent<Button>().interactable = false;
                break;
        }
    }

    public void EnterLift(Transform entity , Transform connectFloorLeftSide , Transform connectFloorRightSide , bool isLeftSide)
    {
        Vector3 nextPosition;
        if (isLeftSide == true)
        {
            nextPosition = new Vector3(connectFloorLeftSide.position.x, connectFloorLeftSide.position.y, 0);
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

    public void GoToFloor(int index)
    {
        EnterLift(playerPosition, connectFloorsLeftSide[index], connectFloorsRightSide[index], playerInteractAtSide);
        if(index != 0)
            AiMermaidController.inst.DestroyWhenEnterLift();
    }
    
    public void GoToFloor5()
    {
        EnterLift(playerPosition, connectFloorsLeftSide[4], null, playerInteractAtSide);
        AiMermaidController.inst.DestroyMermaidAI();
        AiMermaidController.inst.spawnAI = false;
    }

    void CloseCanvas_TransitionBlack_PlayerState()
    {
        Canvas_Lift.SetActive(false);
        UITransition.inst.LiftTransitionIn();
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
