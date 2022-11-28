using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public string SCENE_GAMEPLAY { get {return "Scene_Aquarium";} }
    public string SCENE_MAINMENU { get {return "Scene_MainMenu";} }
    public static SceneController Inst;

    readonly Dictionary<string,AbstractGameModeController> gameModeControllers = new Dictionary<string, AbstractGameModeController>();
    private AbstractGameModeController currentActiveController;
    public bool IsLoadingScene { get; private set; }
    
    private void Awake()
    {
        if (Inst == null)
            Inst = this;
    }

    void Start()
    {
        //load mainmenu
        LoadSceneOverlay(SCENE_MAINMENU);
    }

#region  GameModeController
    public void AddGameController(string sceneName,AbstractGameModeController controller )
    {
        if(!gameModeControllers.ContainsKey(sceneName))
        {
            gameModeControllers.Add(sceneName,controller);
        }
    }

    public void RemoveGameController(string sceneName)
    {
        if(gameModeControllers.ContainsKey(sceneName))
            gameModeControllers.Remove(sceneName);
    }

    public AbstractGameModeController GetGameController(string sceneName)
    {
        return gameModeControllers.ContainsKey(sceneName) ? gameModeControllers[sceneName] : null;
    }

    public T GetGameController<T>()
    {
        foreach(var controller in gameModeControllers)
        {
            object value = controller.Value;
            if(value.GetType() == typeof(T))
                return (T)Convert.ChangeType(value, typeof(T));
        }
        return default;
    }

    public AbstractGameModeController GetCurrentActiveController()
    {
        return currentActiveController;
    }

#endregion

#region loadScene
    public void LoadSceneOverlay(string sceneName)
    {
        StartCoroutine(DoLoadSceneOverlay(sceneName));
    }

    public void LoadStreamingScene (string sceneName)
    {
        StartCoroutine (LoadSceneAsync (sceneName));
    }

    public void UnLoadStreamingScene (string sceneName)
    {
        StartCoroutine (UnLoadSceneAsync (sceneName));
    }

    public void SetSceneActive(string sceneName)
    {
        Scene newlyLoadedScene = SceneManager.GetSceneByName(sceneName);
        if(newlyLoadedScene.isLoaded)
            SceneManager.SetActiveScene(newlyLoadedScene);
        
        AbstractGameModeController controller = gameModeControllers[SceneManager.GetActiveScene().name];
        currentActiveController = controller;
    }

    public bool IsSceneLoaded(string sceneName)
    {
        Scene newlyLoadedScene = SceneManager.GetSceneByName(sceneName);
        if(newlyLoadedScene.buildIndex == -1)
        {
            return false;
        }

        return true;
    }

    private IEnumerator DoLoadSceneOverlay (string sceneName)
    {
        // prevScene = SceneManager.GetActiveScene();

        IsLoadingScene = true;
        
        yield return SceneManager.LoadSceneAsync (sceneName, LoadSceneMode.Additive);

        Scene newlyLoadedScene = SceneManager.GetSceneAt (SceneManager.sceneCount - 1);

        IsLoadingScene = false;
        SceneManager.SetActiveScene (newlyLoadedScene);
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        IsLoadingScene = true;
        yield return SceneManager.LoadSceneAsync (sceneName, LoadSceneMode.Additive);
        IsLoadingScene = false;
    }

    private IEnumerator UnLoadSceneAsync(string sceneName)
    {
        yield return SceneManager.UnloadSceneAsync (sceneName);
    }
#endregion

    
}
