using System.Collections.ObjectModel;
using UnityEngine;

namespace BuilderStory
{
    public class WaitingBuildState : IBehaviour
    {
        private const string Idle = "Stopped";

        private readonly Worker _worker;
        private readonly IBuildable[] _buildables;

        private Animator _animator;

        public WaitingBuildState(Animator animator, IBuildable[] buildables, Worker worker)
        {
            _worker = worker;
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
            foreach (var buildable in _buildables)
            {
                if ( buildable.IsBuilding == true)
                {
                    return false;
                }
            }

            return true;
            // return _worker.IsBusy != true;
        }

        public void Update()
        {
        }
    }
}
