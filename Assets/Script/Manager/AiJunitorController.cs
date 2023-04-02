using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class AiJunitorController : MonoBehaviour
{
    public static AiJunitorController inst;
    [SerializeField] GameObject junitorPrefab;
    [SerializeField] Transform respawnPosition;
    Transform spawnPosition;
    [HideInInspector]
    public StateController junitorController;

    [SerializeField] Volume nightGlobalVolume;
    
    public bool spawnAI = false;
    public bool isPlayerInSpawnCollider = false;
    public bool isPlayerMove = false;
    public float spawnTime = 0f;

    public bool CannotExitHiding = false;
    public Transform SpawnPosition {set {spawnPosition = value;}}

    private void Awake() {
        inst = this;
    }

    private void Start() {
        InputSystemManager.Inst.onPressMove += (b) => isPlayerMove = b;
        
        StartCoroutine(CreateJunitorAI());
    }

    public IEnumerator CreateJunitorAI()
    {
        yield return new WaitUntil(() => isPlayerMove);
        
        yield return new WaitForSeconds(spawnTime);
        
        if(spawnAI && isPlayerInSpawnCollider && spawnPosition != null && junitorController == null)
        {
            var rand = Random.Range(0,100);
            if(rand >= 60)
            {
                // UITransition.inst.JunitorTransitionIn();
                CreateJunitor();
            }
        }

        if(junitorController == null)
        {
            yield return new WaitForSeconds(spawnTime);
            StartCoroutine(CreateJunitorAI());
        }
    }

    public void CreateJunitor()
    {
        var aiPrefab = Instantiate(junitorPrefab,spawnPosition.position,Quaternion.identity);
        junitorController = aiPrefab.GetComponent<StateController>();

        if(nightGlobalVolume.profile.TryGet<ColorAdjustments>(out var colorAdj))
        {
            colorAdj.active = true;
        }
    }

    public void DestroyJunitorAI()
    {
        if (junitorController != null)
        {
            Destroy(junitorController.gameObject);
            CannotExitHiding = false;
            junitorController = null;
            
            if(nightGlobalVolume.profile.TryGet<ColorAdjustments>(out var colorAdj))
           {
                colorAdj.active = false;
           }

           StartCoroutine(CreateJunitorAI());
        }
    }

    public void JunitorTransition()
    {
        if (junitorController == null)
            CreateJunitor();
        else
            DestroyJunitorAI();
    }

    public void DestroyWhenEnterDoor()
    {
        DestroyJunitorAI();
    }

    public void OnAttackPlayer()
    {
        UITransition.inst.DieTransitionIn();
    }

    public void RespawnPlayer()
    {
        if(junitorController == null)
            return;

        DestroyJunitorAI();
        PlayerManager.inst.transform.position = respawnPosition.position;
    }

    public void SetActiveAI(bool isActive)
    {
        if(junitorController != null)
            junitorController.enabled = isActive;
    }
}