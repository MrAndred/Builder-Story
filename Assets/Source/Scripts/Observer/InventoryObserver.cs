using UnityEngine;

namespace BuilderStory
{
    public class InventoryObserver
    {
        private Structure[] _structures;
        private LayerMask _layerMask;

        public InventoryObserver(Structure[] structures, LayerMask layerMask)
        {
            _structures = structures;
            _layerMask = layerMask;
        }

        public void Subscribe(IReadOnlyLift lift)
        {
            lift.OnPickedUp += PickedUp;
        }

        public void SubscribePlayer(IReadOnlyLift lift)
        {
            lift.OnPickedUp += PlayerPickedUp;
            lift.OnDropped += Dropped;
        }

        public void Unsubscribe(IReadOnlyLift lift)
        {
            lift.OnPickedUp -= PickedUp;
        }

        public void UnsubscribePlayer(IReadOnlyLift lift)
        {
            lift.OnPickedUp -= PlayerPickedUp;
            lift.OnDropped -= Dropped;
        }

        private void PickedUp(ILiftable liftable)
        {
            var structure = GetBuildingStructure();
            structure.TryHighlight(liftable);
        }

        private void PlayerPickedUp(ILiftable liftable)
        {
            foreach (var structure in _structures)
            {
                structure.TryHighlight(liftable);
            }
        }

        private void Dropped(ILiftable liftable, Transform point)
        {
            var colliders = Physics.OverlapSphere(point.position, 1f, _layerMask);

            foreach (var collider in colliders)
            {
                if (collider.TryGetComponent(out Structure structure) == true)
                {
                }

                foreach (var notPlacedSturcture in _structures)
                {
                    if (notPlacedSturcture != structure)
                    {
                        notPlacedSturcture.TryUnhighlight(liftable);
                    }
                }

                return;
            }
        }

        private Structure GetBuildingStructure()
        {
            foreach (var structure in _structures)
            {
                if (structure.IsBuilding == true && structure.IsBuilt() == false)
                {
                    return structure;
                }
            }

            return null;
        }
    }
}
