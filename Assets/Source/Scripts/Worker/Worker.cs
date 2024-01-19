using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace BuilderStory
{
    [RequireComponent(typeof(Lift))]
    public class Worker : MonoBehaviour, IWorkable
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private float _interactDistance;

        [SerializeField] private Transform _pickupPoint;

        [SerializeField] private Lift _lift;

        private Navigator _navigator;

        private IBuildable[] _buildables;
        private IBehaviour _startBehaviour;

        private StateMachine _stateMachine;
        private bool _isInitialized;

        public bool IsBusy { get; private set; }

        private void Update()
        {
            if (!_isInitialized)
            {
                return;
            }

            _stateMachine.Update();
        }

        public void Init(IBuildable[] buildables, Navigator navigator)
        {
            _buildables = buildables;
            _navigator = navigator;

            var behaviours = new Dictionary<Type, IBehaviour>
            {
                {typeof(WaitingBuildState), new WaitingBuildState(_animator, buildables, this)},
                {typeof(MovingState), new MovingState(_animator, _navMeshAgent, _interactDistance )},
                {typeof(PickupState), new PickupState(_animator, _lift, _pickupPoint, _interactDistance, _layerMask )},
                {typeof(PlacementState), new PlacementState(_animator, _lift, _interactDistance, _layerMask)},
                {typeof(UtilizeState), new UtilizeState(_navigator, _lift, _navMeshAgent, _interactDistance, _layerMask)},
                {typeof(SearchDestinationState), new SearchDestinationState(_navigator, _buildables, _navMeshAgent, _lift) },
            };

            _startBehaviour = behaviours[typeof(WaitingBuildState)];

            _stateMachine = new StateMachine(_startBehaviour, behaviours);

            _isInitialized = true;
        }
    }
}