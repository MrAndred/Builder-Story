using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BuilderStory
{
    public class Structure : MonoBehaviour, IBuildable
    {
        [SerializeField] private StructureTip _tip;
        [SerializeField] private Sprite _icon;

        [SerializeField] private StructureCanvas _canvas;
        [SerializeField] private ParticleSystem _buildEffect;
        [SerializeField] private ParticleSystem _placeEffect;

        private StructureMaterial[] _structMaterials;
        private Dictionary<MaterialType, int> _materials = new Dictionary<MaterialType, int>();

        private Wallet _wallet;
        private Reputation _reputation;
        private int _moneyPerMaterial = 1;
        private bool _isInitialized = false;

        public event Action Placed;

        public IReadOnlyDictionary<MaterialType, int> MaterialsInfo => _materials;

        public Sprite Icon => _icon;

        public bool IsBuilding { get; private set; } = false;

        public int MaterialsCount => _structMaterials.Length;

        public int PlacedMaterialsCount => GetPlacedMaterialsCount();

        private void OnEnable()
        {
            if (_isInitialized == false)
            {
                return;
            }

            foreach (var material in _structMaterials)
            {
                material.Placed += OnPlaced;
            }
        }

        private void OnDisable()
        {
            foreach (var material in _structMaterials)
            {
                material.Disable();
                material.Placed -= OnPlaced;
            }
        }

        public void Init()
        {
            _tip?.Init();

            var materials = GetComponentsInChildren<BuildMaterial>();

            _structMaterials = new StructureMaterial[materials.Length];

            for (int i = 0; i < materials.Length; i++)
            {
                var meshRenderer = materials[i].GetComponent<MeshRenderer>();
                _structMaterials[i] = new StructureMaterial(materials[i], meshRenderer);
            }

            foreach (var material in _structMaterials)
            {
                var structMaterial = material.Material;

                if (_materials.ContainsKey(structMaterial.Type) == false)
                {
                    _materials.Add(
                        structMaterial.Type,
                        _structMaterials.Count(material => material.Material.Type == structMaterial.Type
                    ));
                }

                material.Placed += OnPlaced;
            }

            if (_canvas != null)
            {
                _canvas.gameObject.SetActive(false);
                _canvas.Init();
            }
        }

        public void StartBuild(Wallet wallet, Reputation reputation)
        {
            _canvas?.gameObject.SetActive(true);
            _tip?.gameObject.SetActive(false);

            _wallet = wallet;
            _reputation = reputation;
            IsBuilding = true;
        }

        public bool TryGetBuildMaterial(out BuildMaterial buildMaterial)
        {
            foreach (var material in _structMaterials)
            {
                if (material.IsPlaced == false)
                {
                    buildMaterial = material.Material;
                    return true;
                }
            }

            buildMaterial = null;
            return false;
        }

        public Transform GetMaterialPoint(ILiftable material)
        {
            foreach (var structMaterial in _structMaterials)
            {
                if (structMaterial.IsPlaced == false && structMaterial.Material.Type == material.Type)
                {
                    return structMaterial.Material.Point;
                }
            }

            return null;
        }

        public bool CouldPlaceMaterial(ILiftable material)
        {
            if (IsBuilding == false)
            {
                return false;
            }

            foreach (var structMaterial in _structMaterials)
            {
                if (structMaterial.IsPlaced == false && structMaterial.Material.Type == material.Type)
                {
                    return true;
                }
            }

            return false;
        }

        public bool TryPlaceMaterial(ILiftable material, float placeDuration, out Transform destination)
        {
            if (IsBuilding == false)
            {
                destination = null;
                return false;
            }

            foreach (var structMaterial in _structMaterials)
            {
                if (structMaterial.IsPlaced == false && structMaterial.Material.Type == material.Type)
                {
                    PlaceMaterial(structMaterial, placeDuration);
                    destination = structMaterial.Material.transform;
                    return true;
                }
            }

            destination = null;
            return false;
        }

        public bool IsBuilt()
        {
            foreach (var material in _structMaterials)
            {
                if (!material.IsPlaced)
                {
                    return false;
                }
            }

            return true;
        }

        private void PlaceMaterial(StructureMaterial structMaterial, float placeDuration)
        {
            if (IsBuilding == false)
            {
                return;
            }

            structMaterial.Place(placeDuration);

            if (IsBuilt() == true)
            {
                FinishBuild();
            }
        }

        private void FinishBuild()
        {
            IsBuilding = false;
            _canvas?.Hide();
        }

        private void OnPlaced(ILiftable material)
        {
            int materialCount = 1;

            _wallet.AddMoney(_moneyPerMaterial);
            _reputation.Add();

            _placeEffect.transform.position = material.Position;
            _placeEffect.Play();

            _materials[material.Type] -= materialCount;
            Placed?.Invoke();

            if (IsBuilt() == true)
            {
                _buildEffect.Play();
            }
        }

        private int GetPlacedMaterialsCount()
        {
            int count = 0;

            foreach (var material in _structMaterials)
            {
                if (material.IsPlaced)
                {
                    count++;
                }
            }

            return count;
        }
    }
}