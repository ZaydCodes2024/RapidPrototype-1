using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private float health = 100f;
    public void TakeDamage(float damage)
    {
        health -= damage;
        SoundManager.Instance.PlayPlayerHurtSound(transform.position, 10f);
    }

    public float GetPlayerHealth()
    {
        return health;
    }
}
