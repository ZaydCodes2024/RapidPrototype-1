using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private float health = 100f;
    public void TakeDamage(float damage)
    {
        health -= damage;
    }

    public float GetPlayerHealth()
    {
        return health;
    }
}
