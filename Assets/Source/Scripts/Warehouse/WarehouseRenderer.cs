using BuilderStory.BuildingMaterial;
using UnityEngine;
using UnityEngine.UI;

namespace BuilderStory.Warehouse
{
    public class WarehouseRenderer : MonoBehaviour
    {
        [SerializeField] private Image _materialIcon;
        [SerializeField] private MaterialVisualizations _visualizations;

        public void Render(MaterialType type)
        {
            Sprite sprite = _visualizations.GetMaterialSprite(type);

            _materialIcon.sprite = sprite;
        }
    }
}
