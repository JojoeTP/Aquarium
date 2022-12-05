using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class RoomTrigger : MonoBehaviour
{
    bool isPlayerEnter = false;
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(isPlayerEnter)
            return;
            
        if(other.CompareTag("Player"))
        {
            isPlayerEnter = true;
            RecordTimeManager.Inst.getInRoomTime = Time.time;
        }
    }
}
