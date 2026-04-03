using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private float health = 100f;
    public void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log($"Damage dealt to {gameObject.name}, health: {health} ");
    }

    public float GetPlayerHealth()
    {
        return health;
    }
}
