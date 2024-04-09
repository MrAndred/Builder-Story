using System;
using System.Collections.Generic;
using System.Linq;
using BuilderStory.Audio;
using BuilderStory.Config.Audio;
using UnityEngine;

namespace BuilderStory.Lifting
{
    public class Lift : MonoBehaviour, IReadOnlyLift
    {
        private const string PickUpAudioKey = "PickUp";
        private const string PlaceAudioKey = "Place";

        private readonly Vector3 offset = new Vector3(0, 1f, 0);

        [SerializeField] private float _liftDuration = 1f;

        private float _maxCapacity = 2f;
        private AudioManager _audioManager;
        private AudioMap _audioMap;

        private List<ILiftable> _liftables = new List<ILiftable>();

        public event Action<ILiftable> PickedUp;

        public event Action<ILiftable, Transform> OnDropped;

        public ILiftable LastLiftable => _liftables.LastOrDefault();

        public float Duration => _liftDuration;

        public bool IsFull => _liftables.Count >= _maxCapacity;

        public bool IsEmpty => _liftables.Count <= 0;

        public bool IsLifting { get; private set; } = false;

        public void Init(int capacity, AudioManager audioManager, AudioMap audioMap)
        {
            _maxCapacity = capacity;
            _audioManager = audioManager;
            _audioMap = audioMap;
        }

        public void PickUp(ILiftable liftable, Transform point)
        {
            if (IsLifting || _liftables.Count >= _maxCapacity)
            {
                return;
            }

            _audioManager.PlaySFX(_audioMap.GetAudioClip(PickUpAudioKey));

            liftable.PickUp(point, _liftDuration, _liftables.Count * offset);
            liftable.PickedUp += OnPickedUp;

            PickedUp?.Invoke(liftable);

            _liftables.Add(liftable);
            IsLifting = true;
        }

        public void Place(ILiftable liftable, Transform point)
        {
            if (IsLifting || _liftables.Count <= 0f)
            {
                return;
            }

            _audioManager.PlaySFX(_audioMap.GetAudioClip(PlaceAudioKey));

            liftable.Place(point, _liftDuration);
            liftable.Placed += OnPlaced;

            OnDropped?.Invoke(liftable, point);

            _liftables.Remove(liftable);
            IsLifting = true;
        }

        public void ChangeCapacity(int capacity)
        {
            _maxCapacity = capacity;
        }

        private void OnPickedUp(ILiftable liftable)
        {
            liftable.PickedUp -= OnPickedUp;
            IsLifting = false;
        }

        private void OnPlaced(ILiftable liftable)
        {
            liftable.Placed -= OnPlaced;
            IsLifting = false;
        }
    }
}
