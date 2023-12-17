using UnityEngine;

namespace BuilderStory
{
    public class MovingState : IBehaviour
    {
        private readonly float _stopDistance;
        private readonly Movement _movement;
        private Transform _movable;
        private LayerMask _layerMask;

        public MovingState(Movement movement, float stopDistance, LayerMask layerMask)
        {
            _movement = movement;
            _movable = movement.transform;
            _stopDistance = stopDistance;
            _layerMask = layerMask;
        }

        public void Update()
        {
        }

        public void Enter()
        {
            Vector3 direction = (_movement.TargetPosition - _movable.position).normalized;

            Vector3 predictedPosition = Physics.Linecast(
                _movable.position,
                _movement.TargetPosition + direction * _stopDistance,
                out var hitInfo,
                _layerMask)
            ? hitInfo.point - direction
            : _movement.TargetPosition;
            
            _movement.MoveTo(predictedPosition);
        }

        public void Exit()
        {
            _movement.Stop();
        }

        public bool IsReady()
        {
            if (_movement.TargetPosition == Vector3.zero)
            {
                return false;
            }

            return Vector3.Distance(_movable.position, _movement.TargetPosition) > _stopDistance;
        }
    }
}
