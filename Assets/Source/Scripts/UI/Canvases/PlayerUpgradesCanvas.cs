using UnityEngine;

namespace BuilderStory.UI.Canvases
{
    public class PlayerUpgradesCanvas : UpgradesCanvas
    {
        [SerializeField] private UpgradeContainerRenderer _speedUpgrade;
        [SerializeField] private UpgradeContainerRenderer _capacityUpgrade;

        protected override void SubscribeToEvents()
        {
            SubscribeToUpgrades(
                _speedUpgrade,
                OnSpeedUpgradeButtonClicked,
                SpeedUpgradeAccsesable);

            SubscribeToUpgrades(
                _capacityUpgrade,
                OnCapacityButtonClicked,
                CapacityUpgradeAccsesable);
        }

        protected override void UnsubscribeFromEvents()
        {
            UnsubscribeFromUpgrades(
                _speedUpgrade,
                OnSpeedUpgradeButtonClicked,
                SpeedUpgradeAccsesable);

            UnsubscribeFromUpgrades(
                _capacityUpgrade,
                OnCapacityButtonClicked,
                CapacityUpgradeAccsesable);
        }

        protected override void UpdateUI()
        {
            _speedUpgrade.Render(Saves.PlayerSpeedLevel, Saves.PlayerSpeedCost);
            SpeedUpgradeAccsesable();

            _capacityUpgrade.Render(Saves.PlayerCapacityLevel, Saves.PlayerCapacityCost);
            CapacityUpgradeAccsesable();
        }

        private void OnSpeedUpgradeButtonClicked(int cost)
        {
            BuyUpgrade(cost, Saves.UpgradePlayerSpeed);
        }

        private void OnCapacityButtonClicked(int cost)
        {
            BuyUpgrade(cost, Saves.UpgradePlayerCapacity);
        }

        private void SpeedUpgradeAccsesable()
        {
            MakeUpgradeAccessible(_speedUpgrade, Saves.PlayerSpeedCost);
        }

        private void CapacityUpgradeAccsesable()
        {
            MakeUpgradeAccessible(_capacityUpgrade, Saves.PlayerCapacityCost);
        }
    }
}
