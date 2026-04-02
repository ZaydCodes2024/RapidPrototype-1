using System;
using UnityEngine;

public class Enemy : MonoBehaviour, IHealth
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotateSpeed;
    private float health = 100f;
    private float maxHealth = 100f;
    private Rigidbody enemyRb;
    private void Awake()
    {
        enemyRb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        EnemyMovement();
    }
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

    private void EnemyMovement()
    {
        if (Player.Instance != null)
        {
            Vector3 directionToPlayer = Player.Instance.transform.position - transform.position;

            if (directionToPlayer.sqrMagnitude > 0.01f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
                enemyRb.MoveRotation(Quaternion.Lerp(enemyRb.rotation, targetRotation, rotateSpeed * Time.deltaTime));
            }

            Vector3 nextPosition = enemyRb.position + transform.forward * moveSpeed * Time.deltaTime;
            enemyRb.MovePosition(nextPosition);
        }
    }
    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}
