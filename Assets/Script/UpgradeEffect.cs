using System;

[Serializable]
public struct UpgradeEffect
{
    public StatType statType;
    public float amount;
    public bool isPercent;
    public UpgradeTarget target;
}