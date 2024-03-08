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

        private ProgressSaves _progressSaves;
        private StateMachine _stateMachine;

        private IBehaviour _startBehaviour;
        private Vector3 _originPosition;

        public IReadOnlyLift Lift => _lift;

        private void OnEnable()
        {
            if (_progressSaves == null)
            {
                return;
            }

            _progressSaves.PlayerSpeedChanged += _playerMovement.ChangeSpeed;
            _progressSaves.PlayerCapacityChanged += _lift.ChangeCapacity;
        }

        private void OnDisable()
        {
            if (_progressSaves == null)
            {
                return;
            }

            _progressSaves.PlayerSpeedChanged -= _playerMovement.ChangeSpeed;
            _progressSaves.PlayerCapacityChanged -= _lift.ChangeCapacity;
        }

        private void Update()
        {
            _stateMachine?.Update();
        }

        private void FixedUpdate()
        {
            _playerMovement?.Handle();
        }

        public void Init(Wallet wallet, Reputation reputation, ProgressSaves progressSaves)
        {
            _originPosition = transform.position;
            _progressSaves = progressSaves;
            _startBehaviour = new SearchState(_interactableMask, _interactDistance, transform);

            var behaviours = new Dictionary<Type, IBehaviour>
            {
                {typeof(SearchState), _startBehaviour},
                {typeof(PickupState), new PickupState(_animator, _lift, _pickupPoint, _interactDistance, _interactableMask)},
                {typeof(PlacementState), new PlacementState(_animator , _lift, _interactDistance, _interactableMask)},
                {typeof(PickContractState), new PickContractState(
                    wallet,
                    reputation,
                    _playerRenderer,
                    _interactableMask,
                    transform,
                    _interactDistance)},
            };

            _stateMachine = new StateMachine(_startBehaviour, behaviours);

            _lift.Init(_progressSaves.PlayerCapacity);
            _playerMovement.Init(_progressSaves.PlayerSpeed);

            _progressSaves.PlayerSpeedChanged += _playerMovement.ChangeSpeed;
            _progressSaves.PlayerCapacityChanged += _lift.ChangeCapacity;
        }

        public void Respawn()
        {
            transform.position = _originPosition;
        }
    }
}
