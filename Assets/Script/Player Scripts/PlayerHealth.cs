using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private CameraShake cameraShake;
    private float health = 100f;
    public event EventHandler OnDamageTaken;
    public void TakeDamage(float damage)
    {
        health -= damage;
        OnDamageTaken?.Invoke(this, EventArgs.Empty);
        SoundManager.Instance.PlayPlayerHurtSound(Player.Instance.GetCameraTransform().position, 20f);
        cameraShake.ShakeCamera();
    }

    public float GetPlayerHealth()
    {
        return health;
    }
}
