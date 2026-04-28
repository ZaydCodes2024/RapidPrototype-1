using System;
using UnityEngine;

public class PlayerHUDAnimations : MonoBehaviour
{
    [SerializeField] private Animator enemyKillCounterAnimator;
    [SerializeField] private Animator playerHealthAnimator;
    [SerializeField] private Animator playerVolumeAnimator;
    private const string ENEMY_KILLED = "EnemyKilled";
    private const string HEALTH_FLASH = "HealthFlash";
    private const string DAMAGE_FLASH = "DamageFlash";
    private void Start()
    {
        Enemy.OnKilledByPlayer += Enemy_OnKilledByPlayer;
        PlayerHealth.Instance.OnDamageTaken += playerHealth_OnDamageTaken;
    }

    private void Enemy_OnKilledByPlayer(object sender, EventArgs e)
    {
        enemyKillCounterAnimator.SetTrigger(ENEMY_KILLED);
    }

    private void playerHealth_OnDamageTaken(object sender, EventArgs e)
    {
        playerHealthAnimator.SetTrigger(HEALTH_FLASH);
        playerVolumeAnimator.SetTrigger(DAMAGE_FLASH);
    }

}
