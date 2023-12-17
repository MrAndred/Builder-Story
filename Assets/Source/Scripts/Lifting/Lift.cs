using System;
using System.Collections.Generic;
using UnityEngine;

namespace BuilderStory
{
    public class Lift : MonoBehaviour
    {
        [SerializeField] private float _liftDuration = 1f;
        [SerializeField] private float _maxCapacity = 10f;

        public Transform Destination { get; set; }

        private List<ILiftable> _liftables = new List<ILiftable>();

        public event Action PickedUp;

        public event Action Placed;

        public IReadOnlyList<ILiftable> Liftables => _liftables;

        public float Duration => _liftDuration;

        public bool IsFull => _liftables.Count >= _maxCapacity;

        public bool IsEmpty => _liftables.Count == 0;

        public void PickUp(ILiftable liftable, Transform point)
        {
            if (_liftables.Count >= _maxCapacity)
            {
                return;
            }

            liftable.PickUp(point, _liftDuration);
            liftable.OnPickedUp += OnPickedUp;
        }

        public void Place(ILiftable liftable, Transform point)
        {
            if (_liftables.Count <= 0f)
            {
                return;
            }

            liftable.Place(point, _liftDuration);
            liftable.OnPlaced += OnPlaced;
        }

        private void OnPlaced(ILiftable liftable)
        {
            liftable.OnPlaced -= OnPlaced;
            _liftables.Remove(liftable);
            Placed?.Invoke();
        }

        private void OnPickedUp(ILiftable liftable)
        {
            liftable.OnPickedUp -= OnPickedUp;
            _liftables.Add(liftable);
            PickedUp?.Invoke();
        }
    }
}
