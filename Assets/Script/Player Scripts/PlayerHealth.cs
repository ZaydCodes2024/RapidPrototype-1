using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth Instance {get; private set;}
    public event EventHandler OnDamageTaken;
    [SerializeField] private CameraShake cameraShake;
    [SerializeField] private DamageIndicatorUI damageIndicator;
    private float currentHealth;
    private float maxHealth;
    private void Awake() => Instance = this;
    private void Start()
    {
        PlayerStats.Instance.OnStatsChanged += PlayerStats_OnStatsChanged;
        maxHealth = PlayerStats.Instance.MaxHealth;
        currentHealth = maxHealth;
    }

    private void PlayerStats_OnStatsChanged(object sender, EventArgs e)
    {
        float newMaxHealth = PlayerStats.Instance.MaxHealth;
        float healthPercent = Mathf.CeilToInt(currentHealth / maxHealth);

        maxHealth = newMaxHealth;
        currentHealth = maxHealth * healthPercent;
    }

    public void TakeDamage(float damage, Vector3 damagePosition)
    {
        currentHealth -= damage;
        
        GameObject indicatorObject = Instantiate(damageIndicator.gameObject, damageIndicator.transform.position, damageIndicator.transform.rotation, damageIndicator.transform.parent);

        DamageIndicatorUI indicator = indicatorObject.GetComponent<DamageIndicatorUI>();

        indicator.SetDamageLocation(damagePosition);

        indicatorObject.SetActive(true);

        OnDamageTaken?.Invoke(this, EventArgs.Empty);

        SoundManager.Instance.PlayPlayerHurtSound(Player.Instance.GetCameraTransform().position, 20f);

        cameraShake.ShakeCamera();
    }
    public float GetPlayerHealth() => currentHealth;
}
