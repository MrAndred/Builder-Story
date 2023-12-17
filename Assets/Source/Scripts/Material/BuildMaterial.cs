using System;
using DG.Tweening;
using UnityEngine;

namespace BuilderStory
{
    public class BuildMaterial : MonoBehaviour, ILiftable
    {
        [SerializeField] private MaterialType _type;

        public event Action<ILiftable> OnPickedUp;

        public event Action<ILiftable> OnPlaced;

        public MaterialType Type => _type;

        public bool IsPlaced { get; private set; }

        public bool IsPickedUp { get; private set; }

        public Transform Transform => transform;

        public void PickUp(Transform point, float duration)
        {
            transform.SetParent(point);
            transform
                .DOLocalMove(Vector3.zero, duration)
                .SetEase(Ease.OutFlash)
                .OnComplete(() =>
                {
                    OnPickedUp?.Invoke(this);
                    IsPickedUp = true;
                });

            transform.DOScale(Vector3.one, duration)
                .SetEase(Ease.Linear);
        }

        public void Place(Transform point, float duration)
        {
            transform.SetParent(point);
            transform.DOScale(Vector3.one, duration)
                .SetEase(Ease.Linear);
            transform.DOLocalRotate(Vector3.zero, duration)
                .SetEase(Ease.Linear);
            transform.DOLocalMove(Vector3.zero, duration)
                .SetEase(Ease.OutFlash)
                .OnComplete(() =>
                {
                    OnPlaced?.Invoke(this);
                    IsPlaced = true;
                });
        }
    }
}
