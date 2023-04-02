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

    [Header("Circus Coin")]
    [SerializeField] List<TalkWithNPC> CircusCoinShowDialogue;
    [SerializeField] List<TalkWithNPC> CircusCoinHideDialogue;
    
    [HideInInspector]
    public bool isPuzzleDone = false;

    [Header("Aquariam")]
    [SerializeField] GameObject LockDoorEndGame;

    [Header("Brother Sister")]
    [SerializeField] Transform spawnSisterPosition;
    [SerializeField] GameObject alertCanvas;

    [Header("Wall")]
    [SerializeField] GameObject Wall_Cafeteria;
    [SerializeField] GameObject Wall_Labyrinth;
    [SerializeField] GameObject Wall_Circus;

    [Header("Dialogue & Cutescene")]
    [SerializeField] TalkWithNPC Ch0_C01_01;
    [SerializeField] TalkWithNPC Ch1_D11_01; 
    [SerializeField] TalkWithNPC Ch1_D03_01; 
    [SerializeField] TalkWithNPC Ch1_D06_01; 
    [SerializeField] TalkWithNPC Ch1_D10_01;
    [SerializeField] TalkWithNPC Ch2_D05_01;
    [SerializeField] TalkWithNPC Ch3_D05_01;
    [SerializeField] TalkWithNPC Ch3_D08_01;
    [SerializeField] TalkWithNPC Ch3_D09_01;
    [SerializeField] TalkWithNPC Ch4_D03_01;
    [SerializeField] TalkWithNPC Ch4_D05_01; 
    [SerializeField] TalkWithNPC Ch4_D06_01;  
    [SerializeField] TalkWithNPC Ch4_D07_01;  

    [Header("LockDoor")]
    [SerializeField] DoorSystem cafeteriaDoor;
    [SerializeField] DoorSystem labyrinthDoor;
    [SerializeField] DoorSystem CircusDoor;
    [SerializeField] DoorSystem aquariamDoor;

    [Header("UnLockDoor")]
    [SerializeField] LockDoorConfig Ch0_C03_01_Config;
    [SerializeField] LockDoorConfig Ch1_D01_2_01_Config;
    [SerializeField] LockDoorConfig Ch1_D04_01_Config;
    [SerializeField] LockDoorConfig Ch3_D04_01_Config;

    [Header("Item")]
    [SerializeField] ItemScriptableObject VIPRoom;

    [Header("Puzzle")]
    [SerializeField] DoorEncryption doorEncryption;

    [Header("Dialogue Parent")]
    [SerializeField] GameObject Dialogue_ch0;
    [SerializeField] GameObject Dialogue_ch1;
    [SerializeField] GameObject Dialogue_ch2;
    [SerializeField] GameObject Dialogue_ch3;
    [SerializeField] GameObject Dialogue_ch4;

    [Header("IsMapDone")]
    public bool isMap1Done = false;
    public bool isMap2Done = false;
    public bool isMap3Done = false;
    public bool isMap4Done = false;

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

        SetActiveDialogueCh2_D05_01();
        //จะปิดไรเพิ่มก็ เพิ่มcodeตรงนี้
    }

    public void OnPickUpCircusCoin()
    {
        foreach(var n in CircusCoinShowDialogue)
        {
            n.gameObject.SetActive(true);
        }

        foreach(var n in CircusCoinHideDialogue)
        {
            n.SetActiveFalse();
        }

        SetActiveFalse_Wall_Circus();

        //AiRedHoodController.inst.spawnAI = false;
    }

#endregion

    public void SpawnSkeleton(Transform newSpawnPosition)
    {
        AiJunitorController.inst.SpawnPosition = newSpawnPosition;
        AiJunitorController.inst.CreateJunitor();
        AiJunitorController.inst.spawnAI = true;
    }

    public void WarpSkeletonAfterHiding(Transform pos)
    {
        StartCoroutine(WarpSkeleton(pos));
    }
    
    public IEnumerator WarpSkeleton(Transform pos)
    {
        yield return new WaitForSeconds(2f);

        AiJunitorController.inst.junitorController.transform.position = pos.position;
        EnableAISkeleton(true);
    }

    public void CannotExitHidingSpot()
    {
        AiJunitorController.inst.CannotExitHiding = true;
    }

    public void EnableAISkeleton(bool value)
    {
        AiJunitorController.inst.SetActiveAI(value);
    }

    public void EnableAIMermaid(bool value)
    {
        AiMermaidController.inst.SetActiveAI(value);
    }

    public void SpawnSisterAndAlert()
    {
        AiRedHoodController.inst.spawnAI = true;
        ActionEventManager.inst.AlertText(10.0f);
        ActionEventManager.inst.SpawnSister(false, 10.0f);
    }

    public void SpawnSister(bool isSpawn , float delayBeforeSpawn)
    {
        if (isSpawn == false)
        {
            StartCoroutine(DelaySpawnSister(delayBeforeSpawn));
        }
    }

    IEnumerator DelaySpawnSister(float time)
    {
        yield return new WaitForSeconds(time);
        AiRedHoodController.inst.spawnPosition = spawnSisterPosition;
        AiRedHoodController.inst.CreateRedHood();
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

    public void MermaidTrigger()
    {
        AiMermaidController.inst.spawnAI = true;
        AiMermaidController.inst.CreateMermaidAIAtFirstPosition();
    }

    public void DisableMermaidSpawn()
    {
        AiMermaidController.inst.spawnAI = false;
        AiMermaidController.inst.DestroyMermaidAI();
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

    void LockDoorAfterGetCoin(DoorSystem door)
    {
        door.isLockedDoor = true;
    }

    public void LockCafeteriaDoor()
    {
        LockDoorAfterGetCoin(cafeteriaDoor);
    }

    public void LockLabyrinthDoor()
    {
        LockDoorAfterGetCoin(labyrinthDoor);
    }

    public void LockCircusDoor()
    {
        LockDoorAfterGetCoin(CircusDoor);
    }

    public void LockAquariamDoor()
    {
        LockDoorAfterGetCoin(aquariamDoor);
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

    public void UnLockDoor_Ch3_D04_01_Config()
    {
        UnlockDoor(Ch3_D04_01_Config);
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

    // public void SetActiveDialogueCh2_D02_01()
    // {
    //     SetActiveDialogue(Ch2_D02_01);
    // }

    public void SetActiveFalse_Wall_Cafeteria()
    {
        Wall_Cafeteria.SetActive(false);
    }

    public void SetActiveFalse_Wall_Labyrinth()
    {
        Wall_Labyrinth.SetActive(false);
    }

    public void SetActiveFalse_Wall_Circus()
    {
        Wall_Circus.SetActive(false);
    }

    public void SetActiveDialogueCh1_D03_01()
    {
        SetActiveDialogue(Ch1_D03_01);
    }

    public void SetActiveDialogueCh0_C01_01()
    {
        SetActiveDialogue(Ch0_C01_01);
    }

    public void SetActiveDialogueCh3_D05_01()
    {
        SetActiveDialogue(Ch3_D05_01);
    }

    public void SetActiveDialogueCh4_D03_01()
    {
        SetActiveDialogue(Ch4_D03_01);
    }

    public void SetActiveDialogueCh4_D05_01()
    {
        SetActiveDialogue(Ch4_D05_01);
    }

    public void SetActiveDialogueCh4_D06_01()
    {
        SetActiveDialogue(Ch4_D06_01);
    }

    public void SetActiveDialogueCh4_D07_01()
    {
        SetActiveDialogue(Ch4_D07_01);
    }
    
    public void SetActiveDialogueCh2_D05_01()
    {
        SetActiveDialogue(Ch2_D05_01);
    }
    
    public void SetActiveDialogueCh3_D08_01()
    {
        SetActiveDialogue(Ch3_D08_01);
    }

    public void SetActiveDialogueCh1_D10_01()
    {
        SetActiveDialogue(Ch1_D10_01);
    }

    public void SetActiveDialogueCh3_D09_01()
    {
        SetActiveDialogue(Ch3_D09_01);
    }
    

    public void SetActiveLockDoorDialogueEndGame()
    {
        LockDoorEndGame.SetActive(true);
    }

    public void ChangeDialogueCh1_D06_01()
    {
        Ch1_D06_01.ChangeDialogueId("Ch1_D08_01");
        Ch1_D06_01.triggerEvents.AddListener(() => PlayerManager.inst.PlayerInventory.AddItem(VIPRoom));
        Ch1_D06_01.triggerEvents.AddListener(Ch1_D06_01.SetActiveFalse);
    }

    public void StartPuzzle()
    {
        doorEncryption.StartPuzzle();
    }

    public void EnterWell()
    {
        PlayerPrefs.SetInt("DarkMainMenu",1);
        PlayerManager.inst.playerAnimator.SetBool("Lampitem",true);
    }
        public string SCENE_MAINMENU { get {return "Scene_MainMenu";} }
        public string SCENE_MAINMENU_2 { get {return "Scene_MainMenu2";} }

    public void BackToMainMenu()
    {
        if(PlayerPrefs.GetInt("DarkMainMenu",0) == 0)
            SceneController.inst.OnLoadSceneAsync(SCENE_MAINMENU,null,null);
        else
            SceneController.inst.OnLoadSceneAsync(SCENE_MAINMENU_2,null,null);
    }

    public void WhiteTransition()
    {
        UITransition.inst.WhiteTransitionIn();
    }

    public void LoadingGame()
    {
        Dialogue_ch0.SetActive(false);

        if(isMap1Done)
        {
            Wall_Cafeteria.SetActive(false);
            LockCafeteriaDoor();
            Dialogue_ch1.SetActive(false);
        }

        if(isMap2Done)
        {
            Wall_Labyrinth.SetActive(false);
            LockLabyrinthDoor();
            Dialogue_ch2.SetActive(false);
        }

        if(isMap3Done)
        {
            Wall_Circus.SetActive(false);
            LockCircusDoor();
            Dialogue_ch3.SetActive(false);
        }

        if(isMap4Done)
        {
            LockAquariamDoor();
            Dialogue_ch4.SetActive(false);
        }
        
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
