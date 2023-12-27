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

        private Dictionary<Type, IBehaviour> _behaviours;
        private IBehaviour _startBehaviour;

        private StateMachine _stateMachine;
        private Transform _buildingPoint;
        private bool _isInitialized;

        public bool IsBusy { get; private set; }

        private void OnEnable()
        {
            _lift.Loaded += OnLoaded;
            _lift.Unloaded += OnUnloaded;
        }

        private void OnDisable()
        {
            _lift.Loaded -= OnLoaded;
            _lift.Unloaded -= OnUnloaded;
        }

        private void Update()
        {
            if (!_isInitialized)
            {
                return;
            }

            _stateMachine.Update();
        }

        public void Init()
        {
            var behaviours = new Dictionary<Type, IBehaviour>
            {
                {typeof(IdleState), new IdleState(_animator, this)},
                {typeof(MovingState), new MovingState(_animator, _navMeshAgent, _interactDistance )},
                {typeof(PickupState), new PickupState(_animator, _lift, _pickupPoint, _interactDistance, _layerMask )},
                {typeof(PlacementState), new PlacementState(_animator, _lift, _interactDistance, _layerMask)}
            };

            _startBehaviour = behaviours[typeof(IdleState)];
            _behaviours = behaviours;

            _stateMachine = new StateMachine(_startBehaviour, behaviours);

            _isInitialized = true;
        }

        public void InstallMaterial(Transform buildingPoint, Transform materialPoint)
        {
            _buildingPoint = buildingPoint;
            _navMeshAgent.SetDestination(materialPoint.position);
            IsBusy = true;
        }

        public void UtilizeMaterial(Transform warehousePoint)
        {
            if (_lift.IsEmpty == true)
            {
                return;
            }

            _navMeshAgent.SetDestination(warehousePoint.position);
            IsBusy = true;
        }

        private void OnLoaded()
        {
            if (_buildingPoint == null)
            {
                return;
            }

            _navMeshAgent.SetDestination(_buildingPoint.position);
        }

        private void OnUnloaded()
        {
            _navMeshAgent.ResetPath();
            IsBusy = false;
        }
    }
}