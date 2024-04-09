using UnityEngine;

namespace BuilderStory.BuildingMaterial
{
    [CreateAssetMenu(fileName = "MaterialVisualizations", menuName = "BuilderStory/MaterialVisualizations")]
    public class MaterialVisualizations : ScriptableObject
    {
        [SerializeField] private MaterialVisualization[] _visualizations;

        public Sprite GetMaterialSprite(MaterialType type)
        {
            foreach (var visualization in _visualizations)
            {
                if (visualization.Type == type)
                {
                    return visualization.Image;
                }
            }

            return null;
        }
    }
}
