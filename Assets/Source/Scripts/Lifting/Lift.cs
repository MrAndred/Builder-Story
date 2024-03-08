using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BuilderStory
{
    public class Lift : MonoBehaviour, IReadOnlyLift
    {
        private const string PickUpAudioKey = "PickUp";
        private const string PlaceAudioKey = "Place";

        private readonly Vector3 offset = new Vector3(0, 1f, 0);

        [SerializeField] private float _liftDuration = 1f;
        
        private float _maxCapacity = 2f;

        private List<ILiftable> _liftables = new List<ILiftable>();

        public event Action Loaded;

        public event Action Unloaded;

        public event Action<ILiftable> OnPickedUp;

        public event Action<ILiftable, Transform> OnDropped;

        public ILiftable LastLiftable => _liftables.LastOrDefault();

        public ILiftable[] Liftables => _liftables.ToArray();

        public int Length => _liftables.Count;

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

            AudioManager.Instance.PlaySFX(AudioMap.Instance.GetAudioClip(PickUpAudioKey));

            liftable.PickUp(point, _liftDuration, _liftables.Count * offset);
            liftable.OnPickedUp += PickedUp;

            OnPickedUp?.Invoke(liftable);

            _liftables.Add(liftable);
            IsLifting = true;
        }

        public void Place(ILiftable liftable, Transform point)
        {
            if (IsLifting == true || _liftables.Count <= 0f)
            {
                return;
            }

            AudioManager.Instance.PlaySFX(AudioMap.Instance.GetAudioClip(PlaceAudioKey));

            liftable.Place(point, _liftDuration);
            liftable.OnPlaced += Placed;

            OnDropped?.Invoke(liftable, point);

            _liftables.Remove(liftable);
            IsLifting = true;
        }

        public void ChangeCapacity(int capacity)
        {
            _maxCapacity = capacity;
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
