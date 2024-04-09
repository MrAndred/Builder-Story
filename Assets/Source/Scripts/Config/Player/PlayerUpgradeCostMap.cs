using System.Collections.Generic;

namespace BuilderStory.Config.Player
{
    public class PlayerUpgradeCostMap
    {
        private Dictionary<int, int> _playerSpeedCosts = new Dictionary<int, int>
        {
            { 1, 10 },
            { 2, 25 },
            { 3, 50 },
            { 4, 70 },
        };

        private Dictionary<int, int> _playerCapacityCosts = new Dictionary<int, int>
        {
            { 1, 20 },
            { 2, 50 },
            { 3, 70 },
            { 4, 90 },
        };

        public int GetSpeedUpgradeCost(int level)
        {
            if (_playerSpeedCosts.ContainsKey(level) == false)
            {
                return -1;
            }

            return _playerSpeedCosts[level];
        }

        public int GetCapacityUpgradeCost(int level)
        {
            if (_playerCapacityCosts.ContainsKey(level) == false)
            {
                return -1;
            }

            return _playerCapacityCosts[level];
        }
    }
}
