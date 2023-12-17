using UnityEngine;

namespace BuilderStory
{
    public class Structure : MonoBehaviour, IBuildable
    {
        [SerializeField] private StructureMaterial[] _structMaterials;

        private void OnValidate()
        {
            var materials = GetComponentsInChildren<BuildMaterial>();

            _structMaterials = new StructureMaterial[materials.Length];

            for (int i = 0; i < materials.Length; i++)
            {
                _structMaterials[i] = new StructureMaterial(materials[i]);
            }
        }

        public bool TryGetBuildMaterial(out BuildMaterial buildMaterial)
        {
            foreach (var material in _structMaterials)
            {
                if (material.IsPlaced == false)
                {
                    buildMaterial = material.Material;
                    return true;
                }
            }

            buildMaterial = null;
            return false;
        }

        public bool TryPlaceMaterial(ILiftable material, out Transform destination)
        {
            foreach (var structMaterial in _structMaterials)
            {
                if (structMaterial.IsPlaced == false && structMaterial.Material.Type == material.Type)
                {
                    structMaterial.Place();
                    destination = structMaterial.Material.transform;
                    return true;
                }
            }

            destination = null;
            return false;
        }

        public bool IsBuilt()
        {
            foreach (var material in _structMaterials)
            {
                if (!material.IsPlaced)
                {
                    return false;
                }
            }

            return true;
        }
    }
}