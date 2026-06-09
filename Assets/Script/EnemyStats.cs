using UnityEngine;
using System;
public class EnemyStats : MonoBehaviour
{
    public static EnemyStats Instance { get; private set; }
    [SerializeField] private float damage = 10f;
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float moveSpeed = 5f;
    private void Awake()
    {
        Instance = this;
    }
    public float Damage => damage;
    public float MaxHealth => maxHealth;
    public float MoveSpeed => moveSpeed;

    private float ApplyValue(float currentValue, UpgradeEffect upgradeEffect)
    {
        if (upgradeEffect.isPercent)
        {
            return currentValue * (1 + upgradeEffect.amount / 100f);
        }

        return currentValue + upgradeEffect.amount;
    }
    public void ApplyModifier(UpgradeEffect upgradeEffect)
    {
        switch (upgradeEffect.statType)
        {
            case StatType.Damage:
                damage = ApplyValue(damage, upgradeEffect);
                break;

            case StatType.MaxHealth:
                maxHealth = ApplyValue(maxHealth, upgradeEffect);
                break;

            case StatType.MaxStamina:
                moveSpeed = ApplyValue(moveSpeed, upgradeEffect);
                break;
        }
    }
}
