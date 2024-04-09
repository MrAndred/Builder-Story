using System;
using UnityEngine;

namespace BuilderStory.Lifting
{
    public interface IReadOnlyLift
    {
        public event Action<ILiftable> PickedUp;

        public event Action<ILiftable, Transform> OnDropped;
    }
}
