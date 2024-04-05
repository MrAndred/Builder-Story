using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace BuilderStory
{
    public class LevelUIRoot : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField] private ContractInfoRenderer _contractInfoRenderer;
        [SerializeField] private ContractRenderer _contractRenderer;
        [SerializeField] private ReputationRenderer _reputationRenderer;
        [SerializeField] private WalletRenderer _walletRenderer;
        [SerializeField] private UpgradeRenderer _upgradeRenderer;
        [SerializeField] private LeaderbordRenderer _leaderbordRenderer;
        [SerializeField] private NextLevelButton _nextLevelButton;
        [SerializeField] private MusicToggler _musicToggler;
        [SerializeField] private BoostersGroup _boostersGroup;
        [SerializeField] private AdCanvas _adCanvas;

        private ProgressSaves _saveObject;
        private Reputation _reputation;

        private void OnEnable()
        {
            if (_reputation != null)
            {
                _reputation.ReputationChanged += OnReachedMaxReputation;
            }
        }

        private void OnDisable()
        {
            if (_reputation != null)
            {
                _reputation.ReputationChanged -= OnReachedMaxReputation;
            }
        }

        public void Init(Reputation reputation, Wallet wallet, ProgressSaves saveObject)
        {
            _reputation = reputation;
            _saveObject = saveObject;

            _adCanvas.Init();

            _contractRenderer.Init();
            _walletRenderer.Init(wallet);
            _reputationRenderer.Init(reputation, saveObject.Reputation, saveObject.Level);
            _upgradeRenderer.Init(wallet, saveObject);
            _leaderbordRenderer.Init(saveObject);
            _musicToggler.Init();
            _nextLevelButton.Init(saveObject, _adCanvas);
            _boostersGroup.Init(saveObject, wallet);

            _reputation.ReachedMaxReputation += OnReachedMaxReputation;
        }

        private void OnReachedMaxReputation()
        {
            _nextLevelButton.Show();
        }

        private void OnResetSaves()
        {
            _saveObject.ResetData();
        }
    }
}
