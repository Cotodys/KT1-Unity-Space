using UnityEngine;

namespace Player
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private float speed = 5f;

        private int _currentHp = 3;
        private float _minX;
        private float _maxX;

        private void Start()
        {
            Camera mainCam = Camera.main;
            if (mainCam != null)
            {
                float spriteHalfWidth = 0.5f; 
                _minX = mainCam.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + spriteHalfWidth;
                _maxX = mainCam.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - spriteHalfWidth;
            }
        }

        public void Move(float direction)
        {
            Vector3 newPosition = transform.position + Vector3.right * direction * speed * Time.deltaTime;

 
            newPosition.x = Mathf.Clamp(newPosition.x, _minX, _maxX);

            transform.position = newPosition;
        }

        public void TakeDamage()
        {
            _currentHp--;
            Debug.Log($"[Player] Получил урон! Осталось ХП: {_currentHp}");
            if (_currentHp <= 0)
            {
                Debug.Log("[Player] Смерть игрока!");
            }
        }
    }
}
