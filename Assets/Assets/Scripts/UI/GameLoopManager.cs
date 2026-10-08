using UnityEngine;
using System;

namespace UI
{
    public class GameLoopManager
    {
        public event Action<int> OnScoreChanged;
        public event Action OnGameWon;
        public event Action OnGameLost;

        private int _score;
        private int _totalEnemies;
        private bool _isGameOver;

        public bool IsGameOver => _isGameOver;

        public void Initialize(int totalEnemies)
        {
            _score = 0;
            _totalEnemies = totalEnemies;
            _isGameOver = false;
            Time.timeScale = 1f;
        }

        public void AddScore(int amount)
        {
            if (_isGameOver) return;

            _score += amount;
            OnScoreChanged?.Invoke(_score); 

            _totalEnemies--;
            if (_totalEnemies <= 0)
            {
                WinGame();
            }
        }

        public void LoseGame()
        {
            if (_isGameOver) return;
            _isGameOver = true;
            Time.timeScale = 0f;
            OnGameLost?.Invoke();
        }

        private void WinGame()
        {
            if (_isGameOver) return;
            _isGameOver = true;
            Time.timeScale = 0f;
            OnGameWon?.Invoke();
        }
    }
}
