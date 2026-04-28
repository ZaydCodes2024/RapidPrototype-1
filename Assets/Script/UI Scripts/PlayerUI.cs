using System;
using TMPro;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI enemiesKilledText;
    private void Update()
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        healthText.text = PlayerHealth.Instance.GetPlayerHealth().ToString();
        enemiesKilledText.text = GameManager.Instance.GetEnemyKilledCount().ToString();
    }
}
