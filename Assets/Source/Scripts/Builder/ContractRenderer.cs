using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace BuilderStory
{
    public class ContractRenderer : MonoBehaviour
    {
        private const float AppearTime = 0.5f;
        private const int DefaultPoolSize = 5;
        private const float DefaultHorizontalNormalizedPos = 0f;
        private const float DefaultScrollDuration = 0.15f;

        [SerializeField] private RectTransform _background;
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private Button[] _closeButtons;
        [SerializeField] private Button _build;
        [SerializeField] private RectTransform _content;
        [SerializeField] private Image _icon;

        [SerializeField] private MaterialItem _materialItem;
        [SerializeField] private MaterialVisualizations _materialVisualizations;
        [SerializeField] private ScrollRect _scrollRect;

        private ObjectPool<MaterialItem> _materialsPool;

        private Tweener _tweener;

        public event Action OnCloseClicked;
        public event Action OnBuildClicked;

        private void Awake()
        {
            _materialsPool = new ObjectPool<MaterialItem>(_materialItem, DefaultPoolSize, _content);
        }

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

        public void Show(Structure structure)
        {
            _materialsPool.Reset();

            _icon.sprite = structure.Icon;

            foreach (var key in structure.MaterialsInfo.Keys)
            {
                var value = structure.MaterialsInfo[key];
                var item = _materialsPool.GetAvailable();

                var materialIcon = _materialVisualizations.GetMaterialSprite(key);

                item.Init(materialIcon, value);
                item.gameObject.SetActive(true);
            }

            _scrollRect.gameObject.SetActive(true);
            _scrollRect.DOHorizontalNormalizedPos(DefaultHorizontalNormalizedPos, DefaultScrollDuration);

            _background.localScale = Vector3.one;
            _rectTransform.localScale = Vector3.zero;
            _tweener = _rectTransform.DOScale(Vector2.one, AppearTime).SetEase(Ease.Linear);
        }

        public void Hide()
        {
            _background.localScale = Vector3.zero;

            _tweener = _rectTransform.DOScale(Vector2.zero, AppearTime).SetEase(Ease.Linear).OnComplete(() =>
            {
                _scrollRect.gameObject.SetActive(false);
            });
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
