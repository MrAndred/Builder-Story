using System;
using BuilderStory.BuildingMaterial;
using BuilderStory.Pool;
using BuilderStory.Struct;
using BuilderStory.UI;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace BuilderStory.Builder
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

        public event Action ClickedClose;

        public event Action ClickedBuild;

        private void OnEnable()
        {
            _rectTransform.localScale = Vector3.zero;

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

        public void Init()
        {
            _materialsPool = new ObjectPool<MaterialItem>(_materialItem, DefaultPoolSize, _content);
        }

        public void Show(Structure structure)
        {
            gameObject.SetActive(true);

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

            _tweener?.Kill();
            _tweener = _rectTransform.DOScale(Vector2.one, AppearTime).SetEase(Ease.Linear);
        }

        public void Hide()
        {
            _tweener?.Kill();
            _tweener = _rectTransform
                .DOScale(Vector2.zero, AppearTime)
                .SetEase(Ease.Linear)
                .OnComplete(() =>
                    {
                        _scrollRect.gameObject.SetActive(false);
                        gameObject.SetActive(false);
                    });
        }

        private void OnCloseClick()
        {
            ClickedClose?.Invoke();
        }

        private void OnBuildClick()
        {
            ClickedBuild?.Invoke();
        }
    }
}
