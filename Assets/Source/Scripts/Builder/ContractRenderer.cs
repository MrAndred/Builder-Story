using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace BuilderStory
{
    public class ContractRenderer : MonoBehaviour
    {
        private const float _appeearTime = 0.5f;

        [SerializeField] private RectTransform _background;
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private Button[] _closeButtons;
        [SerializeField] private Button _build;

        private Tweener _tweener;

        public event Action OnCloseClicked;
        public event Action OnBuildClicked;

        private void OnEnable()
        {
            transform.localScale = Vector3.zero;
            _build.onClick.AddListener(OnBuildClick);

            foreach (var close in _closeButtons)
            {
                close.onClick.AddListener(OnCloseClick);
            }

        }

        private void OnDisable()
        {
            foreach (var close in _closeButtons)
            {
                close.onClick.RemoveListener(OnCloseClick);
            }

            _build.onClick.RemoveListener(OnBuildClick);
            _tweener?.Kill();
        }

        public void Show()
        {
            _background.localScale = Vector3.one;
            _rectTransform.localScale = Vector3.zero;
            _tweener = _rectTransform.DOScale(Vector2.one, _appeearTime).SetEase(Ease.Linear);
        }

        public void Hide()
        {
            _background.localScale = Vector3.zero;
            _tweener = _rectTransform.DOScale(Vector2.zero, _appeearTime).SetEase(Ease.Linear);
        }

        private void OnCloseClick()
        {
            OnCloseClicked?.Invoke();
        }

        private void OnBuildClick()
        {
            OnBuildClicked?.Invoke();
        }
    }
}
