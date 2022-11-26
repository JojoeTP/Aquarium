using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController Inst;
    public bool IsLoadingScene { get; private set; }
    private void Awake()
    {
        if (Inst == null)
            Inst = this;
    }

    void Start()
    {
        //load mainmenu
    }

    void AddListenerToButton()
    {

    }



}
