using UnityEngine;
using System.Collections.Generic;

namespace Enemies
{
    public class EnemyGridController
    {
        private readonly EnemyView _enemyPrefab;
        private readonly int _rows;
        private readonly int _cols;
        private readonly float _spacing;
        private readonly float _dropStep;
        private readonly float _moveInterval;

        private List<EnemyView> _activeEnemies = new List<EnemyView>();
        private float _timer;

        public EnemyGridController(EnemyView enemyPrefab, int rows, int cols, float spacing, float dropStep, float moveInterval)
        {
            _enemyPrefab = enemyPrefab;
            _rows = rows;
            _cols = cols;
            _spacing = spacing;
            _dropStep = dropStep;
            _moveInterval = moveInterval;
        }

        public void SpawnGrid()
        {
            float startX = -((_cols - 1) * _spacing) / 2f;
            float startY = Camera.main != null ? Camera.main.ViewportToWorldPoint(new Vector3(0f, 0.8f, 0f)).y : 3f;

            for (int r = 0; r < _rows; r++)
            {
                for (int c = 0; c < _cols; c++)
                {
                    Vector3 spawnPos = new Vector3(startX + (c * _spacing), startY - (r * _spacing), 0f);
                    EnemyView enemy = Object.Instantiate(_enemyPrefab, spawnPos, Quaternion.identity);

                    enemy.OnDestroyed += HandleEnemyDestroyed;
                    _activeEnemies.Add(enemy);
                }
            }
        }



        public void Update()
        {
            if (_activeEnemies.Count == 0) return;

            _timer += Time.deltaTime;
            if (_timer >= _moveInterval)
            {
                _timer = 0f;
                MoveEnemiesDown();
            }
        }

        private void MoveEnemiesDown()
        {
            float playerY = Camera.main != null ? Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, 0f)).y + 1f : -4.5f;

            foreach (var enemy in _activeEnemies)
            {
                if (enemy != null)
                {
                    enemy.transform.Translate(Vector3.down * _dropStep);

                    if (enemy.transform.position.y <= playerY)
                    {
                        Debug.Log("[Game Over] Мобы дошли до линии игрока!");
                        Time.timeScale = 0f;
                    }
                }
            }
        }


        private void HandleEnemyDestroyed(EnemyView enemy)
        {
            enemy.OnDestroyed -= HandleEnemyDestroyed;
            _activeEnemies.Remove(enemy);
            Debug.Log($"Моб уничтожен! Оставшихся врагов: {_activeEnemies.Count}");
        }
    }
}
