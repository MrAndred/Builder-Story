using System;
using System.Collections.Generic;
using UnityEngine;

namespace BuilderStory
{
    [RequireComponent(typeof(CapsuleCollider), typeof(PlayerMovement), typeof(Lift))]
    public class Player : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private Transform _pickupPoint;
        [SerializeField] private float _interactDistance;
        [SerializeField] private LayerMask _interactableMask;
        [SerializeField] private PlayerRenderer _playerRenderer;

        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeField] private Lift _lift;

        private Wallet _wallet;
        private Reputation _reputation;
        private StateMachine _stateMachine;

        private Dictionary<Type, IBehaviour> _behaviours;
        private IBehaviour _startBehaviour;

        private bool _isInitialized;

        private void Update()
        {
            if (!_isInitialized)
            {
                return;
            }

            _stateMachine.Update();
        }

        private void FixedUpdate()
        {
            _playerMovement.Handle();
        }

        public void Init(Wallet wallet, Reputation reputation, float speed, int capacity)
        {
            _startBehaviour = new SearchState(_interactableMask, _interactDistance, transform);

            _wallet = wallet;
            _reputation = reputation;

            var behaviours = new Dictionary<Type, IBehaviour>
            {
                {typeof(SearchState), _startBehaviour},
                {typeof(PickupState), new PickupState(_animator, _lift, _pickupPoint, _interactDistance, _interactableMask)},
                {typeof(PlacementState), new PlacementState(_animator , _lift, _interactDistance, _interactableMask)},
                {typeof(PickContractState), new PickContractState(
                    wallet,
                    _reputation,
                    _playerRenderer, 
                    _interactableMask, 
                    transform, 
                    _interactDistance)},
            };

            _behaviours = behaviours;
            _stateMachine = new StateMachine(_startBehaviour, behaviours);

            _lift.Init(capacity);
            _playerMovement.Init(speed);

            _isInitialized = true;
        }
    }
}
