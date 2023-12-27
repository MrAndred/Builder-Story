using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuilderStory
{
    public class Trash : MonoBehaviour, IBuildable
    {
        [SerializeField] private Transform _trashPoint;

        public bool IsBuilt()
        {
            return false;
        }

        public bool TryGetBuildMaterial(out BuildMaterial buildMaterial)
        {
            buildMaterial = null;
            return true;
        }

        public bool TryPlaceMaterial(ILiftable liftable, out Transform destination)
        {
            destination = _trashPoint;
            return true;
        }
    }
}
