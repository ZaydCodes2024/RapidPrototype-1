using UnityEngine;

[CreateAssetMenu()]
public class UpgradeSkillSO : ScriptableObject
{
    public enum UpgradeRarity
    {
        Common, Rare, Epic, Legendary
    }

    [Header("Upgrade Rarity")]
    public UpgradeRarity upgradeRarity;

    [Header("UI")]
    public Sprite icon;
    public string title;
    public string description;
}
