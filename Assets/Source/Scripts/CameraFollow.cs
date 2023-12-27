using UnityEngine;

namespace BuilderStory
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Vector3 _offset;

        private Player _player;
        private Camera _camera;
        private bool _isInitialized;

        public void Init(Player player)
        {
            _player = player;
            _camera = Camera.main;
            _isInitialized = true;
        }

        private void LateUpdate()
        {
            if (_isInitialized == false)
            {
                return;
            }

            Follow();
        }

        private void Follow()
        {
            var cameraPostion = _player.transform.position + _offset;

            _camera.transform.position = cameraPostion;
        }
    }
}
