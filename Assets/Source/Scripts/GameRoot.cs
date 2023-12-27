using UnityEngine;

namespace BuilderStory
{
    public class GameRoot : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] private CameraFollow _cameraFollow;

        [SerializeField] private MaterialWarehouse[] _materialSources;
        [SerializeField] private Trash[] _trashes;

        [SerializeField] private Worker[] _workers;
        [SerializeField] private Navigator _navigator;

        [SerializeField] private Structure _structure;
        [SerializeField] private Builder _builder;

        private void Start()
        {
            _player.Init();
            _cameraFollow.Init(_player);

            foreach (var source in _materialSources)
            {
                source.Init();
            }

            _navigator.Init(_materialSources, _trashes);

            foreach (var worker in _workers)
            {
                worker.Init();
            }

            _builder.Init(_structure, _workers, _navigator);
        }
    }
}
