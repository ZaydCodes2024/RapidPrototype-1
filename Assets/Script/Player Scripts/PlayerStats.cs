using UnityEngine;
using System;
public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance { get; private set; }
    public event EventHandler OnStatsChanged;
    [SerializeField] private float damage = 20f;
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float maxStamina = 100f;
    [SerializeField] private float moveSpeed = 5f;
    private void Awake()
    {
        Instance = this;
    }
    public float Damage => damage;
    public float MaxHealth => maxHealth;
    public float MoveSpeed => moveSpeed;
    public float MaxStamina => maxStamina;

    private float ApplyValue(float currentValue, StatModifier modifier)
    {
        if (modifier.isPercent)
        {
            return currentValue * (1 + modifier.amount / 100f);
        }

        return currentValue + modifier.amount;
    }
    public void ApplyModifier(StatModifier modifier)
    {
        switch (modifier.statType)
        {
            case StatType.Damage:
                damage = ApplyValue(damage, modifier);
                break;

            case StatType.MaxHealth:
                maxHealth = ApplyValue(maxHealth, modifier);
                break;

            case StatType.MaxStamina:
                maxStamina = ApplyValue(maxStamina, modifier);
                break;
        }

        OnStatsChanged?.Invoke(this, EventArgs.Empty);
    }
}
