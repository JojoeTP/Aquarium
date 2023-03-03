using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Diagnostics;
using System;
using System.IO;

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
    [SerializeField] Canvas SettingPanel;

    [Header("Participate ID")]
    [SerializeField] TMP_InputField inputParticipateID;

    Animator animator;

    private void Start() 
    {
        animator = GetComponent<Animator>();
        ChangeState(MenuState.MainMenu); 

        SoundManager.Inst.InitializeBGM(FMODEvent.inst.MainMenuMusic); 
    }

    void ChangeState(MenuState state)
    {
        currentState = state;

        switch(currentState)
        {
            case MenuState.MainMenu :
                loadingUI.EnableCavnas(false);
                break;
            case MenuState.StartGame :
                break;
            case MenuState.Setting :
                break;
        }
    }

    public void OnClickContinue()
    {
        SaveGameSystemManager.inst.LoadGame();
        ItemManager.Inst.ParticipateId = SaveGameSystemManager.inst.gameData.participateID;

        loadingUI.EnableCavnas(true);

        StartCoroutine(TransitionToGamePlay());    
    }

    public void OnClickNewGame()
    {
        animator.SetTrigger("OpenStartGame");
        ChangeState(MenuState.StartGame);
    }

    public void OnApplyParticipateID()
    {
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
        string subFolderPath = @"\LocalLow\DefaultCompany\Aquarium\PickUpItemTimeData\";
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
        
        SceneController.inst.GameplaySceneLoaded = false;
        AddressablesManager.inst.LoadSpriteAtlas();
        SceneController.inst.OnLoadSceneAsync(SceneController.inst.SCENE_GAMEPLAY,ActionBeforSwitchScene,ActionAfterSwitchScene);

    }

    void ActionBeforSwitchScene()
    {
        print("Before");
    }

    void ActionAfterSwitchScene()
    {
        ItemManager.Inst.SetUpItemPermutation();
        
        //SoundManager.Inst.InitializeBGM(FMODEvent.inst.InGameMusic); 
        SoundManager.Inst.StopBGM();

        SceneController.inst.GameplaySceneLoaded = true;
    }
}
