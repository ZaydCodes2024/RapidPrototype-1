using System;
using UnityEngine;

public class Enemy : MonoBehaviour, IHealth
{
    private float health = 100f;
    private float maxHealth = 100f;
    public void TakeDamage(float damage)
    {
        health -= damage;
        // Debug.Log($"Damage dealt to {gameObject.name}, health: {health} ");

        if (health <= 0)
        {
            health = maxHealth;
            DestroySelf();
        }
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}
