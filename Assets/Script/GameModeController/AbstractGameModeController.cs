using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractGameModeController : MonoBehaviour
{
    public enum GameMode
    {
        INTRO,
        MAINMENU,
        GAMEPLAY,
        UIOVERLAY
    }

    public abstract string ControllerName {get;}
    public GameMode CurrentGameMode {get {return currentGameMode ;}}

    protected static GameMode currentGameMode = GameMode.INTRO;
    protected static GameMode nextGameMode;

    [Header("AbstractGameModeController")]
    public Canvas sceneCanves;

    protected SceneController sceneController;

    private void Awake() {
        sceneController = SceneController.Inst;
        
        if(sceneController != null)
            sceneController.AddGameController(ControllerName,this);
    }
    
    private void OnDestroy() 
    {
        sceneController.RemoveGameController(gameObject.scene.name);
    }
}
