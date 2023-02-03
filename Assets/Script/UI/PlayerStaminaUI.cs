using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStaminaUI : MonoBehaviour
{
    [SerializeField] Slider staminaSlider;
    PlayerMovement playerMovement;

    void Start()
    {
        playerMovement = PlayerManager.inst.PlayerMovement;        
    }

    void FixedUpdate()
    {
        UpdateStaminaBar();
    }

    void UpdateStaminaBar()
    {
        float normalizedStaminaBar = Mathf.Clamp(playerMovement.PlayerStamina/playerMovement.BasePlayerStamina, 0, 1);
        staminaSlider.value = normalizedStaminaBar;
    }
}
