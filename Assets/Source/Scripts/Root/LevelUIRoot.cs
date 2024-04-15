using BuilderStory.Advertisement;
using BuilderStory.Audio;
using BuilderStory.Builder;
using BuilderStory.Leaderbord;
using BuilderStory.Pause;
using BuilderStory.ReputationSystem;
using BuilderStory.Saves;
using BuilderStory.UI;
using BuilderStory.Upgrades;
using BuilderStory.WalletSystem;
using UnityEngine;

namespace BuilderStory.Root
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

        private Reputation _reputation;

        private void OnEnable()
        {
            if (_reputation != null)
            {
                _reputation.Changed += OnReachedMaxReputation;
            }
        }

        private void OnDisable()
        {
            if (_reputation != null)
            {
                _reputation.Changed -= OnReachedMaxReputation;
            }
        }

        public void Init(
            Reputation reputation,
            Wallet wallet,
            ProgressSaves saveObject,
            PauseSystem pauseSystem,
            AudioManager audioSystem)
        {
            _reputation = reputation;

            _adCanvas.Init(pauseSystem);

            _contractRenderer.Init();
            _walletRenderer.Init(wallet);
            _reputationRenderer.Init(reputation, saveObject.Reputation, saveObject.Level);
            _upgradeRenderer.Init(wallet, saveObject);
            _leaderbordRenderer.Init();
            _musicToggler.Init(audioSystem);
            _nextLevelButton.Init(saveObject, pauseSystem);
            _boostersGroup.Init(saveObject, wallet, pauseSystem);

            _reputation.ReachedMax += OnReachedMaxReputation;
        }

        private void OnReachedMaxReputation()
        {
            _nextLevelButton.Show();
        }
    }
}
