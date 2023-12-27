using System.Collections;
using UnityEngine;

namespace BuilderStory
{
    public class PickupState : IBehaviour
    {
        private const string Pickup = "Pickup";

        private Animator _animator;
        private Transform _pickupPoint;
        private Lift _lift;
        private float _pickupDistance;
        private LayerMask _warehouseLayer;

        private MaterialWarehouse _warehouse;
        private Coroutine _pickupCoroutine;

        public PickupState(
            Animator animator,
            Lift lift,
            Transform pickupPoint,
            float pickupLength,
            LayerMask warehouseLayer)
        {
            _animator = animator;
            _lift = lift;
            _pickupPoint = pickupPoint;
            _pickupDistance = pickupLength;
            _warehouseLayer = warehouseLayer;
        }

        public void Enter()
        {
            _pickupCoroutine = _lift.StartCoroutine(StartPickup());
        }

        public void Exit()
        {
            if (_pickupCoroutine != null)
            {
                _lift.StopCoroutine(_pickupCoroutine);
            }
        }

        public bool IsReady()
        {
            if (_lift.IsFull)
            {
                return false;
            }

            var colliders = Physics.OverlapSphere(_lift.transform.position, _pickupDistance, _warehouseLayer);

            if (colliders.Length == 0)
            {
                return false;
            }

            foreach (var collider in colliders)
            {
                if (collider.TryGetComponent(out MaterialWarehouse warehouse) == false)
                {
                    continue;
                }

                if (warehouse.Material == null)
                {
                    continue;
                }

                _warehouse = warehouse;
                return true;
            }

            return false;
        }

        public void Update()
        {

        }

        private IEnumerator StartPickup()
        {
            var delay = new WaitForSeconds(_lift.Duration);

            while (_lift.IsFull == false)
            {
                _animator.SetTrigger(Pickup);

                var liftable = _warehouse.Material;
                liftable.gameObject.SetActive(true);

                _lift.PickUp(liftable, _pickupPoint);
                yield return delay;
            }
        }
    }
}
