using System;
using UnityEngine;

public class Enemy : MonoBehaviour, IHealth
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private ParticleSystem hurtParticles;
    [SerializeField] private ParticleSystem deathParticles;
    private Animator animator;
    private const string ENEMY_FLASH = "Flash";
    private float health = 100f;
    private float maxHealth = 100f;
    private float enemyDamage = 10f;
    private Rigidbody enemyRb;
    public static event EventHandler OnKilledByPlayer;
    public static event EventHandler OnDestroyed;

    public static void ResetStaticData()
    {
        OnKilledByPlayer = null;
        OnDestroyed = null;
    }
    private void Awake()
    {
        enemyRb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        EnemyMovement();
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        
        SoundManager.Instance.PlayEnemyHurtSound(Player.Instance.GetCameraTransform().position, 2.5f);

        Instantiate(hurtParticles, transform.position, transform.rotation);

        animator.SetTrigger(ENEMY_FLASH);

        if (health <= 0)
        {
            health = maxHealth;
            KilledByPlayer();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<PlayerHealth>() == null)  return;

        if (collision.gameObject.TryGetComponent(out PlayerHealth playerHealth))
        {
            playerHealth.TakeDamage(enemyDamage);
            Destroy(gameObject);
            OnDestroyed?.Invoke(this, EventArgs.Empty);
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
    private void KilledByPlayer()
    {
        OnKilledByPlayer?.Invoke(this, EventArgs.Empty);

        SoundManager.Instance.PlayEnemyDeathSound(Player.Instance.GetCameraTransform().position, 2.5f);

        Instantiate(deathParticles, transform.position, transform.rotation);

        Destroy(gameObject);
    }
}
