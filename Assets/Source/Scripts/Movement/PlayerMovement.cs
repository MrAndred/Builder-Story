using UnityEngine;

namespace BuilderStory.Movement
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovement : MonoBehaviour
    {
        private const float _checkDistance = 1f;
        private const float PositionY = 0f;
        private const float MinSpeed = 0f;

        private const string Vertical = "Vertical";
        private const string Horizontal = "Horizontal";

        private static readonly int Speed = Animator.StringToHash("Speed");

        [SerializeField] private Animator _animator;
        [SerializeField] private Joystick _joystick;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Transform _groundCheck;

        [SerializeField] private float _rotationSpeed;

        private float _speed;
        private bool _isInitialized;

        public void Init(float speed)
        {
            _speed = speed;

            _isInitialized = true;
        }

        public void Handle()
        {
            if (!_isInitialized)
            {
                return;
            }

            var direction = GetDirection();

            if (direction != Vector3.zero)
            {
                _animator.SetFloat(Speed, direction.magnitude);

                Move(direction);
                Rotate(direction);
            }
            else
            {
                _animator.SetFloat(Speed, MinSpeed);
            }

            if (Physics
                .Linecast(
                    _groundCheck.position,
                    _groundCheck.position + (Vector3.down * _checkDistance)) == false)
            {
                _animator.SetFloat(Speed, MinSpeed);
            }
        }

        public void ChangeSpeed(float speed)
        {
            _speed = speed;
        }

        private Vector3 GetDirection()
        {
            var joystickDirection = new Vector3(_joystick.Horizontal, PositionY, _joystick.Vertical);

            var horizontal = Input.GetAxis(Horizontal);
            var vertical = Input.GetAxis(Vertical);

            var keyboardDirection = new Vector3(horizontal, PositionY, vertical).normalized;

            return joystickDirection != Vector3.zero ? joystickDirection : keyboardDirection;
        }

        private void Move(Vector3 direction)
        {
            _rigidbody.MovePosition(_rigidbody.position + (direction * _speed * Time.fixedDeltaTime));
        }

        private void Rotate(Vector3 direction)
        {
            Quaternion rotation = Quaternion.LookRotation(direction);
            _rigidbody
                .MoveRotation(
                    Quaternion.RotateTowards(
                        _rigidbody.rotation,
                        rotation,
                        _rotationSpeed * Time.fixedDeltaTime));
        }
    }
}
