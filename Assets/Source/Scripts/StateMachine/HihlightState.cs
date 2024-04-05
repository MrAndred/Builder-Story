//using UnityEngine.AI;

//namespace BuilderStory
//{
//    public class HihlightState : IBehaviour
//    {
//        private Structure[] _structures;
//        private Lift _lift;
//        private NavMeshAgent _agent;

//        private int _lastLiftableCount = 0;

//        public HihlightState(Structure[] structures, Lift lift, NavMeshAgent agent = null)
//        {
//            _structures = structures;
//            _lift = lift;

//            if (agent != null)
//            {
//                _agent = agent;
//            }
//        }

//        public void Enter()
//        {

//            var liftables = _lift.Liftables;
//            int newLiftableCount = _lift.Length;

//            _lastLiftableCount = newLiftableCount;

//            if (_lift.IsEmpty == true)
//            {
//                foreach (var building in _structures)
//                {
//                    building.RemoveHighlight();
//                }

//                return;
//            }

//            if (TryGetBuilding(out Structure structure) == true)
//            {
//                structure.TryHighlight(new ILiftable[] { liftables[newLiftableCount - 1] });
//            }
//        }

//        public void Exit()
//        {

//        }

//        public bool IsReady()
//        {
//            if (_agent?.hasPath == true)
//            {
//                return false;
//            }

//            if (_lastLiftableCount != _lift.Length)
//            {
//                return true;
//            }

//            return false;
//        }

//        public void Update()
//        {

//        }

//        private bool TryGetBuilding(out Structure buildingStructure)
//        {
//            foreach (var structure in _structures)
//            {
//                if (structure.IsBuilding)
//                {
//                    buildingStructure = structure;
//                    return true;
//                }
//            }

//            buildingStructure = null;
//            return false;
//        }
//    }
//}
