using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPanel : MonoBehaviour
{
    enum PANELSTATE
    {
        NONE,
        INVENTORY,
        SETTING,
        MAP,
    }
    [SerializeField] Animator animator;
    [SerializeField] InventoryPanel inventoryPanel;
    [SerializeField] SettingPanel settingPanel;
    [SerializeField] MapPanel mapPanel;

    PANELSTATE currentState;
    Canvas canvas;

    void Awake()
    {
        canvas = GetComponent<Canvas>();        
    }

    void Start()
    {
        Initialize();
    }

    void Initialize()
    {
        SetupInputAction();
        
        OnChangePanel(PANELSTATE.NONE);
    }

    void SetupInputAction()
    {
        InputSystemManager.Inst.onToggleInventory += ToggleInventory;
        InputSystemManager.Inst.onToggleSetting += ToggleSetting;
        InputSystemManager.Inst.onToggleMap += ToggleMap;
    }

    void OnDestroy() 
    {
        InputSystemManager.Inst.onToggleInventory -= ToggleInventory;
        InputSystemManager.Inst.onToggleSetting -= ToggleSetting;
        InputSystemManager.Inst.onToggleMap -= ToggleMap;
    }

    void ToggleInventory()
    {
        if(currentState != PANELSTATE.INVENTORY)
            OnChangePanel(PANELSTATE.INVENTORY);
        else
            OnChangePanel(PANELSTATE.NONE);
    }

    void ToggleSetting()
    {
        if(currentState != PANELSTATE.NONE)
            OnChangePanel(PANELSTATE.NONE);
        else if(currentState != PANELSTATE.SETTING)
            OnChangePanel(PANELSTATE.SETTING);
    }

    void ToggleMap()
    {
        if (currentState != PANELSTATE.NONE)
            OnChangePanel(PANELSTATE.NONE);
        else if (currentState != PANELSTATE.SETTING)
            OnChangePanel(PANELSTATE.SETTING);
    }

    public void OnClose()
    {
        OnChangePanel(PANELSTATE.NONE);
    }

    public void OnOpenInventory()
    {
        OnChangePanel(PANELSTATE.INVENTORY);
    }

    public void OnOpenSetting()
    {
        OnChangePanel(PANELSTATE.SETTING);
    }

    public void OnOpenMap()
    {
        OnChangePanel(PANELSTATE.MAP);
    }

    void OnChangePanel(PANELSTATE _panelState)
    {
        currentState = _panelState;

        switch(currentState)
        {
            case PANELSTATE.NONE :
                if (PlayerManager.inst.playerState != PlayerManager.PLAYERSTATE.NONE)
                    animator.SetTrigger("Exit");

                PlayerManager.inst.playerState = PlayerManager.PLAYERSTATE.NONE;
                break;
            case PANELSTATE.INVENTORY :
                if (PlayerManager.inst.playerState != PlayerManager.PLAYERSTATE.OPENPANEL)
                    animator.SetTrigger("Open");

                animator.SetTrigger("Bag");
                inventoryPanel.OnOpenInventory();
                PlayerManager.inst.playerState = PlayerManager.PLAYERSTATE.OPENPANEL;

                break;
            case PANELSTATE.SETTING :
                if (PlayerManager.inst.playerState != PlayerManager.PLAYERSTATE.OPENPANEL)
                    animator.SetTrigger("Open");

                animator.SetTrigger("Setting");
                PlayerManager.inst.playerState = PlayerManager.PLAYERSTATE.OPENPANEL;
                break;
            case PANELSTATE.MAP:
                if (PlayerManager.inst.playerState != PlayerManager.PLAYERSTATE.OPENPANEL)
                    animator.SetTrigger("Open");

                animator.SetTrigger("Map");
                PlayerManager.inst.playerState = PlayerManager.PLAYERSTATE.OPENPANEL;
                break;
        }
    }
}
