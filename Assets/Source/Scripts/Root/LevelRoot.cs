using System.Collections;
using UnityEngine;

namespace BuilderStory
{
    public class LevelRoot : MonoBehaviour
    {
        private const int WorkersCount = 5;

        [SerializeField] private Player _player;
        [SerializeField] private CameraFollow _cameraFollow;

        [SerializeField] private MaterialWarehouse[] _materialSources;
        [SerializeField] private Structure[] _structures;
        [SerializeField] private Trash[] _trashes;

        [SerializeField] private Worker[] _workerTemplates;
        [SerializeField] private Transform _workersParent;

        [SerializeField] private Navigator _navigator;

        [SerializeField] private LevelUIRoot _levelUIRoot;

        private ObjectPool<Worker> _workerPool;

        private Wallet _wallet;
        private Reputation _reputation;
        private ProgressSaves _saveObject;

        private void Start()
        {
            _saveObject = new ProgressSaves();
            _saveObject.DataLoaded += OnDataLoaded;

            Init();
        }

        private void Init()
        {
            _workerPool = new ObjectPool<Worker>(_workerTemplates, WorkersCount, _workersParent);

            int maxLevelReputation = 0;

            foreach (var structure in _structures)
            {
                structure.Init();
                maxLevelReputation += structure.MaterialsCount;
            }

            _reputation = new Reputation(_saveObject.Reputation, maxLevelReputation, _saveObject.MoneyMultiplier);
            _wallet = new Wallet(_saveObject.Money, _reputation);

            _levelUIRoot.Init(_reputation, _wallet, _saveObject);

            _player.Init(_wallet, _reputation, _saveObject.PlayerSpeed, _saveObject.PlayerCapacity) ;

            _cameraFollow.Init(_player);

            foreach (var source in _materialSources)
            {
                source.Init();
            }

            _navigator.Init(_materialSources, _trashes);

            for (int i = 0; i < _saveObject.WorkersCount; i++)
            {
                var worker = _workerPool.GetAvailable();
                worker.Init(_structures, _navigator, _saveObject.WorkersSpeed, _saveObject.WorkersCapacity);
                worker.gameObject.SetActive(true);
            }

#if UNITY_EDITOR == false
            Agava.YandexGames.YandexGamesSdk.GameReady();
#endif
        }

        private void OnDataLoaded()
        {
            Init();
            _saveObject.DataLoaded -= OnDataLoaded;
        }
    }
}
