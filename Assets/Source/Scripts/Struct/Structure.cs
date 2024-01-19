using UnityEngine;

namespace BuilderStory
{
    public class Structure : MonoBehaviour, IBuildable
    {
        [SerializeField] private StructureMaterial[] _structMaterials;
        [SerializeField] private float _placeDuration = 3f;

        private Wallet _wallet;
        private Reputation _reputation;
        private int _moneyPerMaterial = 1;
        private bool _isInitialized = false;

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
            var materials = GetComponentsInChildren<BuildMaterial>();

            _structMaterials = new StructureMaterial[materials.Length];

            for (int i = 0; i < materials.Length; i++)
            {
                var meshRenderer = materials[i].GetComponent<MeshRenderer>();
                _structMaterials[i] = new StructureMaterial(materials[i], meshRenderer, _placeDuration);
            }

            foreach (var material in _structMaterials)
            {
                material.Placed += OnPlaced;
            }
        }

        public void StartBuild(Wallet wallet, Reputation reputation)
        {
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

        public bool TryPlaceMaterial(ILiftable material, out Transform destination)
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

        private void PlaceMaterial(StructureMaterial structMaterial)
        {
            if (IsBuilding == false)
            {
                return;
            }

            structMaterial.Place();

            if (IsBuilt() == true)
            {
                FinishBuild();
            }
        }

        private void FinishBuild()
        {
            IsBuilding = false;
        }

        private void OnPlaced()
        {
            _wallet.AddMoney(_moneyPerMaterial);
            _reputation.Add();
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