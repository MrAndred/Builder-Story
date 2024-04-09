using System;
using BuilderStory.BuildingMaterial;
using BuilderStory.Lifting;
using UnityEngine;

namespace BuilderStory.Struct
{
    public class StructureMaterial
    {
        private MeshRenderer _meshRenderer;
        private Material _default;
        private Material _highlighted;
        private bool _isHighlighted;

        public StructureMaterial(
            BuildMaterial material,
            MeshRenderer meshRenderer,
            Material defaultMaterial,
            Material highlighted
            )
        {
            IsPlaced = false;
            Material = material;

            _meshRenderer = meshRenderer;
            var mesh = _meshRenderer.gameObject.GetComponent<MeshFilter>().mesh;
            mesh.Optimize();

            _meshRenderer.material = highlighted;
            _meshRenderer.enabled = false;
            _default = defaultMaterial;
            _highlighted = highlighted;
            _isHighlighted = false;
        }

        public event Action<ILiftable> Placed;

        public bool IsPlaced { get; private set; }

        public BuildMaterial Material { get; private set; }

        public bool Highlighted => _isHighlighted;

        public void Place()
        {
            IsPlaced = true;

            _isHighlighted = false;

            _meshRenderer.enabled = true;
            _meshRenderer.material = _default;

            Placed?.Invoke(Material);
        }

        public void Highlight()
        {
            _meshRenderer.material = _highlighted;
            _meshRenderer.enabled = true;
            _isHighlighted = true;
        }

        public void RemoveHighlight()
        {
            _meshRenderer.enabled = false;
            _isHighlighted = false;
        }
    }
}