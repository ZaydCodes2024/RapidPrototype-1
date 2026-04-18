using TMPro;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI enemiesKilledText;
    [SerializeField] private PlayerHealth playerHealth;

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
