using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class AiDirectorController : MonoBehaviour
{
    public static AiDirectorController inst;
    [SerializeField] GameObject directorPrefab;
    [SerializeField] Transform respawnPosition;
    Transform spawnPosition;
    [HideInInspector]
    public StateController directorController;

    [SerializeField] Volume nightGlobalVolume;
    
    public bool spawnAI = false;
    public bool isPlayerInSpawnCollider = false;
    public bool isPlayerMove = false;

    public Transform SpawnPosition {set {spawnPosition = value;}}

    private void Awake() {
        inst = this;
    }

    private void Start() {
        InputSystemManager.Inst.onPressMove += (b) => isPlayerMove = b;
        
        StartCoroutine(CreateDirectorAI());
    }

    public IEnumerator CreateDirectorAI()
    {
        yield return new WaitUntil(() => isPlayerMove);

        if(spawnAI && isPlayerInSpawnCollider && spawnPosition != null && directorController == null)
        {
            var rand = Random.Range(0,100);
            if(rand >= 70)
            {
                UITransition.inst.DirectorTransitionIn();
            }
        }

        if(directorController == null)
        {
            yield return new WaitForSeconds(3f);
            StartCoroutine(CreateDirectorAI());
        }
    }

    public void CreateDirector()
    {
        var aiPrefab = Instantiate(directorPrefab,spawnPosition.position,Quaternion.identity);
        directorController = aiPrefab.GetComponent<StateController>();

        if(nightGlobalVolume.profile.TryGet<ColorAdjustments>(out var colorAdj))
        {
            colorAdj.active = true;
        }

        directorController.animator.SetBool("Event",true);
    }

    public void DestroyDirectorAI()
    {
        if (directorController != null)
        {
            AiDirectorController.inst.directorController.animator.SetBool("Event",false);
            Destroy(directorController.gameObject);
            directorController = null;
            
            if(nightGlobalVolume.profile.TryGet<ColorAdjustments>(out var colorAdj))
           {
                colorAdj.active = false;
           }

           StartCoroutine(CreateDirectorAI());
        }
    }

    public void DirectorTransition()
    {
        if (directorController == null)
            CreateDirector();
        else
            DestroyDirectorAI();
    }

    public void DestroyWhenEnterDoor()
    {
        DestroyDirectorAI();
    }

    public void OnAttackPlayer()
    {
        UITransition.inst.DieTransitionIn();
    }

    public void RespawnPlayer()
    {
        if(directorController == null)
            return;

        DestroyDirectorAI();
        PlayerManager.inst.transform.position = respawnPosition.position;
    }
}