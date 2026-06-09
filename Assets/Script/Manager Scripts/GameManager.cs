using UnityEngine;
using TMPro;
using System;
using UnityEditor;

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
    private float waitingToStartTimer = 3f;
    private float countdownStartTimer = 2f;
    private float spawnTimer;
    private float spawnTimerMax = 1f;
    private float roundNumberTextTimer = 2f;
    private float roundNumberTextTimerMax = 2f;
    private int enemyCount;
    private bool isUpgrading;
    private enum State
    {
        WaitingToStart, GamePlaying, GameOver, RoundStarting, RoundEndDelay, UpgradePhase
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

    private void Update()
    {
        if (playerHealth.GetPlayerHealth() <= 0)
        {
            state = State.GameOver;
        }

        switch (state)
        {
            case State.WaitingToStart:
                HandleStartCountdown();
                break;

            case State.RoundStarting:
                HandleRoundStart();
                break;

            case State.GamePlaying:
                HandleSpawning();
                CheckRoundEnd();
                break;

            case State.RoundEndDelay:
                HandleRoundEndDelay();
                break;

            case State.UpgradePhase:
                break;

            case State.GameOver:
                Loader.Load(Loader.Scene.GameOverScene);
                break;
        }
    }

    private void HandleStartCountdown()
    {
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
            }
        }
    }

    #region Editor Controls

    [ContextMenu("End Game")]
    public void EndGame() => Loader.Load(Loader.Scene.GameOverScene);
    
    [ContextMenu("End Round")]
    private void EndRound()
    {
        state = State.RoundEndDelay;
        roundTimer = timeBetweenRounds;
    }

    #endregion
    
    #region Round Flow

    private void StartRound()
    {
        state = State.RoundStarting;

        roundNumberText.gameObject.SetActive(true);
        roundNumberText.text = "Round " + currentRound;

        roundNumberTextTimer = roundNumberTextTimerMax;

        enemiesToSpawn = enemiesPerRound + (currentRound - 1) * 2;
        enemiesAlive = 0;
    }
    private void HandleRoundStart()
    {
        roundNumberTextTimer -= Time.deltaTime;

        if (roundNumberTextTimer <= 0)
        {
            roundNumberText.gameObject.SetActive(false);
            state = State.GamePlaying;
        }
    }

    private void CheckRoundEnd()
    {
        if (enemiesAlive == 0 && enemiesToSpawn == 0)
        {
            EndRound();
        }
    }
    private void HandleRoundEndDelay()
    {
        roundTimer -= Time.deltaTime;

        if (roundTimer <= 0)
        {
            if (currentRound % 5 == 0)
            {
                EnterUpgradePhase();
            }
            else
            {
                StartNextRound();
            }
        }
    }
    private void StartNextRound()
    {
        currentRound++;
        StartRound();
    }

    #endregion
    
    #region Upgrade Phase
    public void UpgradeChosen()
    {
        Player.Instance.LockCursorState();

        isUpgrading = false;

        OnUpgradeSelected?.Invoke(this, EventArgs.Empty);

        StartNextRound();
    }

    private void EnterUpgradePhase()
    {
        state = State.UpgradePhase;
        isUpgrading = true;

        OnUpgradePhaseStarted?.Invoke(this, EventArgs.Empty);
    }
    #endregion 

    #region Enemy System
    private void Enemy_OnDestroyed(object sender, System.EventArgs e) => enemiesAlive--;

    private void Enemy_OnKilledByPlayer(object sender, System.EventArgs e)
    {
        enemyCount++;
        enemiesAlive--;
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
    private void HandleSpawning()
    {
        if (enemiesToSpawn <= 0) return;

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

    #endregion
    
    public int GetEnemyKilledCount() => enemyCount;
    public int GetRoundsCompleted() => currentRound - 1;
    public bool IsPlayerUpgrading() => isUpgrading;
}
