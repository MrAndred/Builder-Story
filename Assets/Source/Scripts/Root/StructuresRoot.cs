using BuilderStory.Audio;
using BuilderStory.Config.Audio;
using BuilderStory.Config.BuildMaterial;
using BuilderStory.Struct;
using UnityEngine;

namespace BuilderStory.Root
{
    public class StructuresRoot : MonoBehaviour
    {
        [SerializeField] private Structure[] _structures;

        public Structure[] Structures => _structures;

        public void Init(
            BuildMaterialMap buildMaterialMap,
            Material highlight,
            AudioManager audioManager,
            AudioMap audioMap)
        {
            foreach (var structure in _structures)
            {
                structure.Init(buildMaterialMap, highlight, audioManager, audioMap);
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
