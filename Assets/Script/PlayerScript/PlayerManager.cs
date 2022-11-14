using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    public enum PLAYERSTATE
    {
        NONE,
        CONVERSATION,
        HIDING,
    }

    public static PlayerManager inst;
    public GameObject playerSprite;
    public Animator playerAnimator;

    [Header("PlayerScript")]
    public PlayerMovement playerMovement;
    public PlayerInteract playerInteract;
    public PlayerLight playerLight;
    public PlayerInventory playerInventory;

    [Header("PlayerAction")]
    public bool isHide;

    [Header("PlayerAction")]
    public PLAYERSTATE playerState;
    
    private void Awake() 
    {
        inst = this;
    }

    void Start()
    {
        playerState = PLAYERSTATE.NONE;
        
        InputSystemManager.Inst.onMove += playerMovement.OnMove;
        InputSystemManager.Inst.onPressMove += playerMovement.OnPressMove;
        InputSystemManager.Inst.onSprint += playerMovement.OnSprint;
        InputSystemManager.Inst.onInteract += playerInteract.OnInteract;
        InputSystemManager.Inst.onOpenInventory += playerInventory.OnOpenInventory;
        InputSystemManager.Inst.onLight += playerLight.OnLight;
    }

    void Update()
    {
        
    }

    private void FixedUpdate() 
    {
        playerMovement.Move();
    }

    void SetUpPlayer()
    {
        // InputSystemManager.Inst.SetUIControl(true);
        // InputSystemManager.Inst.SetPlayerControl(true);
    }
}
