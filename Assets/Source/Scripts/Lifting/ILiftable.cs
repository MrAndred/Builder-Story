using System;
using UnityEngine;

namespace BuilderStory
{
    public interface ILiftable
    {
        public event Action<ILiftable> OnPickedUp;

        public event Action<ILiftable> OnPlaced;

        public MaterialType Type { get; }

        public bool IsPickedUp { get; }

        public bool IsPlaced { get; }

        public void PickUp(Transform transform, float duration, Vector3 offset);

        public void Place(Transform transform, float duration);
    }
}
