using BuilderStory.Config.Worker;

namespace BuilderStory.Saves.Worker
{
    public class WorkerModel
    {
        private const int DefaultSpeedLevel = 1;
        private const int DefaultCapacityLevel = 1;
        private const int DefaultWorkersCountLevel = 1;

        private WorkerUpgradeMap _workerUpgradeMap = new WorkerUpgradeMap();
        private WorkerUpgradeCostMap _workerUpgradeCostMap = new WorkerUpgradeCostMap();

        private int _speedLevel;
        private int _capacityLevel;
        private int _countLevel;

        public WorkerModel()
        {
            _capacityLevel = DefaultCapacityLevel;
            _speedLevel = DefaultSpeedLevel;
            _countLevel = DefaultWorkersCountLevel;
        }

        public WorkerModel(int countLevel, int speedLevel, int capacityLevel)
        {
            _countLevel = countLevel;
            _speedLevel = speedLevel;
            _capacityLevel = capacityLevel;
        }

        public int CountLevel => _countLevel;

        public int Count => _workerUpgradeMap.GetCount(_countLevel);

        public int CountUpgradeCost =>
            _workerUpgradeCostMap.GetCountUpgradeCost(_countLevel);

        public int SpeedLevel => _speedLevel;

        public float Speed => _workerUpgradeMap.GetSpeed(_speedLevel);

        public int SpeedUpgradeCost =>
            _workerUpgradeCostMap.GetSpeedUpgradeCost(_speedLevel);

        public int CapacityLevel => _capacityLevel;

        public int Capacity => _workerUpgradeMap.GetCapacity(_capacityLevel);

        public int CapacityUpgradeCost =>
            _workerUpgradeCostMap.GetCapacityUpgradeCost(_capacityLevel);

        public void UpgradeSpeed()
        {
            _speedLevel += 1;
        }

        public void UpgradeCapacity()
        {
            _capacityLevel += 1;
        }

        public void UpgradeCount()
        {
            _countLevel += 1;
        }
    }
}
