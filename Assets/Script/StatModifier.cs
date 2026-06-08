using System;

[Serializable]
public struct StatModifier
{
    public StatType statType;
    public float amount;
    public bool isPercent;
}