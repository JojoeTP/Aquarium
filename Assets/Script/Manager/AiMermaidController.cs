using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class AiMermaidController : MonoBehaviour
{
    public static AiMermaidController inst;
    [SerializeField] GameObject mermaidPrefab;
    [SerializeField] Transform respawnPosition;
    [SerializeField] Transform firstSpawnPosition;
    [SerializeField] List<Transform> spawnPositionList;
    StateController mermaidController;

    [SerializeField] Volume nightGlobalVolume;

    public bool spawnAI = true; //Turn to false when talk with director
    public float spawnTime = 0f;

    void Awake() 
    {
        inst = this;
    }

    public void CreateMermaidAIAtFirstPosition()
    {
        var aiPrefab = Instantiate(mermaidPrefab,firstSpawnPosition.position,Quaternion.identity);
        mermaidController = aiPrefab.GetComponent<StateController>();

        if(nightGlobalVolume.profile.TryGet<ColorAdjustments>(out var colorAdj))
        {
            colorAdj.active = true;
        }
    }

    void CreateMermaidAI()
    {
        var aiPrefab = Instantiate(mermaidPrefab,spawnPositionList[Random.Range(0,spawnPositionList.Count)].position,Quaternion.identity);
        mermaidController = aiPrefab.GetComponent<StateController>();

        if(nightGlobalVolume.profile.TryGet<ColorAdjustments>(out var colorAdj))
        {
            colorAdj.active = true;
        }
    }
    

    public void SpawnAI()
    {
        UITransition.inst.MermaidTransitionIn();
    }

    public IEnumerator CreateMermaidAI(float time)
    {
        yield return new WaitForSeconds(time);

        if(mermaidController == null && spawnAI)
        {
            UITransition.inst.MermaidTransitionIn();
        }
    }
    
    public void DestroyMermaidAI()
    {
        if (mermaidController != null)
        {
            Destroy(mermaidController.gameObject);
            mermaidController = null;
            
            if(nightGlobalVolume.profile.TryGet<ColorAdjustments>(out var colorAdj))
           {
                colorAdj.active = false;
           }
        }
    }

    public void MermaidTransition()
    {
        if (mermaidController == null)
            CreateMermaidAI();
        else
            DestroyMermaidAI();
    }

    public void DestroyWhenEnterLift()
    {
        DestroyMermaidAI();
        StartCoroutine(CreateMermaidAI(5f));
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.GetComponent<PlayerManager>() != null)
        {
            if(mermaidController != null)
                UITransition.inst.MermaidTransitionIn();
        }
    }

    void OnTriggerExit2D(Collider2D other) 
    {
        if(other.GetComponent<PlayerManager>() != null)
        {
            StartCoroutine(CreateMermaidAI(spawnTime));
        }
    }

    public void OnAttackPlayer()
    {
        UITransition.inst.DieTransitionIn();
    }

    public void RespawnPlayer()
    {
        if(mermaidController == null)
            return;
            
        DestroyMermaidAI();
        PlayerManager.inst.transform.position = respawnPosition.position;
    }

    public void SetActiveAI(bool isActive)
    {
        if(mermaidController != null)
            mermaidController.enabled = isActive;
    }
}

#if UNITY_EDITOR
    [CustomEditor(typeof(AiMermaidController))]
    public class AiMermaidControllerTester : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            AiMermaidController actionActive = (AiMermaidController)target;

            if (GUILayout.Button("Spawn Mermaid"))
            {
                actionActive.SpawnAI();
            }
        }
    }
#endif
