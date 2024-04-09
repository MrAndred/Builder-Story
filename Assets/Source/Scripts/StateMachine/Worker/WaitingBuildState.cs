using BuilderStory.Lifting;
using BuilderStory.Struct;
using UnityEngine;

namespace BuilderStory.States.Worker
{
    public class WaitingBuildState : IBehaviour
    {
        private const string Idle = "Stopped";

        private readonly Lift _lift;
        private readonly IBuildable[] _buildables;

        private Animator _animator;

        public WaitingBuildState(Animator animator, IBuildable[] buildables, Lift lift)
        {
            _lift = lift;
            _animator = animator;
            _buildables = buildables;
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
            if (_lift.IsEmpty == false)
            {
                return false;
            }

            foreach (var buildable in _buildables)
            {
                if (buildable.IsBuilding == true)
                {
                    return false;
                }
            }

            return true;
        }

        public void Update()
        {
        }
    }
}
