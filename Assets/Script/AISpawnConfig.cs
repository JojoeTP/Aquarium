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
            AiDirectorController.inst.spawnAI = false;
            switch(spawnConfigType)
            {
                case SpawnConfigType.Junitor:
                    // AiJunitorController.inst.spawnAI = false;
                    break;
                case SpawnConfigType.RedHood:
                    // AiRedHoodController.inst.spawnAI = false;
                    break;
                case SpawnConfigType.Director:
                    AiDirectorController.inst.spawnAI = false;
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
                    AiJunitorController.inst.SpawnPosition = spawnPosition;
                    break;
                case SpawnConfigType.RedHood:
                    // AiRedHoodController.inst.spawnAI = true;
                    AiRedHoodController.inst.SpawnPosition = spawnPosition;
                    break;
                case SpawnConfigType.Director:
                    AiDirectorController.inst.spawnAI = true;
                    AiDirectorController.inst.SpawnPosition = spawnPosition;
                    break;
            }
        }
    }
}
