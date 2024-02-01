using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BuilderStory
{
    public class Lift : MonoBehaviour
    {
        private readonly Vector3 offset = new Vector3(0, 1f, 0);

        [SerializeField] private float _liftDuration = 1f;
        
        private float _maxCapacity = 2f;

        private List<ILiftable> _liftables = new List<ILiftable>();

        public event Action Loaded;

        public event Action Unloaded;

        public ILiftable LastLiftable => _liftables.LastOrDefault();

        public float Duration => _liftDuration;

        public bool IsFull => _liftables.Count >= _maxCapacity;

        public bool IsEmpty => _liftables.Count == 0;

        public bool IsLifting { get; private set; } = false;

        public void Init(int capacity)
        {
            _maxCapacity = capacity;
        }

        public void PickUp(ILiftable liftable, Transform point)
        {
            if (IsLifting == true || _liftables.Count >= _maxCapacity)
            {
                return;
            }

            liftable.PickUp(point, _liftDuration, _liftables.Count * offset);
            liftable.OnPickedUp += PickedUp;
            _liftables.Add(liftable);
            IsLifting = true;
        }

        public void Place(ILiftable liftable, Transform point)
        {
            if (IsLifting == true || _liftables.Count <= 0f)
            {
                return;
            }

            liftable.Place(point, _liftDuration);
            liftable.OnPlaced += Placed;
            _liftables.Remove(liftable);
            IsLifting = true;
        }

        private void PickedUp(ILiftable liftable)
        {
            liftable.OnPickedUp -= PickedUp;
            IsLifting = false;

            if (_liftables.Count >= _maxCapacity)
            {
                Loaded?.Invoke();
            }
        }

        private void Placed(ILiftable liftable)
        {
            liftable.OnPlaced -= Placed;
            IsLifting = false;

            if (_liftables.Count <= 0f)
            {
                Unloaded?.Invoke();
            }
        }
    }
}
