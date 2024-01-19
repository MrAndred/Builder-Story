using UnityEngine;

namespace BuilderStory
{
    public class CameraFollow : MonoBehaviour
    {
        private const float _smoothTime = 0.23f;

        [SerializeField] private Vector3 _offset;

        private Player _player;
        private Camera _camera;
        private bool _isInitialized;

        private Vector3 _velocity = Vector3.zero;

        public void Init(Player player)
        {
            _player = player;
            _camera = Camera.main;
            _isInitialized = true;
        }

        private void FixedUpdate()
        {
            if (!_isInitialized)
            {
                return;
            }

            Follow();
        }

        private void Follow()
        {
            Vector3 targetPosition = _player.transform.position + _offset;
            _camera.transform.position = Vector3.SmoothDamp(_camera.transform.position, targetPosition, ref _velocity, _smoothTime);
        }
    }
}
