using DG.Tweening;
using System;
using UnityEngine;

namespace BuilderStory
{
    public class MaterialWarehouse : MonoBehaviour
    {
        private const float _floatingDuration = 1f;

        [SerializeField] private BuildMaterial _template;
        [SerializeField] private Transform _poolParent;
        [SerializeField] private int _poolSize;
        [SerializeField] private MaterialType _type;
        [SerializeField] private RectTransform _infoHolder;

        private ObjectPool<BuildMaterial> _materialPool;
        private Tweener _floating;

        public BuildMaterial Material => _materialPool.GetAvailable();

        public MaterialType Type => _type;

        private void OnDisable()
        {
            _floating?.Kill();
        }

        private void OnValidate()
        {
            if (_template.Type != _type)
            {
                Debug.LogError($"MaterialWarehouse: Material type {_type} does not match template type {_template.Type}");
                throw new ArgumentException();
            }
        }

        public void Init()
        {
            _materialPool = new ObjectPool<BuildMaterial>(_template, _poolSize, _poolParent);

            AnimateHolder();
        }

        private void AnimateHolder()
        {
            if (_infoHolder == null)
            {
                return;
            }

            Vector2 _offset = new Vector2(0f, 10f);

            Vector2 targetAnchor = _infoHolder.anchoredPosition + _offset;

            _floating = _infoHolder.DOAnchorPos(targetAnchor, _floatingDuration);

            _floating.SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
        }
    }
}