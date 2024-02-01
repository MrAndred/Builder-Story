using UnityEngine;

namespace BuilderStory
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovement : MonoBehaviour
    {
        private const string Speed = "Speed";

        [SerializeField] private Animator _animator;
        [SerializeField] private Joystick _joystick;
        [SerializeField] private Rigidbody _rigidbody;

        [SerializeField] private float _rotationSpeed;

        private float _speed;

        private Vector3 _originVector;
        private bool _isInitialized;

        public void Init(float speed)
        {
            _speed = speed;

            _originVector = transform.position;

            _isInitialized = true;
        }

        public void Handle()
        {
            if (!_isInitialized)
            {
                return;
            }

            var direction = new Vector3(_joystick.Horizontal, _originVector.y, _joystick.Vertical);

            if (direction.magnitude > 0)
            {
                _animator.SetFloat(Speed, direction.magnitude);
                
                Move(direction);
                Rotate(direction);
            } else
            {
                _animator.SetFloat(Speed, 0);
            }
        }

        private void Move(Vector3 direction)
        {
            _rigidbody.MovePosition(_rigidbody.position + (direction * _speed * Time.fixedDeltaTime));
        }

        private void Rotate(Vector3 direction)
        {
            Quaternion rotation = Quaternion.LookRotation(direction);
            _rigidbody.MoveRotation(Quaternion.RotateTowards(_rigidbody.rotation, rotation, _rotationSpeed * Time.fixedDeltaTime));
        }
    }
}
