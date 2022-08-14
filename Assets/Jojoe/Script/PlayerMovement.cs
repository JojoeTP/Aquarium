using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{

    Rigidbody2D rb;
    public float playerBaseSpeed;
    float playerSpeed;

    public bool IsSprint = false;
    public float sprintSpeed;
    public float basePlayerStamina;
    public float playerStamina;
    public float staminaCost;
    public float staminaRegeneration;

    public Slider staminaSlider;

    // bool isHide = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerStamina = basePlayerStamina;
        // staminaSlider.value = 0.7f;
    }

    void Update()
    {
        // Move();
        CheckStamina();
        Sprint();
        ChangeStaminaBar();
        StaminaRegeneration();
    }

    void CheckStamina(){
        if(playerStamina < 0){
            playerSpeed = playerBaseSpeed;
            IsSprint = false;
        }
    }

    void Sprint(){
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if(playerStamina > 0){
                playerSpeed = playerBaseSpeed + sprintSpeed;
                playerStamina -= staminaCost;
                IsSprint = true;
            }
        }
        else
        {
            playerSpeed = playerBaseSpeed;
            IsSprint = false;
        }
    }

    void ChangeStaminaBar(){
        float normalizedStaminaBar = Mathf.Clamp(playerStamina/basePlayerStamina, 0, 1);
        staminaSlider.value = normalizedStaminaBar;
    }

    void StaminaRegeneration(){
        if(basePlayerStamina > playerStamina && IsSprint == false){
            playerStamina += staminaRegeneration;
        }
        if(playerStamina > basePlayerStamina){
            playerStamina = basePlayerStamina;
        }
    }
    
    private void FixedUpdate() {
        Move();
    }

    void Move(){
        if(GetComponent<PlayerHide>().hide == true){
            return;
        }
            
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        rb.velocity = new Vector2(horizontal * playerSpeed , vertical * playerSpeed );
        // rb.velocity = new Vector2(horizontal * playerSpeed * Time.deltaTime, vertical * playerSpeed * Time.deltaTime);

    }
}
