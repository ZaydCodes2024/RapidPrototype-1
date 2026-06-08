using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStamina : MonoBehaviour
{
    public static PlayerStamina Instance {get; private set;}
    private Image staminaBar;
    private float currentStamina;
    private float staminaDecay = 15f;
    private float staminaRegen = 15f;
    private float staminaSpeed= 4f;
    private float staminaMax;
    private float regenTimer;
    private float regenDelay = 2f;
    private bool canSprint;
    private bool sprintLocked = false;

    private void Awake()
    {
        Instance = this;

        PlayerStats.Instance.OnStatsChanged += PlayerStats_OnStatsChanged;
        staminaMax = PlayerStats.Instance.MaxStamina;
        currentStamina = staminaMax;
        staminaBar = GetComponent<Image>();
    }

    private void PlayerStats_OnStatsChanged(StatType type)
    {
        if (type != StatType.MaxStamina)    return;
        
        staminaMax = PlayerStats.Instance.MaxStamina;
        currentStamina = Mathf.Min(currentStamina, staminaMax);
        Debug.Log($"Stamina upgraded: Max = {staminaMax}, Current = {currentStamina}");
    }

    private void Update()
    {
        if (GameManager.Instance.IsPlayerUpgrading())   return;
        
        currentStamina = Mathf.Clamp(currentStamina, 0, staminaMax);

        UpdateStamina();

        UpdateStaminaBar();
    }

    private void UpdateStaminaBar()
    {
        float sFraction = currentStamina/staminaMax;

        staminaBar.fillAmount = Mathf.Lerp(staminaBar.fillAmount, sFraction, staminaSpeed * Time.deltaTime);
    }

    private void UpdateStamina()
    {
        canSprint = GameInput.Instance.IsRunning();

        if (canSprint && !sprintLocked)
        {
            currentStamina -= staminaDecay * Time.deltaTime;

            if (currentStamina <= 0)
            {
                currentStamina = 0f;
                sprintLocked = true;
            }
        }
        else
        {
            if (currentStamina < staminaMax)
            {
                regenTimer += Time.deltaTime;
                if (regenTimer >= regenDelay)
                    RegenerateStamina();
            }
        }
    }

    private void RegenerateStamina()
    {
        currentStamina += staminaRegen * Time.deltaTime;

        if (currentStamina >= staminaMax)
        {
            currentStamina = staminaMax;
            sprintLocked = false;
            regenTimer = 0f;
        }

        if (currentStamina >= staminaMax * 0.3f) // 30% threshold
        {
            sprintLocked = false;
        }
    }
    public float GetStaminaValue() => currentStamina;

    public bool IsSprintLocked() => sprintLocked;
}
