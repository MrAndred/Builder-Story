using DG.Tweening;
using UnityEngine;

namespace BuilderStory
{
    public class StructureMaterial
    {
        private MeshRenderer _meshRenderer;
        private float _placeDuration;
        private Sequence _place;

        public bool IsPlaced { get; private set; }

        public BuildMaterial Material { get; private set; }

        public StructureMaterial(BuildMaterial material, MeshRenderer meshRenderer, float placeDuration)
        {
            IsPlaced = false;
            Material = material;
            _placeDuration = placeDuration;
            _meshRenderer = meshRenderer;
            _meshRenderer.gameObject.transform.localScale = Vector3.zero;
            _meshRenderer.enabled = false;
        }

        public void Disable()
        {
            _place?.Kill();
        }

        public void Place()
        {
            IsPlaced = true;
            _meshRenderer.enabled = true;

            Tween scale = _meshRenderer.gameObject.transform.DOScale(Vector3.one, _placeDuration)
                .SetEase(Ease.Linear);

            _place = DOTween.Sequence();

            _place.Append(scale);

            _place.Play();
        }
    }
}