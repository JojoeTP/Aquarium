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

    private void Awake() 
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        playerSpeed = playerBaseSpeed;
        playerStamina = basePlayerStamina;
    }

    void Update()
    {
        ChangeStaminaBar();
    }

    void CheckStamina()
    {
        if(playerStamina < 0)
        {
            playerSpeed = playerBaseSpeed;
            IsSprint = false;
        }
    }

    public void OnSprint()
    {
        if(!IsSprint)
        {
            if(playerStamina > 0)
            {
                playerSpeed = playerBaseSpeed + sprintSpeed;
                IsSprint = true;
            }
        }
    }

    public void CancelSprint()
    {
        if(IsSprint)
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
            playerStamina -= staminaCost;
            
        if(basePlayerStamina > playerStamina && IsSprint == false)
        {
            playerStamina += staminaRegeneration;
        }
        if(playerStamina > basePlayerStamina)
        {
            playerStamina = basePlayerStamina;
        }
    }
    
    private void FixedUpdate() 
    {
        CheckStamina();
        StaminaRegeneration();
    }

    public void Move(PlayerControls playerControl, bool isHide)
    {
        if(isHide)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        Vector2 direction = playerControl.Player.Move.ReadValue<Vector2>();
        rb.velocity = direction * playerSpeed;
    }
}
