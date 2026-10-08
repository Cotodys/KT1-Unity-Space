using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class GameUIView : MonoBehaviour
    {
        [Header("UI Elements")]
        [SerializeField] private Text scoreText;
        [SerializeField] private GameObject winPanel;
        [SerializeField] private GameObject losePanel;
        [SerializeField] private Text finalScoreText;

        public void UpdateScore(int newScore)
        {
            if (scoreText != null)
                scoreText.text = $"Счет: {newScore}";
        }

        public void ShowWinScreen(int finalScore)
        {
            if (winPanel != null) winPanel.SetActive(true);
            if (finalScoreText != null) finalScoreText.text = $"Финальный счет: {finalScore}";
        }

        public void ShowLoseScreen()
        {
            if (losePanel != null) losePanel.SetActive(true);
        }
    }
}
