using UnityEngine;

namespace Player
{
    public class PlayerController
    {
        private readonly PlayerView _playerView;

        public PlayerController(PlayerView playerView)
        {
            _playerView = playerView;
        }

        public void Update()
        {
            if (_playerView == null) return;

            float horizontalInput = Input.GetAxisRaw("Horizontal");

            if (horizontalInput != 0)
            {
                _playerView.Move(horizontalInput);
            }
        }
    }
}
