using DG.Tweening;
using System;
using UnityEngine;

namespace BuilderStory
{
    public class StructureMaterial
    {
        private MeshRenderer _meshRenderer;
        private Sequence _place;

        public event Action<ILiftable> Placed;

        public bool IsPlaced { get; private set; }

        public Transform Point => Material.Point;

        public BuildMaterial Material { get; private set; }

        public StructureMaterial(BuildMaterial material, MeshRenderer meshRenderer)
        {
            IsPlaced = false;
            Material = material;

            _meshRenderer = meshRenderer;
            var mesh = _meshRenderer.gameObject.GetComponent<MeshFilter>().mesh;
            mesh.Optimize();

            _meshRenderer.gameObject.transform.localScale = Vector3.zero;
            _meshRenderer.enabled = false;
        }

        public void Disable()
        {
            _place?.Kill();
        }

        public void Place(float placeDuration)
        {
            IsPlaced = true;
            _meshRenderer.enabled = true;

            Tween scale = _meshRenderer.gameObject.transform.DOScale(Vector3.one, placeDuration)
                .SetEase(Ease.Linear);

            _place = DOTween.Sequence();

            _place.Append(scale);

            _place.Play();

            _place.OnComplete(() =>
            {
                Placed?.Invoke(Material);
            });
        }
    }
}