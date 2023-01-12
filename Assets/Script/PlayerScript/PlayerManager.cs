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
        ENTERDOOR,
        OPENPANEL,
        GETTINGITEM,
    }

    public static PlayerManager inst;
    public GameObject playerSprite;
    public Animator playerAnimator;

    [Header("PlayerScript")]
    PlayerMovement playerMovement;
    PlayerInteract playerInteract;
    PlayerLight playerLight;
    PlayerInventory playerInventory;

    public PlayerMovement PlayerMovement {get {return playerMovement;}}
    public PlayerInteract PlayerInteract {get {return playerInteract;}}
    public PlayerLight PlayerLight {get {return playerLight;}}
    public PlayerInventory PlayerInventory {get {return playerInventory;}}

    [Header("PlayerAction")]
    public PLAYERSTATE playerState;
    
    private void Awake() 
    {
        inst = this;

        TryGetComponent<PlayerMovement>(out playerMovement);
        TryGetComponent<PlayerInteract>(out playerInteract);
        TryGetComponent<PlayerLight>(out playerLight);
        TryGetComponent<PlayerInventory>(out playerInventory);
    }

    void Start()
    {
        playerState = PLAYERSTATE.NONE;
    }

    void Update()
    {
        
    }

    private void FixedUpdate() 
    {

    }

    void SetUpPlayer()
    {
        // InputSystemManager.Inst.SetUIControl(true);
        // InputSystemManager.Inst.SetPlayerControl(true);
    }
}
