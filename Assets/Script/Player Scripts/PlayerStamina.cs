using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStamina : MonoBehaviour
{
    public static PlayerStamina Instance {get; private set;}
    private Image staminaBar;
    private float stamina;
    private float staminaDecay = 15f;
    private float staminaRegen = 15f;
    private float staminaSpeed= 4f;
    private float staminaMax = 100f;
    private float regenTimer;
    private float regenDelay = 2f;
    private bool canSprint;
    private bool sprintLocked = false;

    private void Awake()
    {
        Instance = this;
        stamina = staminaMax;
        staminaBar = GetComponent<Image>();
    }

    private void Update()
    {
        stamina = Mathf.Clamp(stamina, 0, staminaMax);

        UpdateStamina();

        UpdateStaminaBar();
    }

    private void UpdateStaminaBar()
    {
        float sFraction = stamina/staminaMax;

        staminaBar.fillAmount = Mathf.Lerp(staminaBar.fillAmount, sFraction, staminaSpeed * Time.deltaTime);
    }

    private void UpdateStamina()
    {
        canSprint = GameInput.Instance.IsRunning();

        if (canSprint && !sprintLocked)
        {
            stamina -= staminaDecay * Time.deltaTime;

            if (stamina <= 0)
            {
                stamina = 0f;
                sprintLocked = true;
            }
        }
        else
        {
            if (stamina < staminaMax)
            {
                regenTimer += Time.deltaTime;
                if (regenTimer >= regenDelay)
                    RegenerateStamina();
            }
        }
    }

    private void RegenerateStamina()
    {
        stamina += staminaRegen * Time.deltaTime;

        if (stamina >= staminaMax)
        {
            stamina = staminaMax;
            sprintLocked = false;
            regenTimer = 0f;
        }

        if (stamina >= staminaMax * 0.3f) // 30% threshold
        {
            sprintLocked = false;
        }
    }
    public float GetStaminaValue()
    {
        return stamina;
    }

    public bool IsSprintLocked()
    {
        return sprintLocked;
    }
}
