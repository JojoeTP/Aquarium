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
    [SerializeField] List<TalkWithNPC> labyrinthENDShowDialogue;
    [SerializeField] List<TalkWithNPC> labyrinthENDHideDialogue;
    
    [HideInInspector]
    public bool isPuzzleDone = false;

    [Header("Enemy")]
    [SerializeField] GameObject skeletonPrefab;
    [SerializeField] GameObject mermaidPrefab;
    [SerializeField] GameObject directorPrefab;
    Transform spawnPosition;

    [Header("Brother Sister")]
    [SerializeField] GameObject brotherPrefab;
    [SerializeField] Transform spawnSisterPosition;
    [SerializeField] GameObject sisterPrefab;
    [SerializeField] GameObject alertCanvas;

    [Header("Wall")]
    [SerializeField] GameObject Wall_Cafeteria;
    [SerializeField] GameObject Wall_Labyrinth;
    [SerializeField] GameObject Wall_Aquarium;

    [Header("Dialogue & Cutescene")]
    [SerializeField] TalkWithNPC Ch1_D11_01; 
    [SerializeField] TalkWithNPC Ch1_D03_01; 
    [SerializeField] TalkWithNPC Ch1_D06_01; 

    [Header("LockDoor")]
    [SerializeField] LockDoorConfig Ch0_C03_01_Config;
    [SerializeField] LockDoorConfig Ch1_D01_2_01_Config;
    [SerializeField] LockDoorConfig Ch1_D04_01_Config;

    [Header("Item")]
    [SerializeField] ItemScriptableObject VIPRoom;

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

        foreach(var n in labyrinthENDShowDialogue)
        {
            n.gameObject.SetActive(true);
        }

        foreach(var n in labyrinthENDHideDialogue)
        {
            n.SetActiveFalse();
        }
        SetActiveFalse_Wall_Labyrinth();

        labyrinthENDSpriteRenderer.sprite = dark_LabyrinthSprite;
        SoundManager.Inst.MuteBGM();
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

    public void SpawnSkeleton(Transform newSpawnPosition)
    {
        AiJunitorController.inst.SpawnPosition = newSpawnPosition;
        AiJunitorController.inst.CreateJunitor();
        AiJunitorController.inst.spawnAI = true;
    }

    public void EnableAISkeleton(bool value)
    {
        AiJunitorController.inst.SetActiveAI(value);
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
    }

    void UnlockDoor(LockDoorConfig lockDoorConfig)
    {
        foreach(var n in lockDoorConfig.lockDoorDialogue)
        {
            Destroy(n.gameObject);
        }
        lockDoorConfig.lockDoorDialogue.Clear();

        foreach(var n in lockDoorConfig.lockDoor)
        {
            n.isLockedDoor = false;
        }
    }

    public void UnLockDoor_Ch0_C03_01()
    {
        UnlockDoor(Ch0_C03_01_Config);
    }

    public void UnLockDoor_Ch1_D01_2_01()
    {
        UnlockDoor(Ch1_D01_2_01_Config);
    }

    public void UnLockDoor_Ch1_D04_01()
    {
        UnlockDoor(Ch1_D04_01_Config);
    }

    public void WarpToPosition(Transform transform)
    {
        PlayerManager.inst.transform.position = transform.position;
    }

    void SetActiveDialogue(TalkWithNPC talkWithNPC)
    {
        talkWithNPC.gameObject.SetActive(true);
    }

    public void SetActiveDialogueCh1_D11_01()
    {
        SetActiveDialogue(Ch1_D11_01);
    }

    public void SetActiveFalse_Wall_Cafeteria()
    {
        Wall_Cafeteria.SetActive(false);
    }

    public void SetActiveFalse_Wall_Labyrinth()
    {
        Wall_Labyrinth.SetActive(false);
    }

    public void SetActiveFalse_Wall_Aquarium()
    {
        Wall_Aquarium.SetActive(false);
    }

    public void SetActiveDialogueCh1_D03_01()
    {
        SetActiveDialogue(Ch1_D03_01);
    }

    public void ChangeDialogueCh1_D06_01()
    {
        Ch1_D06_01.ChangeDialogueId("Ch1_D08_01");
        Ch1_D06_01.triggerEvents.AddListener(() => PlayerManager.inst.PlayerInventory.AddItem(VIPRoom));
        Ch1_D06_01.triggerEvents.AddListener(Ch1_D06_01.SetActiveFalse);
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
