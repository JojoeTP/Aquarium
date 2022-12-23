using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;

    [Header("PlayerSpeed")]
    [SerializeField] float playerBaseSpeed;
    float playerSpeed;

    [Header("Sprint")]
    [SerializeField] bool IsSprint = false;
    [SerializeField] bool IsExhausted = false;
    [SerializeField] float sprintSpeed;
    [SerializeField] float basePlayerStamina;
    float playerStamina;
    [SerializeField] float staminaCost;
    [SerializeField] float staminaRegeneration;

    public float PlayerStamina {get {return playerStamina;}}
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
    }

    public void OnSprint(InputValue value)
    {
        if(value.isPressed)
        {
            if(IsExhausted)
                return;

            playerSpeed = sprintSpeed;
            IsSprint = true;
            PlayerManager.inst.playerAnimator.SetBool("Run", true);

        }
        else
        {
            playerSpeed = playerBaseSpeed;
            IsSprint = false;
            PlayerManager.inst.playerAnimator.SetBool("Run", false);
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
    
    private void FixedUpdate() 
    {
        Move();
        CheckStamina();
        StaminaRegeneration();
    }

    bool isMove = false;
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
        
        
        if(rb.velocity != Vector2.zero)
            PlayerManager.inst.playerAnimator.SetBool("Walk",true);
        else
            PlayerManager.inst.playerAnimator.SetBool("Walk",false);
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
}
