using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class LoadSceneController : MonoBehaviour
{
    public string SCENE_MAINMENU { get {return "Scene_MainMenu";} }
    public string SCENE_MAINMENU_2 { get {return "Scene_MainMenu2";} }
    public string SCENE_CORE { get {return "Scene_Core";} }
    public string Scene_INITIALIZE { get {return "Scene_Initialize";} }

#if !UNITY_EDITOR
    void Awake()
    {
        LoadCoreScene();
    }
#endif

    void LoadCoreScene()
    {
        SceneManager.LoadScene(SCENE_CORE,LoadSceneMode.Additive);
        StartCoroutine(GoToSceneMainMenu());
    }

    IEnumerator GoToSceneMainMenu()
    {
        yield return new WaitUntil(() => SceneController.inst != null);

        if(PlayerPrefs.GetInt("DarkMainMenu",0) == 0)
            SceneController.inst.OnLoadSceneAsync(SCENE_MAINMENU,null,UnloadLoadScene);
        else
            SceneController.inst.OnLoadSceneAsync(SCENE_MAINMENU_2,null,UnloadLoadScene);
    }

    void UnloadLoadScene()
    {
        SceneManager.UnloadSceneAsync(Scene_INITIALIZE);
    }
}
