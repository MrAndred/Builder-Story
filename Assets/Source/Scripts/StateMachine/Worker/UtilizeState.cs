using UnityEngine;
using UnityEngine.AI;

namespace BuilderStory
{
    public class UtilizeState : IBehaviour
    {
        private readonly Navigator _navigator;
        private readonly NavMeshAgent _navMeshAgent;
        private readonly Lift _lift;
        private readonly LayerMask _layerMask;
        private float _interactDistance;

        public UtilizeState(Navigator nagiator, Lift lift, NavMeshAgent navMeshAgent, float interactDistance, LayerMask layerMask)
        {
            _navigator = nagiator;
            _lift = lift;
            _navMeshAgent = navMeshAgent;
            _interactDistance = interactDistance;
            _layerMask = layerMask;
        }

        public void Enter()
        {
            var trashPoint = _navigator.GetRandomTrashPoint();

            _navMeshAgent.SetDestination(trashPoint.position);
        }

        public void Exit()
        {
        }

        public bool IsReady()
        {
            if (_navMeshAgent.hasPath == true)
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

                if (buildable.IsBuilt() == true)
                {
                    return true;
                }

                if (buildable.CouldPlaceMaterial(_lift.LastLiftable) == false)
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
