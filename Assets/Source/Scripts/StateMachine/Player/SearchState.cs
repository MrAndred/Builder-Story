using UnityEngine;

namespace BuilderStory
{
    public class SearchState : IBehaviour
    {
        private readonly Transform _searchPoint;
        private readonly LayerMask _interactablesMask;
        private readonly float _searchDistance;

        public SearchState(LayerMask interactablesMask, float searchDistance, Transform searchPoint)
        {
            _interactablesMask = interactablesMask;
            _searchDistance = searchDistance;
            _searchPoint = searchPoint;
        }

        public void Enter()
        {

        }

        public void Exit()
        {

        }

        public bool IsReady()
        {
            var colliders = Physics.OverlapSphere(_searchPoint.transform.position, _searchDistance, _interactablesMask);

            if (colliders.Length == 0)
            {
                return true;
            }

            return false;
        }

        public void Update()
        {

        }
    }
}
