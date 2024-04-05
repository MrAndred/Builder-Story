using System;
using UnityEngine;

namespace BuilderStory
{
    public interface IReadOnlyLift
    {
        public event Action<ILiftable> OnPickedUp;

        public event Action<ILiftable, Transform> OnDropped;
    }
}
