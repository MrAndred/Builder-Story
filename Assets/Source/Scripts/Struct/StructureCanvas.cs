using System.Collections.Generic;
using UnityEngine;

namespace BuilderStory
{
    public class StructureCanvas : MonoBehaviour
    {
        [SerializeField] private Transform _materialsTransform;
        [SerializeField] private MaterialVisualizations _materialVisualizations;
        [SerializeField] private MaterialItem _materialItemPrefab;
        [SerializeField] private Structure _structure;

        private Dictionary<MaterialType, MaterialItem> _materials;

        private void OnEnable()
        {
            _structure.Placed += OnMateralPlaced;
        }

        private void OnDisable()
        {
            _structure.Placed -= OnMateralPlaced;
        }

        public void Init()
        {
            _materials = new Dictionary<MaterialType, MaterialItem>();

            foreach (var key in _structure.MaterialsInfo.Keys)
            {
                var item = Instantiate(_materialItemPrefab, _materialsTransform, false);

                var value = _structure.MaterialsInfo[key];
                var materialIcon = _materialVisualizations.GetMaterialSprite(key);
                item.Init(materialIcon, value);

                _materials.Add(key, item);
            }
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        private void OnMateralPlaced()
        {
            foreach (var key in _structure.MaterialsInfo.Keys)
            {
                var value = _structure.MaterialsInfo[key];

                if (value == 0)
                {
                    _materials[key].Hide();
                    continue;
                }

                _materials[key].ChangeCount(value);
            }
        }
    }
}
