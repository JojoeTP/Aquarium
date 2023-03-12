using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AiMermaidController : MonoBehaviour
{
    [SerializeField] GameObject mermaidPrefab;
    [SerializeField] Transform firstSpawnPosition;
    [SerializeField] List<Transform> spawnPositionList;
    StateController mermaidController;

    public bool spawnAI = true; //Turn to false when talk with director

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
        }
    }
    
    public void DestroyMermaidAI()
    {
        if (mermaidController != null)
        {
            Destroy(mermaidController.gameObject);
            mermaidController = null;
        }
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
