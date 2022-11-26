using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] Button newGameButton;
    [SerializeField] Button continueButton;
    [SerializeField] Button settingButton;
    [SerializeField] Button exitButton;

    void Start()
    {
        
    }

    public void OnClickContinue()
    {

    }

    public void OnClickNewGame()
    {

    }

    public void OnClickSetting()
    {

    }

    public void OnClickExit()
    {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
