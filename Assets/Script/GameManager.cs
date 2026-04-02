using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform enemyPrefab;
    [SerializeField] private float minSpawnDistance;
    [SerializeField] private float maxSpawnDistance;
    [SerializeField] private float minSpawnHeight;
    [SerializeField] private float maxSpawnHeight;
    private float spawnTimer;
    private float spawnTimerMax = 5f;

    private void Update()
    {
        SpawnEnemy();
    }
    private void SpawnEnemy()
    {
        if (enemyPrefab == null && Player.Instance == null)     return;

        Vector3 playerPosition = Player.Instance.transform.position;
        Vector2 randomCircle = Random.insideUnitCircle.normalized;

        float randomDistance = Random.Range(minSpawnDistance, maxSpawnDistance);

        Vector3 spawnPosition = playerPosition + new Vector3(randomCircle.x, 0 , randomCircle.y) * randomDistance;
        spawnPosition.y = playerPosition.y + Random.Range(minSpawnHeight, maxSpawnHeight);

        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0)
        {
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            spawnTimer = spawnTimerMax;
        }
    }
}
