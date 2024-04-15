using System;
using System.Collections.Generic;
using BuilderStory.Audio;
using BuilderStory.Config.Audio;
using BuilderStory.Lifting;
using BuilderStory.Navigation;
using BuilderStory.Saves;
using BuilderStory.States;
using BuilderStory.States.Worker;
using BuilderStory.Struct;
using UnityEngine;
using UnityEngine.AI;

namespace BuilderStory.WorkerSystem
{
    [RequireComponent(typeof(Lift))]
    public class Worker : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private float _interactDistance;

        [SerializeField] private Transform _pickupPoint;

        [SerializeField] private Lift _lift;

        private Navigator _navigator;

        private Structure[] _buildables;
        private IBehaviour _startBehaviour;

        private StateMachine _stateMachine;
        private ProgressSaves _progressSaves;

        public IReadOnlyLift Lift => _lift;

        private void OnEnable()
        {
            if (_progressSaves == null)
            {
                return;
            }

            _progressSaves.WorkersSpeedChanged += ChangeSpeed;
            _progressSaves.WorkersCapacityChanged += _lift.ChangeCapacity;
        }

        private void OnDisable()
        {
            if (_progressSaves == null)
            {
                return;
            }

            _progressSaves.WorkersSpeedChanged -= ChangeSpeed;
            _progressSaves.WorkersCapacityChanged -= _lift.ChangeCapacity;
        }

        private void Update()
        {
            _stateMachine?.Update();
        }

        public void Init(
            Structure[] buildables,
            Navigator navigator,
            ProgressSaves progressSaves,
            AudioManager audioManager,
            AudioMap audioMap)
        {
            _progressSaves = progressSaves;
            _buildables = buildables;
            _navigator = navigator;

            _lift.Init(progressSaves.WorkersCapacity, audioManager,  audioMap);
            _navMeshAgent.speed = progressSaves.WorkersSpeed;

            var behaviours = new Dictionary<Type, IBehaviour>
            {
                {
                    typeof(WaitingBuildState),
                    new WaitingBuildState(_animator, buildables, _lift)
                },
                {
                    typeof(MovingState),
                    new MovingState(_animator, _navMeshAgent, _interactDistance)
                },
                {
                    typeof(PickupState),
                    new PickupState(
                        _animator,
                        _lift,
                        _pickupPoint,
                        _interactDistance,
                        _layerMask)
                },
                {
                    typeof(PlacementState),
                    new PlacementState(_animator, _lift, _interactDistance, _layerMask)
                },
                {
                    typeof(UtilizeState),
                    new UtilizeState(
                        _navigator,
                        _lift,
                        _navMeshAgent,
                        _buildables,
                        _interactDistance,
                        _layerMask)
                },
                {
                    typeof(SearchDestinationState),
                    new SearchDestinationState(_navigator, _buildables, _navMeshAgent, _lift)
                },
            };

            _startBehaviour = behaviours[typeof(WaitingBuildState)];

            _stateMachine = new StateMachine(_startBehaviour, behaviours);

            _progressSaves.WorkersSpeedChanged += ChangeSpeed;
            _progressSaves.WorkersCapacityChanged += _lift.ChangeCapacity;
        }

        private void ChangeSpeed(float speed)
        {
            _navMeshAgent.speed = speed;
        }
    }
}