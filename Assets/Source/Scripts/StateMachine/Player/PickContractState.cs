using BuilderStory.Builder;
using BuilderStory.PlayerSystem;
using BuilderStory.ReputationSystem;
using BuilderStory.Struct;
using BuilderStory.WalletSystem;
using UnityEngine;

namespace BuilderStory.States.Player
{
    public class PickContractState : IBehaviour
    {
        private Wallet _wallet;
        private Reputation _reputation;
        private Transform _raycastPoint;
        private PlayerRenderer _renderer;
        private LayerMask _interactLayer;
        private float _interactDistance;

        private Structure _structure;

        public PickContractState(
            Wallet wallet,
            Reputation reputation,
            PlayerRenderer renderer,
            LayerMask interactLayer,
            Transform raycastPoint,
            float interactDistance)
        {
            _wallet = wallet;
            _reputation = reputation;
            _renderer = renderer;
            _interactLayer = interactLayer;
            _interactDistance = interactDistance;
            _raycastPoint = raycastPoint;
        }

        public void Enter()
        {
            _renderer.Show(_structure);
            _renderer.ContractAccepted += ContractAccepted;
        }

        public void Exit()
        {
            _renderer.ContractAccepted -= ContractAccepted;

            var colliders = Physics.OverlapSphere(
                _raycastPoint.position,
                _interactDistance,
                _interactLayer);

            if (colliders.Length == 0)
            {
                _renderer.HideAll();
                return;
            }

            _renderer.HideContract();
        }

        public bool IsReady()
        {
            var colliders = Physics.OverlapSphere(
                _raycastPoint.position,
                _interactDistance,
                _interactLayer);

            if (colliders.Length == 0)
            {
                return false;
            }

            foreach (var collider in colliders)
            {
                if (collider.TryGetComponent(out Structure buildable) == false)
                {
                    continue;
                }

                if (buildable.IsBuilt() == true)
                {
                    return false;
                }

                if (buildable.IsBuilding == false)
                {
                    _structure = buildable;
                    return true;
                }
            }

            return false;
        }

        public void Update()
        {
        }

        private void ContractAccepted()
        {
            BuildContract buildContract = new BuildContract(_structure, _wallet, _reputation);

            buildContract.Build();
        }
    }
}
