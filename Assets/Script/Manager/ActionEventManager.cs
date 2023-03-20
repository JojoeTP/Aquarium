using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class ActionEventManager : MonoBehaviour
{
    public static ActionEventManager inst;

    [Header("Puzzle")]
    [SerializeField] SpriteRenderer labyrinthENDSpriteRenderer;
    [SerializeField] Sprite dark_LabyrinthSprite;
    [HideInInspector]
    public bool isPuzzleDone = false;

    [Header("Enemy")]
    Transform spawnPosition;
    [SerializeField] GameObject skeletonPrefab;
    [SerializeField] GameObject mermaidPrefab;
    [SerializeField] GameObject directorPrefab;

    [Header("Brother Sister")]
    [SerializeField] GameObject brotherPrefab;
    [SerializeField] Transform spawnSisterPosition;
    [SerializeField] GameObject sisterPrefab;
    [SerializeField] GameObject alertCanvas;

    [Header("LockDoor")]
    [SerializeField] LockDoorConfig Ch0_C03_01_Config;

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
        SoundManager.Inst.MuteBGM(); //ปิด BGM
        //จะปิดไรเพิ่มก็ เพิ่มcodeตรงนี้
    }
#endregion
    public void UpdateSpawnPosition(Transform newSpawnPosition)
    {
        spawnPosition = newSpawnPosition;
    }
    public StateController SpawnEnemy(GameObject enemy)
    {
        return Instantiate(enemy,spawnPosition.transform.position,spawnPosition.transform.rotation).GetComponent<StateController>();
    }

    public void SpawnSkeleton()
    {
        skeleton = SpawnEnemy(skeletonPrefab);
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

    public void DeleteSavePlayerPrefs()
    {
        PlayerPrefs.DeleteKey("IsSaved");
    }

    public void ContinuePlayBGM()
    {
        SoundManager.Inst.ContinuePlayBGM();
        //เพิ่มตรงนี้ให้เล่นต่อ
    }

    public void UnLockDoor_Ch0_C03_01()
    {
        foreach(var n in Ch0_C03_01_Config.lockDoorDialogue)
        {
            Destroy(n.gameObject);
        }
        Ch0_C03_01_Config.lockDoorDialogue.Clear();

        foreach(var n in Ch0_C03_01_Config.lockDoor)
        {
            n.isLockedDoor = false;
        }
    }

    public void WarpToPosition(Transform transform)
    {
        PlayerManager.inst.transform.position = transform.position;
    }
}

[Serializable]
public class LockDoorConfig
{
    public List<TalkWithNPC> lockDoorDialogue = new List<TalkWithNPC>();
    public List<DoorSystem> lockDoor = new List<DoorSystem>();
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
            
            if (GUILayout.Button("Test Delete Save"))
            {
                Debug.Log("Delete COMPLETE");
                actionActive.DeleteSavePlayerPrefs();
            }

            if (GUILayout.Button("Test Labyrinth END"))
            {
                actionActive.OnPickUpLabyrinthCoin();
            }

            if (GUILayout.Button("Continue Play BGM"))
            {
                actionActive.ContinuePlayBGM();
            }
        }
    }
#endif
