using System.Collections.Generic;

namespace BuilderStory
{
    public class WorkerUpgradeMap
    {
        private Dictionary<int, float> _speedMap = new Dictionary<int, float>()
        {
            {1, 4f },
            {2, 4.4f },
            {3, 4.8f },
            {4, 5.2f },
            {5, 5.6f },
            {6, 6f },
            {7, 6.4f },
            {8, 6.8f },
            {9, 7.2f },
            {10, 7.6f }
        };

        public Dictionary<int, int> _capacityMap = new Dictionary<int, int>()
        {
            {1, 1 },
            {2, 2 },
            {3, 3 },
            {4, 4 },
            {5, 5 },
            {6, 6 },
            {7, 6 },
            {8, 8 },
            {9, 9 },
            {10, 10 }
        };

        public Dictionary<int, int> _countMap = new Dictionary<int, int>() {
            {1,  1},
            {2, 2 },
            {3, 3 },
            {4, 4 },
            {5, 5 },
            {6, 6 },
            {7, 6 },
            {8, 8 },
            {9, 9 },
            {10, 10 }
        };

        public float GetSpeed(int level)
        {
            return _speedMap[level];
        }

        public int GetCapacity(int level)
        {
            return _capacityMap[level];
        }

        public int GetCount(int level)
        {
            return _countMap[level];
        }
    }
}
