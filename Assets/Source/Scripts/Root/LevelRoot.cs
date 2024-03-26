using UnityEngine;

namespace BuilderStory
{
    public class LevelRoot : MonoBehaviour
    {
        private const string MainThemeOST = "Background";

        [Header("Player")]
        [SerializeField] private Player _player;
        [SerializeField] private CameraFollow _cameraFollow;

        [Header("Roots")]
        [SerializeField] private AudioManager _audioManager;
        [SerializeField] private StructuresRoot _structuresRoot;
        [SerializeField] private WorkersRoot _workersRoot;
        [SerializeField] private MaterialSourcesRoot _materialSourcesRoot;
        [SerializeField] private LevelUIRoot _levelUIRoot;

        [Header("Navigator")]
        [SerializeField] private Trash[] _trashes;
        [SerializeField] private Navigator _navigator;

        [Header("Config")]
        [SerializeField] private LayerMask _buildableMask;
        [SerializeField] private BuildMaterialMap _buildMaterialMap;
        [SerializeField] private AudioMap _audioMap;
        [SerializeField] Material _highlight;

        private Wallet _wallet;
        private Reputation _reputation;
        private ProgressSaves _saveObject;

        private InventoryObserver _inventoryObserver;
        private Structure[] _structures;

        private void OnEnable()
        {
            _inventoryObserver?.SubscribePlayer(_player.Lift);
        }

        private void OnDisable()
        {
            _inventoryObserver?.UnsubscribePlayer(_player.Lift);
        }

        private void Start()
        {
            _saveObject = new ProgressSaves();
            _saveObject.DataLoaded += OnDataLoaded;

            _saveObject.LoadData(this);
        }

        private void Init()
        {
            _audioManager.Init();

            _audioManager.PlayMusic(_audioMap.GetAudioClip(MainThemeOST));

            _wallet = new Wallet(_saveObject);

            int level = BuilderStoryUtil.GetLevelNumber();

            _structuresRoot.Init(_buildMaterialMap, _highlight);
            _structures = _structuresRoot.Structures;

            int materialsCount = _structuresRoot.GetStructureMaterialsCount();

            _reputation = new Reputation(_saveObject, materialsCount);

            _levelUIRoot.Init(_reputation, _wallet, _saveObject);

            _player.Init(_wallet, _reputation, _saveObject);

            _cameraFollow.Init(_player);

            _materialSourcesRoot.Init();

            _navigator.Init(_materialSourcesRoot.MaterialSources, _trashes);
            _inventoryObserver = new InventoryObserver(_structures, _buildableMask);

            _workersRoot.Init(level, _saveObject, _inventoryObserver, _structures, _navigator);

            _inventoryObserver.SubscribePlayer(_player.Lift);

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
