using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System;

namespace BuilderStory
{
    public class UpgradeTab : MonoBehaviour
    {
        private const float OpenDuration = 0.25f;
        private const float CloseDuration = 0.25f;
        private readonly Color NormalColor = Color.white;


        [SerializeField] private CanvasGroup _upgradeCanvas;
        [SerializeField] private Button _upgradeTabButton;

        private Tweener _openTweener;
        private Tweener _closeTweener;

        public event Action<UpgradeTab> OnTabOpened;

        private void OnDisable()
        {
            _upgradeTabButton.onClick.RemoveListener(OnUpgradeTabButtonClicked);
        }

        public void Init()
        {
            _upgradeTabButton.onClick.AddListener(OnUpgradeTabButtonClicked);
        }

        public void CloseUpgradeTab()
        {
            _closeTweener?.Kill();
            _closeTweener = _upgradeCanvas.transform.DOScale(Vector3.zero, CloseDuration).SetEase(Ease.Linear).OnComplete(() =>
            {
                _upgradeCanvas.gameObject.SetActive(false);
            });

            SetClosed();
        }

        public void OpenUpgradeTab()
        {
            _upgradeCanvas.gameObject.SetActive(true);
            _upgradeCanvas.transform.localScale = Vector3.zero;

            _openTweener?.Kill();
            _openTweener = _upgradeCanvas.transform.DOScale(Vector3.one, OpenDuration).SetEase(Ease.Linear);

            SetOpened();
        }

        public void SetOpened()
        {
            SetButtonColors(_upgradeTabButton.colors.pressedColor);
        }

        public void SetClosed()
        {
            SetButtonColors(NormalColor);
        }

        private void SetButtonColors(Color color)
        {
            ColorBlock colors = _upgradeTabButton.colors;
            colors.normalColor = color;
            _upgradeTabButton.colors = colors;
        }

        private void OnUpgradeTabButtonClicked()
        {
            OnTabOpened?.Invoke(this);
        }
    }
}
