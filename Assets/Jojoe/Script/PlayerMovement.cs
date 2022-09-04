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

    public bool hide = false;

    public Animator playerAnimation;


    // bool isHide = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerStamina = basePlayerStamina;
        // staminaSlider.value = 0.7f;
    }

    void Update()
    {
        ChangeStaminaBar();
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

    void ChangeStaminaBar()
    {
        float normalizedStaminaBar = Mathf.Clamp(playerStamina/basePlayerStamina, 0, 1);
        staminaSlider.value = normalizedStaminaBar;
    }

    void StaminaRegeneration()
    {
        if(basePlayerStamina > playerStamina && IsSprint == false){
            playerStamina += staminaRegeneration;
        }
        if(playerStamina > basePlayerStamina){
            playerStamina = basePlayerStamina;
        }
    }
    
    private void FixedUpdate() 
    {
        CheckStamina();
        Sprint();
        StaminaRegeneration();
        Move();
    }

    void Move()
    {
        if(hide)
        {
            rb.velocity = Vector2.zero;
            return;
        }
            
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        
        Vector2 direction = new Vector2(horizontal,vertical);

        rb.velocity = direction * playerSpeed;


        if (horizontal != 0) { playerAnimation.SetBool("Walk",true); }
        else { playerAnimation.SetBool("Walk", false); };


    }

    public void SetSortingOrder(int order)
    {
        GetComponent<SpriteRenderer>().sortingOrder = order;
    }
}
