using UnityEngine;
using Player;
using Enemies;

namespace Infrastructure
{
    public class GameBootstrapper : MonoBehaviour
    {
        [Header("Player Settings")]
        [SerializeField] private PlayerView playerPrefab;
        [SerializeField] private Transform spawnPoint;

        [Header("Enemy Grid Settings")]
        [SerializeField] private EnemyView enemyPrefab;
        [SerializeField] private int rows = 3;
        [SerializeField] private int columns = 6;
        [SerializeField] private float spacing = 1.2f;
        [SerializeField] private float dropStep = 0.5f;
        [SerializeField] private float moveInterval = 2.0f;
        [SerializeField] private UI.GameUIView uiView; 
        private UI.GameLoopManager _gameLoopManager;
        private PlayerController _playerController;
        private EnemyGridController _enemyGridController;



        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            InitializeServices();
            StartGame();
        }

            private void InitializeServices()
        {
            _gameLoopManager = new UI.GameLoopManager();
        }


        private void StartGame()
        {
            int totalEnemies = rows * columns;
            _gameLoopManager.Initialize(totalEnemies);

            if (uiView != null)
            {
                _gameLoopManager.OnScoreChanged += uiView.UpdateScore;
                _gameLoopManager.OnGameLost += uiView.ShowLoseScreen;
                _gameLoopManager.OnGameWon += () => uiView.ShowWinScreen(totalEnemies * 10);
                uiView.UpdateScore(0);
            }

            if (Camera.main != null && spawnPoint != null)
            {
                float bottomY = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0f, 0f)).y;
                float padding = 0.5f;
                Vector3 finalSpawnPosition = new Vector3(spawnPoint.position.x, bottomY + padding, 0f);

                PlayerView playerInstance = Instantiate(playerPrefab, finalSpawnPosition, Quaternion.identity);
                _playerController = new PlayerController(playerInstance);
            }

            _enemyGridController = new EnemyGridController(enemyPrefab, rows, columns, spacing, dropStep, moveInterval, _gameLoopManager);
            _enemyGridController.SpawnGrid();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Time.timeScale = 1f;
                UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
                return;
            }

            _playerController?.Update();
            _enemyGridController?.Update();
        }

    }
}
