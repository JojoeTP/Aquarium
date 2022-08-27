using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingSpot : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D other) 
    {
        if(other.GetComponent<PlayerMovement>() != null)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                if(other.GetComponent<PlayerMovement>().hide)
                {
                    other.GetComponent<PlayerMovement>().SetSortingOrder(1);
                    other.GetComponent<PlayerMovement>().hide = false;
                }
                else
                {
                    other.GetComponent<PlayerMovement>().SetSortingOrder(-1);
                    other.GetComponent<PlayerMovement>().hide = true;
                }
            }
        }
    }
}
