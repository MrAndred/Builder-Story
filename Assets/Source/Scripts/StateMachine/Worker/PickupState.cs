using System.Collections;
using UnityEngine;

namespace BuilderStory
{
    public class PickupState : IBehaviour
    {
        private Transform _pickupPoint;
        private Lift _lift;
        private float _pickupDistance;
        private LayerMask _warehouseLayer;

        private MaterialWarehouse _warehouse;

        private Coroutine _pickupCoroutine;

        public PickupState(
            Lift lift,
            Transform pickupPoint,
            float pickupLength,
            LayerMask warehouseLayer)
        {
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
            _lift.StopCoroutine(_pickupCoroutine);
        }

        public bool IsReady()
        {
            if (_lift.IsFull)
            {
                return false;
            }

            var hit = Physics.Raycast(
                _lift.transform.position,
                _lift.transform.forward,
                out var hitInfo,
                _pickupDistance,
                _warehouseLayer);

            if (!hit)
            {
                return false;
            }

            if (hitInfo.collider.TryGetComponent(out MaterialWarehouse warehouse) == false)
            {
                return false;
            }

            _warehouse = warehouse;
            return true;
        }

        public void Update()
        {

        }

        private IEnumerator StartPickup()
        {
            var delay = new WaitForSeconds(_lift.Duration);

            while (_lift.IsFull == false)
            {
                var liftable = _warehouse.Material;

                _lift.PickUp(liftable, _pickupPoint);
                yield return delay;
            }
        }
    }
}
