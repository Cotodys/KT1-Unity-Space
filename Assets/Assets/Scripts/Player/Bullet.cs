using UnityEngine;

namespace Player
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float speed = 10f;
        private float _topScreenBoundary;

        private void Start()
        {
            if (Camera.main != null)
            {
                _topScreenBoundary = Camera.main.ViewportToWorldPoint(new Vector3(0f, 1f, 0f)).y + 1f;
            }
        }

        private void Update()
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime);

            if (transform.position.y > _topScreenBoundary)
            {
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Enemy"))
            {
                Destroy(gameObject);
            }
        }
    }
}
