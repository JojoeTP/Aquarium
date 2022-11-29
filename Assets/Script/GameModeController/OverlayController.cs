using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlayController : AbstractGameModeController
{
    public bool IsInitialized {get {return isInitialized;} set {isInitialized = value;}}
    private bool isInitialized;

    public override string ControllerName
    {
        get
        {
            return "Scene_PlayerUI_Panel";
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        isInitialized = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
