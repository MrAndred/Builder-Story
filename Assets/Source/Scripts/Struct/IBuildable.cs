using UnityEngine;

namespace BuilderStory
{
    public interface IBuildable
    {
        public bool IsBuilding { get; }

        public bool TryGetBuildMaterial(out BuildMaterial buildMaterial);

        public bool CouldPlaceMaterial(ILiftable material);

        public Transform GetMaterialPoint(ILiftable material);

        public bool TryPlaceMaterial(ILiftable liftable, out Transform destination);

        public bool IsBuilt();
    }
}
