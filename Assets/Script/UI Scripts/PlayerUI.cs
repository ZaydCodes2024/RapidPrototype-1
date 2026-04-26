using System;
using TMPro;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI enemiesKilledText;
    [SerializeField] private Animator enemyKillCounterAnimator;
    [SerializeField] private Animator playerHealthAnimator;
    [SerializeField] private PlayerHealth playerHealth;
    private const string ENEMY_KILLED = "EnemyKilled";
    private const string HEALTH_FLASH = "HealthFlash";
    
    private void Start()
    {
        Enemy.OnKilledByPlayer += Enemy_OnKilledByPlayer;
        playerHealth.OnDamageTaken += playerHealth_OnDamageTaken;
    }

    private void playerHealth_OnDamageTaken(object sender, EventArgs e)
    {
        playerHealthAnimator.SetTrigger(HEALTH_FLASH);
    }

    private void Enemy_OnKilledByPlayer(object sender, EventArgs e)
    {
        enemyKillCounterAnimator.SetTrigger(ENEMY_KILLED);
    }

    private void Update()
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        healthText.text = playerHealth.GetPlayerHealth().ToString();
        enemiesKilledText.text = GameManager.Instance.GetEnemyKilledCount().ToString();
    }
}
