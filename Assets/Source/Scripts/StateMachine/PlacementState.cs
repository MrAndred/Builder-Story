using System.Collections;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace BuilderStory
{
    public class PlacementState : IBehaviour
    {
        private const string Placement = "Pickup";

        private readonly Animator _animator;
        private readonly  float _placementDistance;
        private readonly Lift _lift;
        private readonly LayerMask _layerMask;

        private IBuildable _buildable;

        private Coroutine _placing;

        public PlacementState(Animator animator, Lift lift, float placementDistance, LayerMask layerMask)
        {
            _animator = animator;
            _lift = lift;
            _placementDistance = placementDistance;
            _layerMask = layerMask;
        }

        public void Enter()
        {
            _placing = _lift.StartCoroutine(Place());
        }

        public void Exit()
        {
            if (_placing != null)
            {
                _lift.StopCoroutine(_placing);
            }
        }

        public bool IsReady()
        {
            if (_lift.IsEmpty)
            {
                return false;
            }

            var colliders = Physics.OverlapSphere(_lift.transform.position, _placementDistance, _layerMask);

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

                _buildable = buildable;

                return true;
            }

            return false;
        }

        public void Update()
        {
        }

        private IEnumerator Place()
        {
            var delay = new WaitForSeconds(_lift.Duration);

            while (_lift.IsEmpty == false)
            {
                if (_buildable.TryPlaceMaterial(_lift.FirstLiftable, out Transform destination) == false)
                {
                    break;
                }

                _animator.SetTrigger(Placement);
                _lift.Place(_lift.FirstLiftable, destination);

                yield return delay;
            }
        }
    }
}
