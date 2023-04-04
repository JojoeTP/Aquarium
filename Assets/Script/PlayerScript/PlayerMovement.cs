using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using FMOD.Studio;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;

    [Header("Player")]
    [SerializeField] float playerBaseSpeed;
    [SerializeField] float sprintSpeed;
    [SerializeField] float basePlayerStamina;
    [SerializeField] float staminaCost;
    [SerializeField] float staminaRegeneration;

    float playerSpeed;
    float playerStamina;


    [Header("Walk")]
    bool isMove = false;
    bool isRunIntoWall = false;

    [Header("Sprint")]
    bool IsSprint = false;
    bool IsExhausted = false;

#region  Audio
    EventInstance playerMovement;
#endregion

    public float PlayerStamina {get {return playerStamina;} set {playerStamina = value;}}
    public float BasePlayerStamina {get {return basePlayerStamina;}}

    Vector2 direction;
    Vector3 scale;

    private void Awake() 
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        InputSystemManager.Inst.onMove += OnMove;
        InputSystemManager.Inst.onPressMove += OnPressMove;
        InputSystemManager.Inst.onSprint += OnSprint;

        scale = transform.localScale;

        playerSpeed = playerBaseSpeed;
        playerStamina = basePlayerStamina;

        StartCoroutine(Blink());

        CreateAudioInstance();
    }

    void Update()
    {
        UpdateSound();
    }

    void FixedUpdate() 
    {
        Move();
        CheckStamina();
        StaminaRegeneration();
        PlayAnimation();
    }

    public void OnSprint(InputValue value)
    {
        if(value.isPressed)
        {
            if(IsExhausted)
                return;

            playerSpeed = sprintSpeed;
            IsSprint = true;
            playerMovement.setParameterByName("PlayerMovement",1);
        }
        else
        {
            playerSpeed = playerBaseSpeed;
            IsSprint = false;
            playerMovement.setParameterByName("PlayerMovement",0);
        }
    }

    void CheckStamina()
    {
        if(playerStamina < 0)
        {
            playerSpeed = playerBaseSpeed;
            IsSprint = false;
            IsExhausted = true;
        }
    }

    void StaminaRegeneration()
    {
        if(IsSprint)
        {
            playerStamina -= staminaCost;
            return;
        }
            
        if(basePlayerStamina > playerStamina)
        {
            playerStamina += staminaRegeneration;
        }
        else
        {
            IsExhausted = false;
            playerStamina = basePlayerStamina;
        }
    }

    public void Move()
    {
        if(!isMove)
            direction = Vector2.zero;

        if(PlayerManager.inst.playerState != PlayerManager.PLAYERSTATE.NONE)
        {
            direction = Vector2.zero;
        }

        rb.velocity = direction * playerSpeed;

        if(direction.x == 1)
            transform.localScale = new Vector3(-scale.x,transform.localScale.y,transform.localScale.z);
        else if(direction.x == -1)
            transform.localScale = new Vector3(scale.x,transform.localScale.y,transform.localScale.z);
    }

    public void OnMove(Vector2 value)
    {
        direction = value.normalized;
    }

    public void OnPressMove(bool value)
    {
        isMove = value;
    }

    IEnumerator Blink()
    {
        PlayerManager.inst.playerAnimator.SetTrigger("Blink");
        float blinkTime = Random.Range(10f,30f);
        yield return new WaitForSeconds(blinkTime);
        StartCoroutine(Blink());
    }

    void PlayAnimation()
    {
        PlayWalkAnimation();
        PlaySprintAnimation();
    }

    void PlayWalkAnimation()
    {
        if(rb.velocity != Vector2.zero && !isRunIntoWall)
            PlayerManager.inst.playerAnimator.SetBool("Walk",true);
        else
            PlayerManager.inst.playerAnimator.SetBool("Walk",false);

    }

    void PlaySprintAnimation()
    {
        if(IsSprint)
            PlayerManager.inst.playerAnimator.SetBool("Run", true);
        else
            PlayerManager.inst.playerAnimator.SetBool("Run", false);
    }

    void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.CompareTag("Wall"))
        {
            isRunIntoWall = true;
        }
    }

    void OnCollisionExit2D(Collision2D other) 
    {
        if(other.gameObject.CompareTag("Wall"))
        {
            isRunIntoWall = false;
        }
    }

    void CreateAudioInstance()
    {
        playerMovement = SoundManager.Inst.CreateInstance(FMODEvent.inst.playerMovementSFX.sound);
    }

    void UpdateSound()
    {
        UpdateMoveSound();
    }
    void UpdateMoveSound()
    {
        if(direction != Vector2.zero)
        {
            PLAYBACK_STATE playBackState;
            playerMovement.getPlaybackState(out playBackState);
            if(playBackState.Equals(PLAYBACK_STATE.STOPPED))
                playerMovement.start();
        }
        else
            playerMovement.stop(STOP_MODE.ALLOWFADEOUT);
    }

    public bool IsMove()
    {
        return isMove;
    }

    void OnDestroy() {
        playerMovement.stop(STOP_MODE.ALLOWFADEOUT);

        InputSystemManager.Inst.onMove -= OnMove;
        InputSystemManager.Inst.onPressMove -= OnPressMove;
        InputSystemManager.Inst.onSprint -= OnSprint;
    }
}
