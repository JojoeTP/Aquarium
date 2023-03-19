using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISpawnConfig : MonoBehaviour
{
    [SerializeField] Transform spawnPosition;
    
    void OnTriggerExit2D(Collider2D other) 
    {
        if(other.GetComponent<PlayerManager>() != null)
        {
            AiDirectorController.inst.spawnAI = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.GetComponent<PlayerManager>() != null)
        {
            AiDirectorController.inst.spawnAI = true;
            AiDirectorController.inst.SpawnPosition = spawnPosition;
        }
    }
}
