using System;
using UnityEngine;

namespace BuilderStory
{
    public class MaterialWarehouse : MonoBehaviour
    {
        [SerializeField] private BuildMaterial _template;
        [SerializeField] private Transform _poolParent;
        [SerializeField] private int _poolSize;
        [SerializeField] private MaterialType _type;

        private ObjectPool<BuildMaterial> _materialPool;

        public BuildMaterial Material => _materialPool.GetAvailable();

        public MaterialType Type => _type;

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
        }
    }
}