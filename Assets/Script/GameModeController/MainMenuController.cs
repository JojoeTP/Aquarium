using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuController : MonoBehaviour
{
    enum MenuState
    {
        MainMenu,
        StartGame,
        Setting
    }

    MenuState currentState;

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
                break;
            case MenuState.StartGame :
                break;
            case MenuState.Setting :
                break;
        }
    }

    public void OnClickContinue()
    {

    }

    public void OnClickNewGame()
    {
        animator.SetTrigger("OpenStartGame");
        ChangeState(MenuState.StartGame);
    }

    public void OnApplyParticipateID()
    {
        ItemManager.Inst.ParticipateId = int.Parse(inputParticipateID.text);
        StartCoroutine(TransitionToGamePlay());    
    }

    public void OnClickSetting()
    {
        animator.SetTrigger("OpenSetting");
        ChangeState(MenuState.Setting);
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

        SceneController.inst.OnLoadSceneAsync(SceneController.inst.SCENE_GAMEPLAY,ActionBeforSwitchScene,ActionAfterSwitchScene);

    }

    void ActionBeforSwitchScene()
    {
        print("Before");
    }

    void ActionAfterSwitchScene()
    {
        print("After");
        ItemManager.Inst.SetUpItemPermutation();

        //SoundManager.Inst.InitializeBGM(FMODEvent.inst.InGameMusic); 
        SoundManager.Inst.StopBGM();
    }
}
