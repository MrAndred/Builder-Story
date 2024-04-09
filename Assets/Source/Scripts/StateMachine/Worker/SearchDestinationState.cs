using BuilderStory.BuildingMaterial;
using BuilderStory.Lifting;
using BuilderStory.Navigation;
using BuilderStory.Struct;
using UnityEngine;
using UnityEngine.AI;

namespace BuilderStory.States.Worker
{
    public class SearchDestinationState : IBehaviour
    {
        private const float MaxDistance = 50f;

        private Navigator _navigator;
        private IBuildable[] _structures;
        private NavMeshAgent _agent;
        private Lift _lift;

        public SearchDestinationState(
            Navigator navigator,
            IBuildable[] structures,
            NavMeshAgent agent,
            Lift lift)
        {
            _navigator = navigator;
            _structures = structures;
            _agent = agent;
            _lift = lift;
        }

        private bool HasBuilding => HasBuildingStructure();

        public void Enter()
        {
            var structure = GetBuilding();
            var destinationPoint = _navigator.GetRandomTrashPoint().position;

            if (_lift.IsFull && structure.CouldPlaceMaterial(_lift.LastLiftable) == true)
            {
                destinationPoint = structure.GetMaterialPoint(_lift.LastLiftable).position;
            }
            else if (_lift.IsEmpty)
            {
                if (structure.TryGetBuildMaterial(out BuildMaterial buildMaterial) &&
                    _navigator.TryGetMaterialPosition(buildMaterial, out var materialPosition))
                {
                    destinationPoint = materialPosition.position;
                }
            }

            NavMeshHit closestHit;

            if (NavMesh.SamplePosition(destinationPoint, out closestHit, MaxDistance, NavMesh.AllAreas))
            {
                Vector3 finalPosition = closestHit.position;
                _agent.SetDestination(finalPosition);
            }
        }

        public void Exit()
        {
        }

        public bool IsReady()
        {
            if (_agent.hasPath == true)
            {
                return false;
            }

            if (HasBuilding == false)
            {
                return false;
            }

            if (_lift.IsLifting == true)
            {
                return false;
            }

            if (_lift.IsFull == true || _lift.IsEmpty == true)
            {
                return true;
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

        private IBuildable GetBuilding()
        {
            foreach (var structure in _structures)
            {
                if (structure.IsBuilding == true)
                {
                    return structure;
                }
            }

            return null;
        }
    }
}
