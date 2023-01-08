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
    }

    [SerializeField] InventoryPanel inventoryPanel;
    [SerializeField] SettingPanel settingPanel;

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
    }

    void OnDestroy() 
    {
        InputSystemManager.Inst.onToggleInventory -= ToggleInventory;
        InputSystemManager.Inst.onToggleSetting -= ToggleSetting;
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

    void OnChangePanel(PANELSTATE _panelState)
    {
        currentState = _panelState;

        switch(currentState)
        {
            case PANELSTATE.NONE :
                canvas.enabled = false;
                inventoryPanel.Canvas.enabled = false;
                settingPanel.Canvas.enabled = false;
                break;
            case PANELSTATE.INVENTORY :
                canvas.enabled = true;
                inventoryPanel.Canvas.enabled = true;
                settingPanel.Canvas.enabled = false;
                inventoryPanel.OnOpenInventory();
                break;
            case PANELSTATE.SETTING :
                canvas.enabled = true;
                inventoryPanel.Canvas.enabled = false;
                settingPanel.Canvas.enabled = true;
                break;
        }
    }
}
