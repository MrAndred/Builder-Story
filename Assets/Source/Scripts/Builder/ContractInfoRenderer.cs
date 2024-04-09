using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace BuilderStory.Builder
{
    public class ContractInfoRenderer : MonoBehaviour
    {
        private const float AppearDuration = 0.8f;

        [SerializeField] private Button _infoContract;
        [SerializeField] private RectTransform _infoContractTransform;

        [SerializeField] private Vector2 _showContractLocalPosition = new Vector2(0f, 150f);
        [SerializeField] private Vector2 _hideContractLocalPosition = new Vector2(0f, -350f);

        private Tweener _tweener;

        public event Action InfoClicked;

        private void OnEnable()
        {
            _infoContract.onClick.AddListener(OnClicked);
        }

        private void OnDisable()
        {
            _tweener?.Kill();
            _infoContract.onClick.RemoveListener(OnClicked);
        }

        public void Show()
        {
            _infoContract.interactable = true;
            _tweener = _infoContractTransform
                .DOAnchorPos(_showContractLocalPosition, AppearDuration)
                .SetEase(Ease.OutBounce);
        }

        public void Hide()
        {
            _infoContract.interactable = false;
            _tweener = _infoContractTransform
                .DOAnchorPos(_hideContractLocalPosition, AppearDuration)
                .SetEase(Ease.InCubic);
        }

        private void OnClicked()
        {
            InfoClicked?.Invoke();
        }
    }
}
