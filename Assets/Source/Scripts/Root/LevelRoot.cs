using UnityEngine;

namespace BuilderStory
{
    public class LevelRoot : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] private CameraFollow _cameraFollow;

        [SerializeField] private MaterialWarehouse[] _materialSources;
        [SerializeField] private Structure[] _structures;
        [SerializeField] private Trash[] _trashes;

        [SerializeField] private Worker[] _workers;
        [SerializeField] private Navigator _navigator;

        [SerializeField] private ReputationRenderer _reputationRenderer;
        [SerializeField] private WalletRenderer _walletRenderer;

        private Wallet _wallet;
        private Reputation _reputation;

        private void Start()
        {
            Init();
        }

        private void Init()
        {
            int maxLevelReputation = 0;

            foreach (var structure in _structures)
            {
                structure.Init();
                maxLevelReputation += structure.MaterialsCount;
            }

            int reputationValue = 0;
            int moneyMultiplier = 1;

            _reputation = new Reputation(reputationValue, maxLevelReputation, moneyMultiplier);

            int money = 0;
            _wallet = new Wallet(money, _reputation);

            _walletRenderer.Init(_wallet);
            _reputationRenderer.Init(_reputation);

            _player.Init(_wallet, _reputation);

            _cameraFollow.Init(_player);

            foreach (var source in _materialSources)
            {
                source.Init();
            }

            _navigator.Init(_materialSources, _trashes);

            foreach (var worker in _workers)
            {
                worker.Init(_structures, _navigator);
            }

#if UNITY_EDITOR == false
            Agava.YandexGames.YandexGamesSdk.GameReady();
#endif
        }

        private void OnDataLoaded(string data)
        {
            Debug.Log(data);
        }

        private void OnLoadError(string error)
        {
            Debug.LogError(error);
        }
    }
}
