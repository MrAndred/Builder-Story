using System;
using UnityEngine;

namespace BuilderStory
{
    public class WorkerUpgradesCanvas : UpgradesCanvas
    {
        [SerializeField] private UpgradeContainerRenderer _speedUpgrade;
        [SerializeField] private UpgradeContainerRenderer _capacityUpgrade;
        [SerializeField] private UpgradeContainerRenderer _countUpgrade;

        protected override void UpdateUI()
        {
            _speedUpgrade.Render(Saves.WorkersSpeedLevel, Saves.WorkersSpeedCost);
            SpeedUpgradeAccsesable();

            _capacityUpgrade.Render(Saves.WorkersCapacityLevel, Saves.WorkersCapacityCost);
            CapacityUpgradeAccsesable();

            _countUpgrade.Render(Saves.WorkersCountLevel, Saves.WorkersCountCost);
            CountUpgradeAccsesable();
        }

        protected override void SubscribeToEvents()
        {
            SubscribeToUpgrades(_speedUpgrade, OnSpeedUpgradeButtonClicked, SpeedUpgradeAccsesable);
            SubscribeToUpgrades(_capacityUpgrade, OnCapacityButtonClicked, CapacityUpgradeAccsesable);
            SubscribeToUpgrades(_countUpgrade, OnCountUpgradeButtonClicked, CountUpgradeAccsesable);
        }

        protected override void UnsubscribeFromEvents()
        {
            UnsubscribeFromUpgrades(_speedUpgrade, OnSpeedUpgradeButtonClicked, SpeedUpgradeAccsesable);
            UnsubscribeFromUpgrades(_capacityUpgrade, OnCapacityButtonClicked, CapacityUpgradeAccsesable);
            UnsubscribeFromUpgrades(_countUpgrade, OnCountUpgradeButtonClicked, CountUpgradeAccsesable);
        }

        private void OnSpeedUpgradeButtonClicked(int cost)
        {
            BuyUpgrade(cost, Saves.UpgradeWorkersSpeed);
        }

        private void OnCapacityButtonClicked(int cost)
        {
            BuyUpgrade(cost, Saves.UpgradeWorkersCapacity);
        }

        private void OnCountUpgradeButtonClicked(int cost)
        {
            BuyUpgrade(cost, Saves.UpgradeWorkersCount);
        }

        private void SpeedUpgradeAccsesable()
        {
            MakeUpgradeAccessible(_speedUpgrade, Saves.WorkersSpeedCost);
        }

        private void CapacityUpgradeAccsesable()
        {
            MakeUpgradeAccessible(_capacityUpgrade, Saves.WorkersCapacityCost);
        }

        private void CountUpgradeAccsesable()
        {
            MakeUpgradeAccessible(_countUpgrade, Saves.WorkersCountCost);
        }
    }
}
