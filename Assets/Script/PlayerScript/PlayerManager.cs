using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    PlayerManager inst;

    [Header("InputSystem")]
    PlayerControls playerControl;

    [Header("PlayerScript")]
    public PlayerMovement playerMovement;
    public PlayerHide playerHide;
    public PlayerLight playerLight;
    public PickUpItem pickUpItem;


    private void Awake() 
    {
        inst = this;
        playerControl = new PlayerControls();
    }

    private void OnEnable() 
    {
        playerControl.Enable();
    }

    private void OnDisable() 
    {
        playerControl.Disable();
    }

    void Start()
    {
        playerControl.Player.Interact.performed += context => playerHide.ToggleHiding();
        playerControl.Player.Sprint.performed += context => playerMovement.OnSprint();
        playerControl.Player.Sprint.canceled += context => playerMovement.CancelSprint();
    }

    void Update()
    {
        
    }

    private void FixedUpdate() 
    {
        playerMovement.Move(playerControl,playerHide.isHide);
    }
}
