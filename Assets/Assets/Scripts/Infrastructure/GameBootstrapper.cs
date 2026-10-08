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
            Debug.Log("[Bootstrapper] Инициализация систем игры...");
        }

        private void StartGame()
        {
            if (Camera.main != null && spawnPoint != null)
            {
                float bottomY = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0f, 0f)).y;
                float padding = 0.5f;
                Vector3 finalSpawnPosition = new Vector3(spawnPoint.position.x, bottomY + padding, 0f);

                PlayerView playerInstance = Instantiate(playerPrefab, finalSpawnPosition, Quaternion.identity);
                _playerController = new PlayerController(playerInstance);
            }

            _enemyGridController = new EnemyGridController(enemyPrefab, rows, columns, spacing, dropStep, moveInterval);
            _enemyGridController.SpawnGrid();
        }

        private void Update()
        {
            _playerController?.Update();
            _enemyGridController?.Update();
        }
    }
}
