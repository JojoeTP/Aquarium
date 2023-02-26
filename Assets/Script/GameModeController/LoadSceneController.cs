using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class LoadSceneController : MonoBehaviour
{
    public string SCENE_MAINMENU { get {return "Scene_MainMenu";} }
    public string SCENE_CORE { get {return "Scene_Core";} }
    public string Scene_INITIALIZE { get {return "Scene_Initialize";} }

#if !UNITY_EDITOR
    void Awake()
    {
        LoadCoreScene();
    }
#endif

    void Start()
    {
        
    }

    void LoadCoreScene()
    {
        SceneManager.LoadScene(SCENE_CORE,LoadSceneMode.Additive);
        StartCoroutine(GoToSceneMainMenu());
    }

    IEnumerator GoToSceneMainMenu()
    {
        yield return new WaitUntil(() => SceneController.inst != null);
        SceneController.inst.OnLoadSceneAsync(SCENE_MAINMENU,null,UnloadLoadScene);
    }

    void UnloadLoadScene()
    {
        SceneManager.UnloadSceneAsync(Scene_INITIALIZE);
    }
}
