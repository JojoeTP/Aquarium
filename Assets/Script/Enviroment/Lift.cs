using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift : MonoBehaviour
{
    public List<Lift> liftButtons = new List<Lift>();
    public void ShowLiftButton()
    {
        //Canvas_Lift.SetActive(true);

        //for (var i = 0; i < floor_Buttons.Length; i++)
        //{
        //    floor_Buttons[i].GetComponent<Button>().interactable = true;
        //    if (lift.currentFloor == Lift.CurrentFloor.Floor1)
        //    {
        //        floor_Buttons[0].GetComponent<Button>().interactable = false;
        //    }
        //    else if (lift.currentFloor == Lift.CurrentFloor.Floor2)
        //    {
        //        floor_Buttons[1].GetComponent<Button>().interactable = false;
        //    }
        //    else if (lift.currentFloor == Lift.CurrentFloor.Floor3)
        //    {
        //        floor_Buttons[2].GetComponent<Button>().interactable = false;
        //    }
        //}

        //if (CheckCondition())
        //{
        //    floor5_Button.SetActive(true);
        //}
        //else
        //{
        //    floor5_Button.SetActive(false);
        //}
    }

    public CurrentFloor currentFloor;
    public enum CurrentFloor
    {
        Floor1,Floor2, Floor3, Floor4, Floor5
    }
    
}
