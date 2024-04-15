using BuilderStory.Saves;
using BuilderStory.UI.Canvases;
using BuilderStory.WalletSystem;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace BuilderStory.Upgrades
{
    public class UpgradeRenderer : MonoBehaviour
    {
        private const float OpenDuration = 0.35f;
        private const float CloseDuration = 0.35f;

        [SerializeField] private CanvasGroup _upgradeCanvas;
        [SerializeField] private UpgradeTab[] _upgradeTabs;

        [SerializeField] private Button _openCanvas;
        [SerializeField] private Button[] _closeButtons;

        [SerializeField] private PlayerUpgradesCanvas _playerUpgrardesCanvas;
        [SerializeField] private WorkerUpgradesCanvas _workerUpgradesCanvas;

        private UpgradeTab _currentTab;

        private Tweener _openTweener;
        private Tweener _closeTweener;

        private void OnEnable()
        {
            foreach (var tab in _upgradeTabs)
            {
                tab.Init();
                tab.Opened += OpenUpgradeTab;
            }

            foreach (var button in _closeButtons)
            {
                button.onClick.AddListener(OnCloseCanvasButtonClicked);
            }
        }

        private void OnDisable()
        {
            foreach (var button in _closeButtons)
            {
                button.onClick.RemoveListener(OnCloseCanvasButtonClicked);
            }

            foreach (var tab in _upgradeTabs)
            {
                tab.Opened -= OpenUpgradeTab;
            }
        }

        private void OnDestroy()
        {
            _openCanvas.onClick.RemoveListener(OnOpenButtonClicked);
        }

        public void Init(Wallet wallet, ProgressSaves saves)
        {
            _openCanvas.onClick.AddListener(OnOpenButtonClicked);

            _currentTab = _upgradeTabs[0];
            _currentTab.OpenUpgradeTab();

            _playerUpgrardesCanvas.Init(wallet, saves);
            _workerUpgradesCanvas.Init(wallet, saves);

            _upgradeCanvas.transform.localScale = Vector3.zero;
        }

        private void OnCloseCanvasButtonClicked()
        {
            _closeTweener?.Kill();
            _closeTweener = _upgradeCanvas.transform.DOScale(Vector3.zero, CloseDuration).SetEase(Ease.InBack).OnComplete(() =>
            {
                _upgradeCanvas.gameObject.SetActive(false);
            });

            _openCanvas.gameObject.SetActive(true);
        }

        private void OnOpenButtonClicked()
        {
            _upgradeCanvas.gameObject.SetActive(true);

            _openTweener?.Kill();
            _openTweener = _upgradeCanvas.transform.DOScale(Vector3.one, OpenDuration).SetEase(Ease.OutBack);

            _openCanvas.gameObject.SetActive(false);
        }

        private void OpenUpgradeTab(UpgradeTab tab)
        {
            if (_currentTab == tab)
            {
                return;
            }

            if (_currentTab != null)
            {
                _currentTab.CloseUpgradeTab();
            }

            _currentTab = tab;
            _currentTab.OpenUpgradeTab();
        }
    }
}
