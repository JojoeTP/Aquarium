using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;

    [Header("PlayerSpeed")]
    public float playerBaseSpeed;
    public float playerSpeed;

    [Header("Sprint")]
    public bool IsSprint = false;
    public float sprintSpeed;
    public float basePlayerStamina;
    public float playerStamina;
    public float staminaCost;
    public float staminaRegeneration;
    public Slider staminaSlider;

    Vector2 direction;

    Vector3 scale;

    private void Awake() 
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        scale = transform.localScale;

        playerSpeed = playerBaseSpeed;
        playerStamina = basePlayerStamina;

        StartCoroutine(Blink());
    }

    public void OnSprint(InputValue value)
    {
        if(value.isPressed)
        {
            playerSpeed = playerBaseSpeed + sprintSpeed;
            IsSprint = true;
        }
        else
        {
            playerSpeed = playerBaseSpeed;
            IsSprint = false;
        }

    }

    void CheckStamina()
    {
        if(playerStamina < 0)
        {
            playerSpeed = playerBaseSpeed;
            IsSprint = false;
        }
    }

    void ChangeStaminaBar()
    {
        float normalizedStaminaBar = Mathf.Clamp(playerStamina/basePlayerStamina, 0, 1);
        staminaSlider.value = normalizedStaminaBar;
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
            playerStamina = basePlayerStamina;
        }
    }
    
    private void FixedUpdate() 
    {
        CheckStamina();
        ChangeStaminaBar();
        StaminaRegeneration();
    }

    bool isMove = false;
    public void Move()
    {
        if(!isMove)
            direction = Vector2.zero;

        
        if(PlayerManager.inst.isHide)
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
