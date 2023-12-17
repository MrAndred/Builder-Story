using UnityEngine;

namespace BuilderStory
{
    public interface IBuildable
    {
        public bool TryGetBuildMaterial(out BuildMaterial buildMaterial);

        public bool TryPlaceMaterial(ILiftable liftable, out Transform destination);

        public bool IsBuilt();
    }
}
