using UnityEngine;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get; private set;}
    public event EventHandler OnUpgradePhaseStarted;
    public event EventHandler OnUpgradeSelected;
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
    private bool isUpgrading;
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

    private void UpgradeSkillSingleUI_onSelectButtonPressed(object sender, System.EventArgs e)
    {
        isUpgrading = false;
    }

    private void Enemy_OnDestroyed(object sender, System.EventArgs e) => enemiesAlive--;

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

                if (!isRoundActive && !isRoundStarting && !isUpgrading)
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

    public void UpgradeChosen()
    {
        Player.Instance.LockCursorState();

        isUpgrading = false;

        OnUpgradeSelected?.Invoke(this, EventArgs.Empty);

        currentRound++;
        StartRound();
    }

    [ContextMenu("End Game")]
    public void EndGame() => Loader.Load(Loader.Scene.GameOverScene);
    private void StartRoundNumberTimer()
    {
        if (!isRoundStarting && IsPlayerUpgrading()) return;

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
        if (!isRoundActive && IsPlayerUpgrading()) return;

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
        if (IsPlayerUpgrading())
        {
            isUpgrading = false;
            return;
        }

        isRoundStarting = true;
        isRoundActive = false;
        
        roundNumberText.gameObject.SetActive(true);
        roundNumberText.text = "Round " + currentRound;
        roundNumberTextTimer = roundNumberTextTimerMax;

        enemiesToSpawn = enemiesPerRound + (currentRound - 1) * 2; // difficulty scaling
        enemiesAlive = 0;
    }
    
    [ContextMenu("End Round")]
    private void EndRound()
    {
        isRoundActive = false;
        roundTimer = timeBetweenRounds;
        isUpgrading = true;
        OnUpgradePhaseStarted?.Invoke(this, EventArgs.Empty);
    }
    private void SpawnEnemy()
    {
        if (enemyPrefab == null || Player.Instance == null)     return;

        Vector3 playerPosition = Player.Instance.transform.position;
        Vector2 randomCircle = UnityEngine.Random.insideUnitCircle.normalized;

        float randomDistance = UnityEngine.Random.Range(minSpawnDistance, maxSpawnDistance);

        Vector3 spawnPosition = playerPosition + new Vector3(randomCircle.x, 0 , randomCircle.y) * randomDistance;
        spawnPosition.y = playerPosition.y + UnityEngine.Random.Range(minSpawnHeight, maxSpawnHeight);
        
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }

    public int GetEnemyKilledCount() => enemyCount;
    public int GetRoundsCompleted() => currentRound - 1;
    public bool IsPlayerUpgrading() => isUpgrading;
}
