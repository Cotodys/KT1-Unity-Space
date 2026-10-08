using UnityEngine;
using Player; // Подключаем папку игрока

namespace Infrastructure
{
    public class GameBootstrapper : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private PlayerView playerPrefab;
        [SerializeField] private Transform spawnPoint;

        private PlayerController _playerController;

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
            Debug.Log("[Bootstrapper] Системы готовы. Создаем игрока...");

            PlayerView playerInstance = Instantiate(playerPrefab, spawnPoint.position, Quaternion.identity);

            _playerController = new PlayerController(playerInstance);
        }

        private void Update()
        {
            _playerController?.Update();
        }
    }
}
