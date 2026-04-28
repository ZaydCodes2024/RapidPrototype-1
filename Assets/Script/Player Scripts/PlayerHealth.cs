using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth Instance {get; private set;}
    [SerializeField] private CameraShake cameraShake;
    private float health = 100f;
    public event EventHandler OnDamageTaken;
    private void Awake()
    {
        Instance = this;
    }
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
