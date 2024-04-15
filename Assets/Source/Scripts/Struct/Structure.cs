using System;
using System.Collections.Generic;
using System.Linq;
using BuilderStory.Audio;
using BuilderStory.BuildingMaterial;
using BuilderStory.Config.Audio;
using BuilderStory.Config.BuildMaterial;
using BuilderStory.Lifting;
using BuilderStory.ReputationSystem;
using BuilderStory.WalletSystem;
using UnityEngine;

namespace BuilderStory.Struct
{
    public class Structure : MonoBehaviour, IBuildable
    {
        private const string Fireworks = "Fireworks";
        private const int MoneyPerMaterial = 1;

        [Header("Structure UI")]
        [SerializeField] private StructureCanvas _canvas;
        [SerializeField] private StructureTip _tip;
        [SerializeField] private Sprite _icon;

        [Header("Effects")]
        [SerializeField] private SpriteRenderer _areaTip;
        [SerializeField] private ParticleSystem _buildEffect;
        [SerializeField] private ParticleSystem _placeEffect;
        [SerializeField] private Transform _buildedStructure;

        private StructureMaterial[] _structMaterials;
        private Dictionary<MaterialType, int> _materials = new Dictionary<MaterialType, int>();

        private Wallet _wallet;
        private Reputation _reputation;
        private bool _isInitialized = false;
        private AudioManager _audioManager;
        private AudioMap _audioMap;

        public event Action Placed;

        public IReadOnlyDictionary<MaterialType, int> MaterialsInfo => _materials;

        public Sprite Icon => _icon;

        public bool IsBuilding { get; private set; } = false;

        public int MaterialsCount => _structMaterials.Length;

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
                material.Placed -= OnPlaced;
            }
        }

        public void Init(
            BuildMaterialMap buildMaterialMap,
            Material highlight,
            AudioManager audioManager,
            AudioMap audioMap)
        {
            _audioManager = audioManager;
            _audioMap = audioMap;
            _tip?.Init();

            var materials = GetComponentsInChildren<BuildMaterial>();

            _structMaterials = new StructureMaterial[materials.Length];

            for (int i = 0; i < materials.Length; i++)
            {
                var meshRenderer = materials[i].GetComponent<MeshRenderer>();
                var buildMaterial = materials[i];

                _structMaterials[i] = new StructureMaterial(
                    buildMaterial,
                    meshRenderer,
                    buildMaterialMap.GetMaterial(buildMaterial.Type),
                    highlight);
            }

            foreach (var material in _structMaterials)
            {
                var structMaterial = material.Material;

                if (_materials.ContainsKey(structMaterial.Type) == false)
                {
                    _materials.Add(
                        structMaterial.Type,
                        _structMaterials
                            .Count(material => material.Material.Type == structMaterial.Type));
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
                    PlaceMaterial(structMaterial);
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

        public void RemoveHighlight()
        {
            foreach (var material in _structMaterials)
            {
                if (material.Highlighted)
                {
                    material.RemoveHighlight();
                }
            }
        }

        public void Highlight(ILiftable material)
        {
            if (IsBuilding == false || IsBuilt())
            {
                return;
            }

            foreach (var structMaterial in _structMaterials)
            {
                if (
                    structMaterial.IsPlaced == false &&
                    structMaterial.Material.Type == material.Type &&
                    structMaterial.Highlighted == false)
                {
                    structMaterial.Highlight();
                    return;
                }
            }
        }

        public void Unhighlight(ILiftable material)
        {
            if (IsBuilding == false || IsBuilt())
            {
                return;
            }

            for (int i = _structMaterials.Length - 1; i >= 0; i--)
            {
                var structMaterial = _structMaterials[i];

                if (structMaterial.Highlighted && structMaterial.Material.Type == material.Type)
                {
                    structMaterial.RemoveHighlight();
                    return;
                }
            }
        }

        private void PlaceMaterial(StructureMaterial structMaterial)
        {
            if (IsBuilding == false)
            {
                return;
            }

            structMaterial.Place();

            if (IsBuilt())
            {
                FinishBuild();
            }
        }

        private void FinishBuild()
        {
            IsBuilding = false;
            _areaTip.gameObject.SetActive(false);
            _canvas?.Hide();
            _buildedStructure.gameObject.SetActive(true);
            _audioManager.PlaySFX(_audioMap.GetAudioClip(Fireworks));
            gameObject.SetActive(false);
        }

        private void OnPlaced(ILiftable material)
        {
            int materialCount = 1;

            _wallet.AddMoney(MoneyPerMaterial);
            _reputation.Add();

            _placeEffect.transform.position = material.Position;
            _placeEffect.Play();

            _materials[material.Type] -= materialCount;
            Placed?.Invoke();

            if (IsBuilt())
            {
                _buildEffect.Play();
            }
        }
    }
}