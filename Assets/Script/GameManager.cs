using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform enemyPrefab;
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private float minSpawnDistance;
    [SerializeField] private float maxSpawnDistance;
    [SerializeField] private float minSpawnHeight;
    [SerializeField] private float maxSpawnHeight;
    [SerializeField] private TextMeshProUGUI gameStartTimerText;
    public static GameManager Instance {get; private set;}
    private float spawnTimer;
    private float spawnTimerMax = 2f;
    private float waitingToStartTimer = 3f;
    private float countdownStartTimer = 2f;
    private int enemyCount;
    private enum State
    {
        WaitingToStart, GamePlaying, GameOver
    }
    private State state;
    private void Awake()
    {
        Instance = this;
        state = State.WaitingToStart;
    }
    private void Start()
    {
        Enemy.OnKilledByPlayer += Enemy_OnKilledByPlayer;
    }

    private void Enemy_OnKilledByPlayer(object sender, System.EventArgs e)
    {
        enemyCount++;
    }

    private void Update()
    {
        switch (state)
        {
           case State.WaitingToStart:
                countdownStartTimer -= Time.deltaTime;
                gameStartTimerText.text = Mathf.CeilToInt(waitingToStartTimer).ToString();

                if (countdownStartTimer <= 0)
                {
                    waitingToStartTimer -= Time.deltaTime;

                    if (waitingToStartTimer <= 0)
                    {
                        gameStartTimerText.gameObject.SetActive(false);
                        state = State.GamePlaying;
                    }
                }
                break;

            case State.GamePlaying:
                SpawnEnemy();

                if (playerHealth.GetPlayerHealth() <= 0)
                {
                    state = State.GameOver;
                }
                break;
            
            case State.GameOver:
                Loader.Load(Loader.Scene.GameOverScene);
                break;
        }
        
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

    public int GetEnemyKilledCount()
    {
        return enemyCount;
    }
}
