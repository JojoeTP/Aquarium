using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class SceneController : MonoBehaviour
{
    public static SceneController inst;
    public string SCENE_GAMEPLAY { get {return "Scene_Aquarium";} }
    public string SCENE_MAINMENU { get {return "Scene_MainMenu";} } 
    public string SCENE_MAINMENU_2 { get {return "Scene_MainMenu2";} } 
    public string SCENE_LOADING { get {return "Scene_Loading";} }

    public float loadingProgress {get; private set;}
    public Scene loadedSceneBefore;

    bool gameplaySceneLoaded = false;
    public bool GameplaySceneLoaded {set {gameplaySceneLoaded = value;} get {return gameplaySceneLoaded;}}

    void Awake() 
    {
        if(inst == null)
            inst = this;
    }

    public void OnLoadSceneAsync(string sceneName, Action beforeSwitchScene = null, Action afterSwitchScene = null)
    {
        StartCoroutine(LoadSceneAsync(sceneName,beforeSwitchScene,afterSwitchScene));
    }

    IEnumerator LoadSceneAsync(string sceneName, Action beforeSwitchScene = null, Action afterSwitchScene = null)
    {
        beforeSwitchScene?.Invoke();

        yield return null;

        var asyncOparation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        asyncOparation.allowSceneActivation = false;

        while(!asyncOparation.isDone)
        {

            loadingProgress = Mathf.Clamp01(asyncOparation.progress / 0.9f);
            print("Scene progress : " + loadingProgress);

            if (loadingProgress >= 0.9f)
            {
                asyncOparation.allowSceneActivation = true;
            }

            yield return null;
        }

        yield return null;

        var loadedScene = SceneManager.GetSceneByName(sceneName);

        if(loadedScene.isLoaded)
        {
            SceneManager.SetActiveScene(loadedScene);
        }

        if(loadedSceneBefore.IsValid())
           SceneManager.UnloadSceneAsync(loadedSceneBefore);

        loadedSceneBefore = loadedScene;
        
        yield return null;

        afterSwitchScene?.Invoke();
    }
}
