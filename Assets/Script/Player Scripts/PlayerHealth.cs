using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth Instance {get; private set;}
    public event EventHandler OnDamageTaken;
    [SerializeField] private CameraShake cameraShake;
    [SerializeField] private DamageIndicatorUI damageIndicator;
    private float health = 100f;
    private void Awake() => Instance = this;
    public void TakeDamage(float damage, Vector3 damagePosition)
    {
        health -= damage;
        
        GameObject indicatorObject = Instantiate(damageIndicator.gameObject, damageIndicator.transform.position, damageIndicator.transform.rotation, damageIndicator.transform.parent);

        DamageIndicatorUI indicator = indicatorObject.GetComponent<DamageIndicatorUI>();

        indicator.SetDamageLocation(damagePosition);

        indicatorObject.SetActive(true);

        OnDamageTaken?.Invoke(this, EventArgs.Empty);

        SoundManager.Instance.PlayPlayerHurtSound(Player.Instance.GetCameraTransform().position, 20f);

        cameraShake.ShakeCamera();
    }

    public float GetPlayerHealth() => health;
}
