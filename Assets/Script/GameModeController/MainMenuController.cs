using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuController : AbstractGameModeController
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

    public override string ControllerName 
    {
        get
        {
            return "Scene_MainMenu";
        }
    }

    private void Start() 
    {
        animator = GetComponent<Animator>();
        ChangeState(MenuState.MainMenu);    
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
        // ItemManager.Inst.SetUpItemPermutation();
        yield return new WaitForSeconds(1f);

        SceneController.Inst.LoadStreamingScene(SceneController.Inst.SCENE_GAMEPLAY);
        while(true)
        {
            var gameplayController = SceneController.Inst.GetGameController(SceneController.Inst.SCENE_GAMEPLAY) as GameplayController;
            if(gameplayController == null)
            {
                yield return null;
            }
            else
            {
                break;
            }
        }

        yield return new WaitUntil(() => ((GameplayController)SceneController.Inst.GetGameController(SceneController.Inst.SCENE_GAMEPLAY)).IsInitialized);

        SceneController.Inst.SetSceneActive(SceneController.Inst.SCENE_GAMEPLAY);
        
        ItemManager.Inst.SetUpItemPermutation();
        

        //When all Setting run finish then unload mainmenu scene
        SceneController.Inst.UnLoadStreamingScene(SceneController.Inst.SCENE_MAINMENU);
        
        //run function must do when start aquarium scene



    }
}
