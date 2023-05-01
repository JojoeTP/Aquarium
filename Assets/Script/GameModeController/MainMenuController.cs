using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Diagnostics;
using System;
using System.IO;
using System.Threading.Tasks;

public class MainMenuController : MonoBehaviour
{
    enum MenuState
    {
        MainMenu,
        StartGame,
        Setting
    }

    MenuState currentState;
    [SerializeField] LoadingUI loadingUI;

    [Header("Panel")]
    [SerializeField] Canvas MainMenuPanel;
    [SerializeField] Canvas StartGamePanel;
    [SerializeField] PlayerPanel SettingPanel;

    [Header("Participate ID")]
    [SerializeField] TMP_InputField inputParticipateID;

    [Header("Button")]
    [SerializeField] Button continueButton;

    public bool isNight;

    Animator animator;

    public bool gameStarted = false;

    private void Start() 
    {
        animator = GetComponent<Animator>();
        ChangeState(MenuState.MainMenu); 

        if(isNight)
            SoundManager.Inst.InitializeBGM(FMODEvent.inst.MainMenuMusic.sound); 
        else
            SoundManager.Inst.InitializeBGM(FMODEvent.inst.FModEventDictionary["MainTitle1"]); 
    }

    void ChangeState(MenuState state)
    {
        currentState = state;

        switch(currentState)
        {
            case MenuState.MainMenu :
                loadingUI.EnableCavnas(false);
                EnableContinueButton();
                break;
            case MenuState.StartGame :
                break;
            case MenuState.Setting :
                break;
        }
    }

    void EnableContinueButton()
    {
        var isSaved = PlayerPrefs.GetInt("IsSaved",0);
        if(isSaved == 0)
            continueButton.interactable = false;
        else if(isSaved == 1)
            continueButton.interactable = true;
    }

    public void OnClickContinue()
    {
        if(gameStarted) return;
        if(PlayerPrefs.GetInt("IsSaved",0) == 0) return;
        
        gameStarted = true;
        SaveGameSystemManager.inst.LoadGame();
        ItemManager.Inst.ParticipateId = SaveGameSystemManager.inst.gameData.participateID;

        loadingUI.EnableCavnas(true);

        ItemManager.Inst.isContinue = true;
        StartCoroutine(TransitionToGamePlay());    
    }

    public void OnClickNewGame()
    {
        animator.SetTrigger("OpenStartGame");
        ChangeState(MenuState.StartGame);
    }

    public void OnApplyParticipateID()
    {
        if(gameStarted) return;
        if(inputParticipateID.text == null) return;
        if(inputParticipateID.text == "") return;

        gameStarted = true;
        SaveGameSystemManager.inst.StartNewGame();
        ItemManager.Inst.ParticipateId = int.Parse(inputParticipateID.text);

        loadingUI.EnableCavnas(true);

        StartCoroutine(TransitionToGamePlay());    
    }

    public void OnClickSetting()
    {
        animator.SetTrigger("OpenSetting");
        ChangeState(MenuState.Setting);
    }

    public void OnClikeJsonFile()
    {
        string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        appDataPath = appDataPath.Replace(@"\Roaming","");
        string subFolderPath = @"\LocalLow\TechThesis\Liberate\Liberate\PickUpItemTimeData\";
        string folderPath = appDataPath + subFolderPath;
        Process.Start(folderPath);
    }

    public void OnClickExit()
    {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void OnBack()
    {
        animator.SetTrigger("Close");
        ChangeState(MenuState.MainMenu);
    }

    IEnumerator TransitionToGamePlay()
    {
        yield return null;
        // maybe switch to loading scene before switch to scene game
        yield return new WaitUntil(() => DialogueManager.inst.IsLoadingDialogueData());

        InputSystemManager.Inst.SetPlayerControl(true);
        SceneController.inst.GameplaySceneLoaded = false;
        AddressablesManager.inst.LoadSpriteAtlas();
        SceneController.inst.OnLoadSceneAsync(SceneController.inst.SCENE_GAMEPLAY,ActionBeforSwitchScene,ActionAfterSwitchScene);

    }

    void ActionBeforSwitchScene()
    {
        SoundManager.Inst.CleanUp();
    }

    void ActionAfterSwitchScene()
    {
        ItemManager.Inst.SetUpItemPermutation();
        
        //SoundManager.Inst.InitializeBGM(FMODEvent.inst.InGameMusic); 
        SoundManager.Inst.StopBGM();

        SceneController.inst.GameplaySceneLoaded = true;

        ActionEventManager.inst.SetActiveDialogueCh0_C01_01();
        
        if(SaveGameSystemManager.inst.isLoad)
        {
            SaveGameSystemManager.inst.SetIsMapDone();
            ActionEventManager.inst.LoadingGame();

            PlayerManager.inst.playerState = PlayerManager.PLAYERSTATE.NONE;
        }
    }

    public void PlaySound()
    {
        SoundManager.Inst.InitializeUI(FMODEvent.inst.FModEventDictionary["Setting"]);
    }
}
