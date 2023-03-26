using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpawnConfigType
{
    Junitor,
    RedHood,
    Director,
}

public class AISpawnConfig : MonoBehaviour
{
    [SerializeField] SpawnConfigType spawnConfigType;
    [SerializeField] Transform spawnPosition;
    
    void OnTriggerExit2D(Collider2D other) 
    {
        if(other.GetComponent<PlayerManager>() != null)
        {
            switch(spawnConfigType)
            {
                case SpawnConfigType.Junitor:
                    AiJunitorController.inst.isPlayerInSpawnCollider = false;
                    break;
                case SpawnConfigType.RedHood:
                    AiRedHoodController.inst.isPlayerInSpawnCollider = false;
                    break;
                case SpawnConfigType.Director:
                    AiDirectorController.inst.isPlayerInSpawnCollider = false;
                    break;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.GetComponent<PlayerManager>() != null)
        {
            switch(spawnConfigType)
            {
                case SpawnConfigType.Junitor:
                    // AiJunitorController.inst.spawnAI = true;
                    AiJunitorController.inst.isPlayerInSpawnCollider = true;
                    AiJunitorController.inst.SpawnPosition = spawnPosition;
                    break;
                case SpawnConfigType.RedHood:
                    AiRedHoodController.inst.isPlayerInSpawnCollider = true;
                    AiRedHoodController.inst.SpawnPosition = spawnPosition;
                    break;
                case SpawnConfigType.Director:
                    AiDirectorController.inst.isPlayerInSpawnCollider = true;
                    AiDirectorController.inst.SpawnPosition = spawnPosition;
                    break;
            }
        }
    }
}
