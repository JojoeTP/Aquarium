using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ActionEventManager : MonoBehaviour
{
    public static ActionEventManager inst;

    [Header("Puzzle")]
    [SerializeField] SpriteRenderer labyrinthENDSpriteRenderer;
    [SerializeField] Sprite dark_LabyrinthSprite;
    [HideInInspector]
    public bool isPuzzleDone = false;

    [Header("Enemy")]
    [SerializeField] Transform spawnPosition;
    [SerializeField] GameObject janitorPrefab;
    [SerializeField] GameObject mermaidPrefab;
    [SerializeField] GameObject directorPrefab;

    [Header("Brother Sister")]
    [SerializeField] GameObject brotherPrefab;
    [SerializeField] Transform spawnSisterPosition;
    [SerializeField] GameObject sisterPrefab;
    [SerializeField] GameObject alertCanvas;

    [HideInInspector]
    public StateController skeleton; //ภาโรง
    [HideInInspector]
    public StateController sister;
    

    void Awake() 
    {
        inst = this;
    }

#region ItemActionEvent
    public void OnPickUpLabyrinthCoin()
    {
        isPuzzleDone = true;
        labyrinthENDSpriteRenderer.sprite = dark_LabyrinthSprite;
        //ปิดเสียงด้วย
    }
#endregion
    void UpdateSpawnPosition(Transform newSpawnPosition)
    {
        spawnPosition = newSpawnPosition;
    }
    public StateController SpawnEnemy(GameObject enemy)
    {
        return Instantiate(enemy,spawnPosition.transform.position,spawnPosition.transform.rotation).GetComponent<StateController>();
    }

    public void SpawnSister(bool isSpawn , float delayBeforeSpawn)
    {
        if (isSpawn == false)
        {
            UpdateSpawnPosition(spawnSisterPosition);
            StartCoroutine(DelaySpawnSister(delayBeforeSpawn));
        }
    }
    IEnumerator DelaySpawnSister(float time)
    {
        yield return new WaitForSeconds(time);
        sister = SpawnEnemy(sisterPrefab);
    }

    public void AlertText(float time)
    {
        alertCanvas.SetActive(true);
        StartCoroutine(DelayCloseAlertText(time));
    }

    IEnumerator DelayCloseAlertText(float time)
    {
        yield return new WaitForSeconds(time);
        alertCanvas.SetActive(false);
    }
    public void CutSceneDoor()
    {
        print("CutSceneDoor");
    }

    public void CutSceneHidingSpot()
    {
        print("CutSceneHidingSpot");
    }

    public void TestSaveGame()
    {
        SaveGameSystemManager.inst.SaveGame();
    }
}

#if UNITY_EDITOR
    [CustomEditor(typeof(ActionEventManager))]
    public class ActionEventTester : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            ActionEventManager actionActive = (ActionEventManager)target;

            if (GUILayout.Button("Test Save Game"))
            {
                Debug.Log("SAVE COMPLETE");
                actionActive.TestSaveGame();
            }


            if (GUILayout.Button("Test Labyrinth END"))
            {
                actionActive.OnPickUpLabyrinthCoin();
            }
        }
    }
#endif
