using System.Collections.Generic;

namespace BuilderStory
{
    public class WorkerUpgradeCostMap
    {
        private Dictionary<int, int> _workerSpeedCosts = new Dictionary<int, int>
        {
            { 1, 20 },
            { 2, 40 },
            { 3, 70 },
            { 4, 90 }
        };

        private Dictionary<int, int> _workerCapacityCosts = new Dictionary<int, int>
        {
            { 1, 50 },
            { 2, 75 },
            { 3, 100 },
            { 4, 125 }
        };

        private Dictionary<int, int> _workerCountCosts = new Dictionary<int, int>
        {
            { 1, 100 },
            { 2, 150 },
            { 3, 200 },
            { 4, 250 },
        };

        public int GetSpeedUpgradeCost(int level)
        {
            if (_workerSpeedCosts.ContainsKey(level) == false)
            {
                return -1;
            }

            return _workerSpeedCosts[level];
        }

        public int GetCapacityUpgradeCost(int level)
        {
            if (_workerCapacityCosts.ContainsKey(level) == false)
            {
                return -1;
            }

            return _workerCapacityCosts[level];
        }

        public int GetCountUpgradeCost(int level)
        {
            if (_workerCountCosts.ContainsKey(level) == false)
            {
                return -1;
            }

            return _workerCountCosts[level];
        }
    }
}
