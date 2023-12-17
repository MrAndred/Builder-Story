using UnityEngine;

namespace BuilderStory
{
    public class PlacementState : IBehaviour
    {
        private float _placementDistance;
        private Lift _lift;
        private LayerMask _layerMask;
        private Transform _destionation;

        public PlacementState(Lift lift, float placementDistance, LayerMask layerMask)
        {
            _lift = lift;
            _placementDistance = placementDistance;
            _layerMask = layerMask;
        }

        public void Enter()
        {
            _lift.Place(_lift.Liftables[0], _destionation);
        }

        public void Exit()
        {
        }

        public bool IsReady()
        {
            if (_lift.IsEmpty)
            {
                return false;
            }

            var hit = Physics.Raycast(
                _lift.transform.position,
                _lift.transform.forward,
                out var hitInfo,
                _placementDistance,
                _layerMask);

            if (!hit)
            {
                return false;
            }

            if (hitInfo.collider.TryGetComponent(out IBuildable buildable) == false)
            {
                Debug.Log("Placement state can be used only with buildable objects");
                return false;
            }

            if (buildable.TryPlaceMaterial(_lift.Liftables[0], out Transform destination) == false)
            {
                return false;
            }

            //if (buildable.TryPla(out BuildMaterial processedBuildMaterial) == false)
            //{
            //    return false;
            //};

            _destionation = destination;
            return true;
        }

        public void Update()
        {
        }
    }
}
