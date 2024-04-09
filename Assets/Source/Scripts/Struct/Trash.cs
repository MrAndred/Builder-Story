using BuilderStory.BuildingMaterial;
using BuilderStory.Lifting;
using UnityEngine;

namespace BuilderStory.Struct
{
    public class Trash : MonoBehaviour, IBuildable
    {
        [SerializeField] private Transform _trashPoint;
        [SerializeField] private ParticleSystem _placeEffect;

        public bool IsBuilding { get; private set; } = true;

        public bool IsBuilt()
        {
            return false;
        }

        public bool TryGetBuildMaterial(out BuildMaterial buildMaterial)
        {
            buildMaterial = null;
            return true;
        }

        public bool CouldPlaceMaterial(ILiftable material)
        {
            return true;
        }

        public bool TryPlaceMaterial(ILiftable liftable, float placeDuration, out Transform destination)
        {
            destination = _trashPoint;

            liftable.Placed += OnPlaced;
            return true;
        }

        public Transform GetMaterialPoint(ILiftable material)
        {
            return _trashPoint;
        }

        private void OnPlaced(ILiftable material)
        {
            _placeEffect.transform.position = material.Position;
            _placeEffect.Play();
            material.Placed -= OnPlaced;
        }
    }
}
