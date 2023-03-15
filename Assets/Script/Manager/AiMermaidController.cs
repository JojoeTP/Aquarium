using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AiMermaidController : MonoBehaviour
{
    public static AiMermaidController inst;
    [SerializeField] GameObject mermaidPrefab;
    [SerializeField] Transform firstSpawnPosition;
    [SerializeField] List<Transform> spawnPositionList;
    StateController mermaidController;

    [SerializeField] Material waterMaterial;

    [ColorUsage(true,true)]
    [SerializeField] Color defaultWaterColor;
    [ColorUsage(true,true)]
    [SerializeField] Color redWaterColor;

    public bool spawnAI = true; //Turn to false when talk with director

    void Awake() 
    {
        inst = this;
    }

    public void CreateMermaidAI()
    {
        var aiPrefab = Instantiate(mermaidPrefab,firstSpawnPosition.position,Quaternion.identity);
        mermaidController = aiPrefab.GetComponent<StateController>();

    }

    IEnumerator CreateMermaidAI(float time)
    {
        yield return new WaitForSeconds(time);

        if(mermaidController == null && spawnAI)
        {
            var aiPrefab = Instantiate(mermaidPrefab,spawnPositionList[Random.Range(0,spawnPositionList.Count)].position,Quaternion.identity);
            mermaidController = aiPrefab.GetComponent<StateController>();

            waterMaterial.SetColor("_Color",redWaterColor);
        }
    }
    
    public void DestroyMermaidAI()
    {
        if (mermaidController != null)
        {
            Destroy(mermaidController.gameObject);
            mermaidController = null;
            waterMaterial.SetColor("_Color",defaultWaterColor);
        }
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
            DestroyMermaidAI();
        }
    }

    void OnTriggerExit2D(Collider2D other) 
    {
        if(other.GetComponent<PlayerManager>() != null)
        {
            StartCoroutine(CreateMermaidAI(5f));
        }
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
                actionActive.CreateMermaidAI();
            }
        }
    }
#endif
