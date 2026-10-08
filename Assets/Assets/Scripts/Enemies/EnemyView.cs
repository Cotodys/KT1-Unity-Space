using UnityEngine;
using System;

namespace Enemies
{
    public class EnemyView : MonoBehaviour
    {
        public event Action<EnemyView> OnDestroyed;

        [SerializeField] private int pointsValue = 10; 

        public int PointsValue => pointsValue;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Bullet"))
            {
                OnDestroyed?.Invoke(this);

                Destroy(other.gameObject);
                Destroy(gameObject);
            }
        }
    }
}
