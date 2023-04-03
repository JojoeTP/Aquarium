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
    [HideInInspector]
    public Transform spawnPosition;

    [HideInInspector]
    public StateController redHoodController;

    [SerializeField] Volume nightGlobalVolume;
    
    public bool spawnAI = false;
    public bool isPlayerInSpawnCollider = false;
    public bool isPlayerMove = false;

    public float spawnTime = 0f;

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

        yield return new WaitForSeconds(spawnTime);
        
        if(spawnAI && isPlayerInSpawnCollider && spawnPosition != null && redHoodController == null)
        {
            var rand = Random.Range(0,100);
            if(rand >= 0)
            {
                // UITransition.inst.RedHoodTransitionIn();
                CreateRedHood();
            }
        }

        if(redHoodController == null)
        {
            yield return new WaitForSeconds(spawnTime);
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

        SoundManager.Inst.PlayOneShot(FMODEvent.inst.FModEventDictionary["Monster_spawner_sound"],aiPrefab.transform.position);
    }

    public void DestroyRedHoodAI()
    {
        if (redHoodController != null)
        {
            SoundManager.Inst.StopMonster();
            Destroy(redHoodController.gameObject);
            redHoodController = null;
            ActionEventManager.inst.UnLockDoor_Ch3_D06_01_Config();
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
        StartCoroutine(AttackPlayer(2f));
    }

    IEnumerator AttackPlayer(float time)
    {
        yield return new WaitForSeconds(time);
        UITransition.inst.DieTransitionIn();
    }

    public void RespawnPlayer()
    {
        if(redHoodController == null)
            return;

        DestroyRedHoodAI();
        PlayerManager.inst.transform.position = respawnPosition.position;
        ActionEventManager.inst.SpawnSisterAndAlert();
        
    }
}