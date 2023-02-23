using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lift : MonoBehaviour
{
    public CurrentFloor currentFloor;
    public enum CurrentFloor
    {
        Floor1,Floor2, Floor3, Floor4, Floor5
    }
    public bool leftSide;
}
