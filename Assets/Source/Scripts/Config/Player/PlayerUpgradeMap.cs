using System.Collections.Generic;

namespace BuilderStory.Config.Player
{
    public class PlayerUpgradeMap
    {
        private readonly Dictionary<int, float> _speedLevels = new Dictionary<int, float>
        {
            { 1, 4f },
            { 2, 4.4f },
            { 3, 4.8f },
            { 4, 5.2f },
            { 5, 5.6f },
            { 6, 6f },
            { 7, 6.4f },
            { 8, 6.8f },
            { 9, 7.2f },
            { 10, 7.6f },
        };

        private readonly Dictionary<int, int> _capacityLevels = new Dictionary<int, int>
        {
            { 1, 1 },
            { 2, 2 },
            { 3, 3 },
            { 4, 4 },
            { 5, 5 },
            { 6, 6 },
            { 7, 6 },
            { 8, 8 },
            { 9, 9 },
            { 10, 10 },
        };

        public float GetSpeed(int level)
        {
            return _speedLevels[level];
        }

        public int GetCapacity(int level)
        {
            return _capacityLevels[level];
        }
    }
}
