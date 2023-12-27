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

        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeField] private Lift _lift;

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

            _playerMovement.Handle();
            _stateMachine.Update();
        }

        public void Init()
        {
            Application.targetFrameRate = 30;
            _startBehaviour = new SearchState(_interactableMask, _interactDistance, transform);

            var behaviours = new Dictionary<Type, IBehaviour>
            {
                {typeof(SearchState), _startBehaviour},
                {typeof(PickupState), new PickupState(_animator, _lift, _pickupPoint, _interactDistance, _interactableMask)},
                {typeof(PlacementState), new PlacementState(_animator , _lift, _interactDistance, _interactableMask)},
            };

            _behaviours = behaviours;
            _stateMachine = new StateMachine(_startBehaviour, behaviours);
            _playerMovement.Init();
            _isInitialized = true;
        }
    }
}
