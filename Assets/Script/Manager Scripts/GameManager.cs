using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get; private set;}
    [SerializeField] private Transform enemyPrefab;
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private float minSpawnDistance;
    [SerializeField] private float maxSpawnDistance;
    [SerializeField] private float minSpawnHeight;
    [SerializeField] private float maxSpawnHeight;
    [SerializeField] private TextMeshProUGUI gameStartTimerText;
    [SerializeField] private TextMeshProUGUI roundNumberText;
    [SerializeField] private int enemiesPerRound = 5;
    [SerializeField] private float timeBetweenRounds = 3f;
    private int currentRound = 1;
    private int enemiesAlive;
    private int enemiesToSpawn;
    private float roundTimer;
    private bool isRoundActive;
    private bool isRoundStarting;
    private float waitingToStartTimer = 3f;
    private float countdownStartTimer = 2f;
    private float spawnTimer;
    private float spawnTimerMax = 1f;
    private float roundNumberTextTimer = 2f;
    private float roundNumberTextTimerMax = 2f;
    private int enemyCount;
    private enum State
    {
        WaitingToStart, GamePlaying, GameOver
    }
    private State state;
    private void Awake()
    {
        Instance = this;
        gameStartTimerText.gameObject.SetActive(false);
        state = State.WaitingToStart;
    }
    private void Start()
    {
        Enemy.OnKilledByPlayer += Enemy_OnKilledByPlayer;
        Enemy.OnDestroyed += Enemy_OnDestroyed;
    }

    private void Enemy_OnDestroyed(object sender, System.EventArgs e)
    {
        enemiesAlive--;
    }

    private void Enemy_OnKilledByPlayer(object sender, System.EventArgs e)
    {
        enemyCount++;
        enemiesAlive--;

        if (isRoundActive && enemiesAlive == 0 && enemiesToSpawn == 0)
        {
            EndRound();
        }
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
                    gameStartTimerText.gameObject.SetActive(true);
                    waitingToStartTimer -= Time.deltaTime;

                    if (waitingToStartTimer <= 0)
                    {
                        gameStartTimerText.gameObject.SetActive(false);
                        StartRound();
                        state = State.GamePlaying;
                    }
                }
                break;

            case State.GamePlaying:
                
                StartRoundNumberTimer();

                HandleSpawning();

                if (!isRoundActive && !isRoundStarting)
                {
                    roundTimer -= Time.deltaTime;

                    if (roundTimer <= 0)
                    {
                        currentRound++;
                        StartRound();
                    }
                }

                if (isRoundActive && enemiesAlive == 0 && enemiesToSpawn == 0)
                {
                    EndRound();
                }

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

    [ContextMenu("End Game")]
    public void EndGame()
    {
        Loader.Load(Loader.Scene.GameOverScene);
    }
    private void StartRoundNumberTimer()
    {
        if (!isRoundStarting) return;

        roundNumberTextTimer -= Time.deltaTime;

        if (roundNumberTextTimer <= 0)
        {
            roundNumberText.gameObject.SetActive(false);
            isRoundStarting = false;
            isRoundActive = true;
            roundNumberTextTimer = roundNumberTextTimerMax;
        }
    }
    private void HandleSpawning()
    {
        if (!isRoundActive) return;

        if (enemiesToSpawn > 0)
        {
            spawnTimer -= Time.deltaTime;

            if (spawnTimer <= 0f)
            {
                SpawnEnemy();

                enemiesToSpawn--;
                enemiesAlive++;

                spawnTimer = spawnTimerMax;
            }
        }
    }
    private void StartRound()
    {
        isRoundStarting = true;
        isRoundActive = false;

        roundNumberText.gameObject.SetActive(true);
        roundNumberText.text = "Round " + currentRound;
        roundNumberTextTimer = roundNumberTextTimerMax;

        enemiesToSpawn = enemiesPerRound + (currentRound - 1) * 2; // difficulty scaling
        enemiesAlive = 0;
    }
    private void EndRound()
    {
        isRoundActive = false;
        roundTimer = timeBetweenRounds;
    }
    private void SpawnEnemy()
    {
        if (enemyPrefab == null || Player.Instance == null)     return;

        Vector3 playerPosition = Player.Instance.transform.position;
        Vector2 randomCircle = Random.insideUnitCircle.normalized;

        float randomDistance = Random.Range(minSpawnDistance, maxSpawnDistance);

        Vector3 spawnPosition = playerPosition + new Vector3(randomCircle.x, 0 , randomCircle.y) * randomDistance;
        spawnPosition.y = playerPosition.y + Random.Range(minSpawnHeight, maxSpawnHeight);
        
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }

    public int GetEnemyKilledCount()
    {
        return enemyCount;
    }
}
