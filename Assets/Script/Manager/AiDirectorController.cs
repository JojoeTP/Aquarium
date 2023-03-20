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
    StateController directorController;

    [SerializeField] Volume nightGlobalVolume;
    
    public bool spawnAI = false;
    public bool isPlayerMove = false;

    Coroutine createDirector;
    public Transform SpawnPosition {set {spawnPosition = value;}}

    private void Awake() {
        inst = this;
    }

    private void Start() {
        InputSystemManager.Inst.onPressMove += (b) => isPlayerMove = b;
        createDirector = StartCoroutine(CreateDirectorAI());
    }

    IEnumerator CreateDirectorAI()
    {
        yield return new WaitUntil(() => isPlayerMove);

        if(spawnAI && spawnPosition != null && directorController == null)
        {
            var rand = Random.Range(0,100);
            if(rand >= 70)
            {
                var aiPrefab = Instantiate(directorPrefab,spawnPosition.position,Quaternion.identity);
                directorController = aiPrefab.GetComponent<StateController>();

                if(nightGlobalVolume.profile.TryGet<ColorAdjustments>(out var colorAdj))
                {
                    colorAdj.active = true;
                }
            }
        }

        if(directorController == null)
        {
            yield return new WaitForSeconds(3f);
            StartCoroutine(CreateDirectorAI());
        }
    }

    public void DestroyDirectorAI()
    {
        if (directorController != null)
        {
            Destroy(directorController.gameObject);
            directorController = null;
            
            if(nightGlobalVolume.profile.TryGet<ColorAdjustments>(out var colorAdj))
           {
                colorAdj.active = false;
           }

           StartCoroutine(CreateDirectorAI());
        }
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