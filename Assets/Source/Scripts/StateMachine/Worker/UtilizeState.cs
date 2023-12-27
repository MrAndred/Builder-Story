using UnityEngine;
using UnityEngine.AI;

namespace BuilderStory
{
    public class UtilizeState : IBehaviour
    {
        private readonly Worker _worker;
        private readonly NavMeshAgent _navMeshAgent;
        private readonly Lift _lift;
        private readonly LayerMask _layerMask;
        private float _interactDistance;

        public UtilizeState(Worker worker, Lift lift, NavMeshAgent navMeshAgent, float interactDistance, LayerMask layerMask)
        {
            _worker = worker;
            _lift = lift;
            _navMeshAgent = navMeshAgent;
            _interactDistance = interactDistance;
            _layerMask = layerMask;
        }

        public void Enter()
        {
            _navMeshAgent.SetDestination(_worker.TrashPosition);
        }

        public void Exit()
        {
        }

        public bool IsReady()
        {
            if (_navMeshAgent.remainingDistance > _interactDistance)
            {
                return false;
            }

            if (_lift.IsEmpty)
            {
                return false;
            }

            var colliders = Physics.OverlapSphere(_lift.transform.position, _interactDistance, _layerMask);

            if (colliders.Length == 0)
            {
                return false;
            }

            foreach (var collider in colliders)
            {
                if (collider.TryGetComponent(out IBuildable buildable) == false)
                {
                    continue;
                }

                if (buildable.CouldPlaceMaterial(_lift.FirstLiftable) == false)
                {
                    return true;
                }
            }

            return false;
        }

        public void Update()
        {
        }
    }
}
