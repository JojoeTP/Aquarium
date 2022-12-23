using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPanel : MonoBehaviour
{
    public static InventoryPanel Inst;

    void Awake() 
    {
        Inst = this;    
    }

    void Start()
    {
        InputSystemManager.Inst.onOpenInventory += InventoryPanel.Inst.OnOpenInventory;
        
        gameObject.SetActive(false);
    }

    void Update()
    {
        
    }

    void ToggleInventoryPanel()
    {
        if(PlayerManager.inst.playerState == PlayerManager.PLAYERSTATE.NONE)
            gameObject.SetActive(true);
        else if(PlayerManager.inst.playerState == PlayerManager.PLAYERSTATE.OPENPANEL)
            gameObject.SetActive(false);
    }

    public void OnOpenInventory()
    {
        ToggleInventoryPanel();
    }
}
