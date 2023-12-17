using UnityEngine;

namespace BuilderStory
{
    public class Navigator : MonoBehaviour
    {
        private MaterialWarehouse[] _materialSources;

        public void Init(MaterialWarehouse[] materialSources)
        {
            _materialSources = materialSources;
        }

        public bool TryGetMaterialPosition(BuildMaterial material, out Transform position)
        {
            foreach (var materialSource in _materialSources)
            {
                if (materialSource.Type == material.Type)
                {
                    position = materialSource.transform;
                    return true;
                }
            }

            position = null;
            return false;
        }
    }
}
