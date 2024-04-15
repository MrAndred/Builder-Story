using UnityEngine;
using UnityEngine.AI;

namespace BuilderStory.States.Worker
{
    public class MovingState : IBehaviour
    {
        private const float MinSpeed = 0f;

        private static readonly int Speed = Animator.StringToHash("Speed");
        private readonly Animator _animator;
        private readonly float _stopDistance;
        private readonly NavMeshAgent _agent;

        public MovingState(Animator animator, NavMeshAgent agent, float stopDistance)
        {
            _agent = agent;
            _stopDistance = stopDistance;
            _animator = animator;
        }

        public void Update()
        {
            _animator.SetFloat(Speed, _agent.velocity.magnitude / _agent.speed);
        }

        public void Enter()
        {
        }

        public void Exit()
        {
            _agent.ResetPath();
            _animator.SetFloat(Speed, MinSpeed);
            _agent.velocity = Vector3.zero;
        }

        public bool IsReady()
        {
            return _agent.remainingDistance > _stopDistance;
        }
    }
}
