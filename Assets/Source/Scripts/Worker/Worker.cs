using System;
using System.Collections.Generic;
using UnityEngine;

namespace BuilderStory
{
    [RequireComponent(typeof(Movement), typeof(Lift))]
    public class Worker : MonoBehaviour, IWorkable
    {
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private float _interactDistance;

        [SerializeField] private Transform _pickupPoint;

        [SerializeField] private Movement _movement;
        [SerializeField] private Lift _lift;

        private Dictionary<Type, IBehaviour> _behaviours;
        private IBehaviour _startBehaviour;

        private StateMachine _stateMachine;
        private bool _isInitialized;

        public bool IsBusy { get; private set; }

        private void OnEnable()
        {
            _lift.PickedUp += OnLiftPickedUp;
            _lift.Placed += OnLiftPlaced;
        }

        private void OnDisable()
        {
            _lift.PickedUp -= OnLiftPickedUp;
            _lift.Placed -= OnLiftPlaced;
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
                {typeof(IdleState), new IdleState(this)},
                {typeof(MovingState), new MovingState(_movement, _interactDistance, _layerMask)},
                {typeof(PickupState), new PickupState(_lift, _pickupPoint, _interactDistance, _layerMask )},
                {typeof(PlacementState), new PlacementState( _lift, _interactDistance, _layerMask)}
            };

            _startBehaviour = behaviours[typeof(IdleState)];
            _behaviours = behaviours;

            _stateMachine = new StateMachine(_startBehaviour);

            _isInitialized = true;
        }

        public void InstallMaterial(Transform buildPosition, Transform materialPosition)
        {
            _lift.Destination = buildPosition;

            IsBusy = true;

            _movement.SetTargetPosition(materialPosition.position);
            IBehaviour behaviour = _behaviours[typeof(MovingState)];

            if (behaviour.IsReady() == true)
            {
                _stateMachine.ChangeState(behaviour);
                _movement.TargetReached += OnTargetReached;
            }
        }

        private void OnTargetReached()
        {
            _movement.TargetReached -= OnTargetReached;

            IBehaviour[] transitionBehaviours = new IBehaviour[]
            {
                _behaviours[typeof(PickupState)],
                _behaviours[typeof(PlacementState)]
            };

            foreach (IBehaviour behaviour in transitionBehaviours)
            {
                if (behaviour.IsReady() == true)
                {
                    _stateMachine.ChangeState(behaviour);
                    break;
                }
            }
        }

        private void OnLiftPickedUp()
        {
            if (_lift.IsFull == true)
            {
                _movement.SetTargetPosition(_lift.Destination.position);
                IBehaviour behaviour = _behaviours[typeof(MovingState)];

                if (behaviour.IsReady() == true)
                {
                    _stateMachine.ChangeState(behaviour);
                    _movement.TargetReached += OnTargetReached;
                }
            }
        }

        private void OnLiftPlaced()
        {
            if (_lift.IsEmpty == true)
            {
                _stateMachine.ChangeState(_startBehaviour);
                IsBusy = false;
            }
        }
    }
}