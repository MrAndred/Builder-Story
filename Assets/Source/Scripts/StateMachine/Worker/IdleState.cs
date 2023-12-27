using UnityEngine;

namespace BuilderStory
{
    public class IdleState : IBehaviour
    {
        private const string Idle = "Stopped";

        private readonly IWorkable _worker;

        private Animator _animator;

        public IdleState(Animator animator, IWorkable worker)
        {
            _worker = worker;
            _animator = animator;
        }

        public void Enter()
        {
            _animator.SetTrigger(Idle);
        }

        public void Exit()
        {
        }

        public bool IsReady()
        {
            return _worker.IsBusy != true;
        }

        public void Update()
        {
        }
    }
}
