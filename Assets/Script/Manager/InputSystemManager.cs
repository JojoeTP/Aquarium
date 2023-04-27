using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class InputSystemManager : MonoBehaviour
{
    private const string PLAYER_ACTIONMAP = "PlayerControl";
    private const string MENU_NAVIGATION_ACTIONMAP = "UI";

    public static InputSystemManager Inst;
    public InputActionAsset playerInputAction;

    public InputSystemUIInputModule inputSystemUIInputModule;

    //UnityAction
    public UnityAction<Vector2> onMove;
    public UnityAction<bool> onPressMove;
    public UnityAction<InputValue> onInteract;
    public UnityAction<InputValue> onSprint;
    public UnityAction onToggleInventory;
    public UnityAction onToggleSetting;
    public UnityAction onToggleMap;
    public UnityAction<InputValue> onNavigate;
    public UnityAction onClose;
    public UnityAction onLight;
    public UnityAction onCheatItem;
    public UnityAction onPlayerStateNone;
    

    //InputActionMap
    InputActionMap playerControlMap;
    InputActionMap uiControlMap;

    //input state verification
    bool globleInputEnable = false;
    bool playerControlEnable = true;
    bool uiControlEnable = true;

    void Awake() 
    {
        Inst = this;    
    }

    void Start()
    {
        playerControlMap = playerInputAction.FindActionMap(PLAYER_ACTIONMAP);
        uiControlMap = playerInputAction.FindActionMap(MENU_NAVIGATION_ACTIONMAP);
        EnableGlobalInput();

    }

    void UpdateInputState()
    {
        if(globleInputEnable && playerControlEnable) playerControlMap.Enable();
        else playerControlMap.Disable();

        if(globleInputEnable && uiControlEnable) uiControlMap.Enable();
        else uiControlMap.Disable();
    }

    public void EnableGlobalInput()
    {
        globleInputEnable = true;
        UpdateInputState();
    }

    public void DisableGlobalInput()
    {
        globleInputEnable = false;
        UpdateInputState();
    }

    public void SetPlayerControl(bool enable)
    {
        playerControlEnable = enable;
        UpdateInputState();
    }

    public void SetUIControl(bool enable)
    {
        uiControlEnable = enable;
        UpdateInputState();
    }

    #region ControlFunction
    //UI
    void OnClose(InputValue value)
    {
        if(value.isPressed)
        {
            onClose?.Invoke();
        }
    }


    //Player
    void OnMove(InputValue value)
    {
        if(value.Get<Vector2>() != Vector2.zero)
        {
            onMove?.Invoke(value.Get<Vector2>());
        }
    }
    
    void OnPressMove(InputValue value)
    {
        if(value.isPressed)
            onPressMove?.Invoke(true);
        else
            onPressMove?.Invoke(false);
    }   

    void OnInteract(InputValue value)
    {
        if(value.isPressed)
        {
            onInteract?.Invoke(value);
        }
    }

    void OnSprint(InputValue value)
    {
        onSprint?.Invoke(value);
    }

    void OnToggleInventory()
    {
        onToggleInventory?.Invoke();
    }

    void OnToggleSetting()
    {
        onToggleSetting?.Invoke();
    }

    void OnToggleMap()
    {
        onToggleMap?.Invoke();
    }

    void OnLight()
    {
        onLight?.Invoke();
    }

    void OnCheatItem()
    {
        onCheatItem?.Invoke();
    }
    void OnPlayerStateNone()
    {
        onPlayerStateNone?.Invoke();
    }


    #endregion
}
