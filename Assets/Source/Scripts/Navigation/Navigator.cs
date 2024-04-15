using BuilderStory.BuildingMaterial;
using BuilderStory.Struct;
using BuilderStory.Warehouse;
using UnityEngine;

namespace BuilderStory.Navigation
{
    public class Navigator : MonoBehaviour
    {
        private Trash[] _trashes;
        private MaterialWarehouse[] _materialSources;

        public void Init(MaterialWarehouse[] materialSources, Trash[] trashes)
        {
            _materialSources = materialSources;
            _trashes = trashes;
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

        public Transform GetRandomTrashPoint()
        {
            return _trashes[Random.Range(0, _trashes.Length)].transform;
        }
    }
}
