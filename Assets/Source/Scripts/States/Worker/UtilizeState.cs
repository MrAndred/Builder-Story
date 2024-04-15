using BuilderStory.Lifting;
using BuilderStory.Navigation;
using BuilderStory.Struct;
using UnityEngine;
using UnityEngine.AI;

namespace BuilderStory.States.Worker
{
    public class UtilizeState : IBehaviour
    {
        private const int OverlapDistanceMultiplier = 2;

        private readonly Navigator _navigator;
        private readonly NavMeshAgent _navMeshAgent;
        private readonly Lift _lift;
        private readonly LayerMask _layerMask;

        private float _interactDistance;
        private IBuildable[] _structures;

        public UtilizeState(
            Navigator nagiator,
            Lift lift,
            NavMeshAgent navMeshAgent,
            IBuildable[] structures,
            float interactDistance,
            LayerMask layerMask)
        {
            _structures = structures;
            _navigator = nagiator;
            _lift = lift;
            _navMeshAgent = navMeshAgent;
            _interactDistance = interactDistance;
            _layerMask = layerMask;
        }

        public void Enter()
        {
            var randomDirection = Random.insideUnitSphere * _interactDistance;
            var trashPoint = _navigator.GetRandomTrashPoint().position + randomDirection;

            _navMeshAgent.SetDestination(trashPoint);
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

            if (_lift.IsEmpty == false && HasBuildingStructure() == false)
            {
                return true;
            }

            float distance = _interactDistance * OverlapDistanceMultiplier;
            var colliders = Physics.OverlapSphere(_lift.transform.position, distance, _layerMask);

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

                if (buildable.IsBuilt())
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

        private bool HasBuildingStructure()
        {
            if (_structures.Length == 0)
            {
                return false;
            }

            foreach (var structure in _structures)
            {
                if (structure.IsBuilding)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
