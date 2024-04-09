using BuilderStory.Config.Player;

namespace BuilderStory.Saves.Player
{
    public class PlayerModel
    {
        private const int DefaultSpeedLevel = 1;
        private const int DefaultCapacityLevel = 1;

        private PlayerUpgradeMap _playerUpgradeMap = new PlayerUpgradeMap();
        private PlayerUpgradeCostMap _playerUpgradeCostMap = new PlayerUpgradeCostMap();
        private int _speedLevel;
        private int _capacityLevel;

        public PlayerModel()
        {
            _capacityLevel = DefaultCapacityLevel;
            _speedLevel = DefaultSpeedLevel;
        }

        public PlayerModel(int speedLevel, int capacity)
        {
            _speedLevel = speedLevel;
            _capacityLevel = capacity;
        }

        public int CapacityLevel => _capacityLevel;

        public int Capacity => _playerUpgradeMap.GetCapacity(_capacityLevel);

        public int SpeedLevel => _speedLevel;

        public float Speed => _playerUpgradeMap.GetSpeed(_speedLevel);

        public int SpeedUpgradeCost =>
            _playerUpgradeCostMap.GetSpeedUpgradeCost(_speedLevel);

        public int CapacityUpgradeCost =>
            _playerUpgradeCostMap.GetCapacityUpgradeCost(_capacityLevel);

        public void UpgradeSpeed()
        {
            _speedLevel += 1;
        }

        public void UpgradeCapacity()
        {
            _capacityLevel += 1;
        }
    }
}
