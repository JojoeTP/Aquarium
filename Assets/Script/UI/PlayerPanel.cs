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
        
        currentState = PANELSTATE.NONE;
        if(PlayerManager.inst != null)
            PlayerManager.inst.playerState = PlayerManager.PLAYERSTATE.NONE;
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

    public void ToggleSetting()
    {
        if(currentState == PANELSTATE.NONE)
            OnChangePanel(PANELSTATE.SETTING);
        else
            OnChangePanel(PANELSTATE.NONE);
    }

    void ToggleMap()
    {
        if (currentState != PANELSTATE.MAP)
            OnChangePanel(PANELSTATE.MAP);
        else
            OnChangePanel(PANELSTATE.NONE);
    }

    public void OnClose()
    {
        OnChangePanel(PANELSTATE.NONE);
    }

    public void OnOpenInventory()
    {
        if(currentState != PANELSTATE.INVENTORY)
            OnChangePanel(PANELSTATE.INVENTORY);
    }

    public void OnOpenSetting()
    {
        if(currentState != PANELSTATE.SETTING)
        OnChangePanel(PANELSTATE.SETTING);
    }

    public void OnOpenMap()
    {
        if(currentState != PANELSTATE.MAP)
        OnChangePanel(PANELSTATE.MAP);
    }

    void OnChangePanel(PANELSTATE _panelState)
    {
        currentState = _panelState;

        switch(currentState)
        {
            case PANELSTATE.NONE :
                OnClosePanel();

                if(PlayerManager.inst != null)
                    PlayerManager.inst.playerState = PlayerManager.PLAYERSTATE.NONE;
                break;
            case PANELSTATE.INVENTORY :
                // OnOpenPanel();
                animator.SetTrigger("Bag");
                inventoryPanel.OnOpenInventory();

                if(PlayerManager.inst != null)
                    PlayerManager.inst.playerState = PlayerManager.PLAYERSTATE.OPENPANEL;
                break;
            case PANELSTATE.SETTING :
                // OnOpenPanel();
                animator.SetTrigger("Setting");

                if(PlayerManager.inst != null)
                    PlayerManager.inst.playerState = PlayerManager.PLAYERSTATE.OPENPANEL;
                break;
            case PANELSTATE.MAP:
                // OnOpenPanel();
                animator.SetTrigger("Map");

                if(PlayerManager.inst != null)
                    PlayerManager.inst.playerState = PlayerManager.PLAYERSTATE.OPENPANEL;
                break;
        }
    }

    void OnOpenPanel()
    {
        if(PlayerManager.inst != null)
        {
            if (PlayerManager.inst.playerState != PlayerManager.PLAYERSTATE.OPENPANEL)
            {
                animator.SetTrigger("Open");
            }
        }
    }

    void OnClosePanel()
    {
        if(PlayerManager.inst != null)
        {
            if (PlayerManager.inst.playerState != PlayerManager.PLAYERSTATE.NONE)
                animator.SetTrigger("Exit");
        }
    }
}
