using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private CameraShake cameraShake;
    private float health = 100f;
    public void TakeDamage(float damage)
    {
        health -= damage;
        SoundManager.Instance.PlayPlayerHurtSound(transform.position, 10f);
        cameraShake.ShakeCamera();
    }

    public float GetPlayerHealth()
    {
        return health;
    }
}
