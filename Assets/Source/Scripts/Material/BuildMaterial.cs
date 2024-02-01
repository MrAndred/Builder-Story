using DG.Tweening;
using System;
using UnityEngine;

namespace BuilderStory
{
    public class BuildMaterial : MonoBehaviour, ILiftable
    {
        private const int JumpCount = 1;

        [SerializeField] private MaterialType _type;
        [SerializeField] private float _jumpForce = 3f;

        private Sequence _pickupSequence;
        private Sequence _placeSequence;

        private Transform _parent;
        private Vector3 _originScale;

        public event Action<ILiftable> OnPickedUp;

        public event Action<ILiftable> OnPlaced;

        public Vector3 Position => transform.position;

        public MaterialType Type => _type;

        public bool IsPlaced { get; private set; }

        public bool IsPickedUp { get; private set; }

        public Transform Point => transform;

        private void OnEnable()
        {
            _parent = transform.parent;
            _originScale = transform.localScale;
        }

        private void OnDisable()
        {
            _pickupSequence?.Kill();
        }

        public void PickUp(Transform point, float duration, Vector3 offset)
        {
            transform.SetParent(point);

            transform.localRotation = Quaternion.identity;
            Tween jump = transform.DOLocalJump(Vector3.zero + offset, _jumpForce, JumpCount, duration)
                .SetEase(Ease.OutFlash);

            _pickupSequence = DOTween.Sequence();

            _pickupSequence.Append(jump);

            _pickupSequence.OnComplete(() =>
            {
                OnPickedUp?.Invoke(this);
                IsPickedUp = true;
            });

            _pickupSequence.Play();
        }

        public void Place(Transform point, float duration)
        {
            transform.SetParent(null);

            Tween scale = transform.DOScale(Vector3.zero, duration)
                .SetEase(Ease.Linear);

            transform.localRotation = Quaternion.identity;

            Tween jump = transform.DOJump(point.transform.position, _jumpForce, JumpCount, duration);

            _placeSequence = DOTween.Sequence();

            _placeSequence.Append(scale);
            _placeSequence.Join(jump);

            _placeSequence.OnComplete(() =>
            {
                OnPlaced?.Invoke(this);
                IsPlaced = true;
                gameObject.SetActive(false);
                ResetOptions();
            });
        }

        private void ResetOptions()
        {
            transform.SetParent(_parent);
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            transform.localScale = _originScale;
        }
    }
}
