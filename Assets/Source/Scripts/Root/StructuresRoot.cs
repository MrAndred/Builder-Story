using UnityEngine;

namespace BuilderStory
{
    public class StructuresRoot : MonoBehaviour
    {
        [SerializeField] private Structure[] _structures;

        public Structure[] Structures => _structures;

        public void Init(BuildMaterialMap buildMaterialMap, Material highlight)
        {
            foreach (var structure in _structures)
            {
                structure.Init(buildMaterialMap, highlight);
            }
        }

        public int GetStructureMaterialsCount()
        {
            int structureMaterialsCount = 0;

            foreach (var structure in _structures)
            {
                structureMaterialsCount += structure.MaterialsCount;
            }

            return structureMaterialsCount;
        }
    }
}
