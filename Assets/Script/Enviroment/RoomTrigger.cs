using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) 
    {
        print(1);
        if(other.CompareTag("Player"))
        {
            RecordTimeManager.Inst.getInRoomTime = Time.time;
        }
    }
}