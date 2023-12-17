using UnityEngine;

namespace BuilderStory
{
    public class GameRoot : MonoBehaviour
    {
        [SerializeField] private MaterialWarehouse[] _materialSource;
        [SerializeField] private Worker[] _workers;
        [SerializeField] private Navigator _navigator;

        [SerializeField] private Structure _structure;
        [SerializeField] private Builder _builder;

        private void Start()
        {
            foreach (var source in _materialSource)
            {
                source.Init();
            }

            _navigator.Init(_materialSource);

            foreach (var worker in _workers)
            {
                worker.Init();
            }

            _builder.Init(_structure, _workers, _navigator);
        }
    }
}
