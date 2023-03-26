using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class AiRedHoodController : MonoBehaviour
{
    public static AiRedHoodController inst;
    [SerializeField] GameObject redHoodPrefab;
    [SerializeField] Transform respawnPosition;
    Transform spawnPosition;

    [HideInInspector]
    public StateController redHoodController;

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
        
        StartCoroutine(CreateRedHoodAI());
    }

    public IEnumerator CreateRedHoodAI()
    {
        yield return new WaitUntil(() => isPlayerMove);

        if(spawnAI && isPlayerInSpawnCollider && spawnPosition != null && redHoodController == null)
        {
            var rand = Random.Range(0,100);
            if(rand >= 75)
            {
                // UITransition.inst.RedHoodTransitionIn();
                CreateRedHood();
            }
        }

        if(redHoodController == null)
        {
            yield return new WaitForSeconds(3f);
            StartCoroutine(CreateRedHoodAI());
        }
    }

    public void CreateRedHood()
    {
        var aiPrefab = Instantiate(redHoodPrefab,spawnPosition.position,Quaternion.identity);
        redHoodController = aiPrefab.GetComponent<StateController>();

        if(nightGlobalVolume.profile.TryGet<ColorAdjustments>(out var colorAdj))
        {
            colorAdj.active = true;
        }
    }

    public void DestroyRedHoodAI()
    {
        if (redHoodController != null)
        {
            Destroy(redHoodController.gameObject);
            redHoodController = null;
            
            if(nightGlobalVolume.profile.TryGet<ColorAdjustments>(out var colorAdj))
           {
                colorAdj.active = false;
           }

           StartCoroutine(CreateRedHoodAI());
        }
    }

    public void RedHoodTransition()
    {
        if (redHoodController == null)
            CreateRedHood();
        else
            DestroyRedHoodAI();
    }

    public void DestroyWhenEnterDoor()
    {
        DestroyRedHoodAI();
    }

    public void OnAttackPlayer()
    {
        UITransition.inst.DieTransitionIn();
    }

    public void RespawnPlayer()
    {
        if(redHoodController == null)
            return;

        DestroyRedHoodAI();
        PlayerManager.inst.transform.position = respawnPosition.position;
    }
}