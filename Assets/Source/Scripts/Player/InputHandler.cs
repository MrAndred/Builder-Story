using UnityEngine;

namespace BuilderStory
{
    public class InputHandler : MonoBehaviour
    {
        [SerializeField] private Joystick _joystick;

        private bool _isInitialized;

        private void Update()
        {
            if (!_isInitialized)
            {
                return;
            }
        }

        public void Init()
        {
            _isInitialized = true;
        }
    }
}
