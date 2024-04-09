using System;
using BuilderStory.BuildingMaterial;
using UnityEngine;

namespace BuilderStory.Lifting
{
    public interface ILiftable
    {
        public event Action<ILiftable> PickedUp;

        public event Action<ILiftable> Placed;

        public Vector3 Position { get; }

        public MaterialType Type { get; }

        public bool IsPickedUp { get; }

        public bool IsPlaced { get; }

        public void PickUp(Transform transform, float duration, Vector3 offset);

        public void Place(Transform transform, float duration);
    }
}
