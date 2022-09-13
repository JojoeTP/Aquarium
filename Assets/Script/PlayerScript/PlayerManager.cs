using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager inst;
    public GameObject playerSprite;

    [Header("InputSystem")]
    public PlayerControls playerControl;

    [Header("PlayerScript")]
    public PlayerMovement playerMovement;
    public PlayerInteract playerInteract;
    public PlayerLight playerLight;
    public PlayerInventory playerInventory;

    [Header("PlayerAction")]
    public bool isHide;


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
        playerControl.Player.Interact.performed += context => playerInteract.Interacting();
        playerControl.Player.Sprint.performed += context => playerMovement.OnSprint();
        playerControl.Player.Sprint.canceled += context => playerMovement.CancelSprint();
    }

    void Update()
    {
        
    }

    private void FixedUpdate() 
    {
        playerMovement.Move(isHide);
    }
}
