using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private float health = 100f;
    private float maxHealth = 100f;

    public void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log($"Damage dealt to {gameObject.name}, health: {health} ");

        if (health <= 0)
        {
            health = maxHealth;
            Debug.Log("Health Reset!");
        }
    }
}
