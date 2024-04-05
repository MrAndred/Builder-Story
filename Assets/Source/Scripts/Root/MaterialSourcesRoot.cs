using UnityEngine;

namespace BuilderStory
{
    public class MaterialSourcesRoot : MonoBehaviour
    {
        [SerializeField] private MaterialWarehouse[] _materialSources;

        public MaterialWarehouse[] MaterialSources => _materialSources;

        public void Init()
        {
            foreach (var source in _materialSources)
            {
                source.Init();
            }
        }
    }
}
