using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BuilderStory
{
    public class Lift : MonoBehaviour
    {
        [SerializeField] private float _liftDuration = 1f;
        [SerializeField] private float _maxCapacity = 10f;

        private List<ILiftable> _liftables = new List<ILiftable>();

        public event Action Loaded;

        public event Action Unloaded;

        public ILiftable FirstLiftable => _liftables.First();

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
            liftable.OnPickedUp += PickUp;
            _liftables.Add(liftable);
        }

        public void Place(ILiftable liftable, Transform point)
        {
            if (_liftables.Count <= 0f)
            {
                return;
            }

            liftable.Place(point, _liftDuration);
            liftable.OnPlaced += Place;
            _liftables.Remove(liftable);
        }

        private void PickUp(ILiftable liftable)
        {
            liftable.OnPickedUp -= PickUp;

            if (_liftables.Count >= _maxCapacity)
            {
                Loaded?.Invoke();
            }
        }

        private void Place(ILiftable liftable)
        {
            liftable.OnPlaced -= Place;

            if (_liftables.Count <= 0f)
            {
                Unloaded?.Invoke();
            }
        }
    }
}
