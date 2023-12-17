using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;

namespace BuilderStory
{
    public class Movement : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _rotationDuration;

        private Coroutine _moveCoroutine;

        public event Action TargetReached;

        public float Speed => _speed;

        public Vector3 TargetPosition { get; private set; }

        public void MoveTo(Vector3 targetPosition)
        {
            if (_moveCoroutine != null)
            {
                StopCoroutine(_moveCoroutine);
            }

            _moveCoroutine = StartCoroutine(MoveToCoroutine(targetPosition));
        }

        public void Stop()
        {
            if (_moveCoroutine != null)
            {
                StopCoroutine(_moveCoroutine);
            }
        }

        public void SetTargetPosition(Vector3 position)
        {
            TargetPosition = position;
        }

        private IEnumerator MoveToCoroutine(Vector3 targetPosition)
        {
            transform.DOLookAt(targetPosition, _rotationDuration);

            targetPosition = new Vector3(targetPosition.x, transform.position.y, targetPosition.z);

            while (transform.position != targetPosition)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, _speed * Time.deltaTime);
                yield return null;
            }

            TargetPosition = Vector3.zero;
            TargetReached?.Invoke();
        }
    }
}
