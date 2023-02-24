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
    public string SCENE_LOADING { get {return "Scene_Loading";} }

    Scene loadedSceneBefore;

    void Awake() 
    {
        if(inst == null)
            inst = this;
    }

    void Start()
    {

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

        while(!asyncOparation.isDone)
        {
            // print("Scene progress : " + asyncOparation.progress);
            yield return null;
        }
        
        asyncOparation.allowSceneActivation = true;
        var loadedScene = SceneManager.GetSceneByName(sceneName);
        
        if(loadedScene.isLoaded)
        {
            SceneManager.SetActiveScene(loadedScene);
        }

        afterSwitchScene?.Invoke();

        if(loadedSceneBefore.IsValid())
            SceneManager.UnloadSceneAsync(loadedSceneBefore);

        loadedSceneBefore = loadedScene;
    }
}
