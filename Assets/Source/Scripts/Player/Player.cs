using System;
using System.Collections.Generic;
using UnityEngine;

namespace BuilderStory
{
    [RequireComponent(typeof(CapsuleCollider), typeof(Movement), typeof(Lift))]
    public class Player : MonoBehaviour, IWorkable
    {
        [SerializeField] private Transform _pickupPoint;
        [SerializeField] private float _interactDistance;
        [SerializeField] private LayerMask _interactableMask;

        [SerializeField] private Joystick _joystick;
        [SerializeField] private Movement _movement;
        [SerializeField] private Lift _lift;

        private StateMachine _stateMachine;

        private Dictionary<Type, IBehaviour> _behaviours;
        private IBehaviour _startBehaviour;

        private bool _isInitialized;

        public bool IsBusy { get; private set; }

        private void OnDisable()
        {
            _lift.PickedUp -= OnLiftPickedUp;
            _lift.Placed -= OnLiftPlaced;
        }

        private void Start()
        {
            Init();
            _lift.PickedUp += OnLiftPickedUp;
            _lift.Placed += OnLiftPlaced;
        }

        private void Update()
        {
            if (!_isInitialized)
            {
                return;
            }

            _stateMachine.Update();

            var direction = new Vector3(_joystick.Horizontal, 0, _joystick.Vertical);
            var target = transform.position + direction;

            if (target != transform.position)
            {
                _movement.TargetReached -= OnTargetReached;
                _movement.MoveTo(target);
                _movement.TargetReached += OnTargetReached;
            }
        }

        public void Init()
        {
            _startBehaviour = new IdleState(this);

            var behaviours = new Dictionary<Type, IBehaviour>
            {
                {typeof(IdleState), _startBehaviour},
                {typeof(PickupState), new PickupState(_lift, _pickupPoint, _interactDistance, _interactableMask)},
                {typeof(PlacementState), new PlacementState(_lift, _interactDistance, _interactableMask)},
            };

            _behaviours = behaviours;
            _stateMachine = new StateMachine(_startBehaviour);
            _isInitialized = true;
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
