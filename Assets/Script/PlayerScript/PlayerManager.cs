using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerMovement))]
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
    PlayerCam playerCam;

    public PlayerMovement PlayerMovement {get {return playerMovement;}}
    public PlayerInteract PlayerInteract {get {return playerInteract;}}
    public PlayerLight PlayerLight {get {return playerLight;}}
    public PlayerInventory PlayerInventory {get {return playerInventory;}}
    public PlayerCam PlayerCam { get { return playerCam; } }

    [Header("PlayerAction")]
    public PLAYERSTATE playerState;
    
    private void Awake() 
    {
        inst = this;
    }
    void Start()
    {
        Init();
    }

    void GetPlayerComponent()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerInteract = GetComponent<PlayerInteract>();
        playerLight = GetComponent<PlayerLight>();
        playerInventory = GetComponent<PlayerInventory>();
        playerCam = GetComponent<PlayerCam>();
    }

    void Init()
    {
        GetPlayerComponent();

        playerState = PLAYERSTATE.NONE;

        LoadPlayerData();
        //Set position
        //Set Item in inventory
    }

    void LoadPlayerData()
    {
        if(!SaveGameSystemManager.inst.isLoad)
            return;
        
        this.transform.position = SaveGameSystemManager.inst.gameData.GetPlayerPosition();
        playerMovement.PlayerStamina = SaveGameSystemManager.inst.gameData.GetPlayerStamina();
        playerInventory.PlayerItemDictionary = SaveGameSystemManager.inst.gameData.GetPlayerItem();
        
    }

    void SetUpPlayer()
    {
        // InputSystemManager.Inst.SetUIControl(true);
        // InputSystemManager.Inst.SetPlayerControl(true);
    }
}
