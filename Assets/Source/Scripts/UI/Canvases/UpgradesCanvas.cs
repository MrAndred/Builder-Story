using System;
using BuilderStory.Saves;
using BuilderStory.WalletSystem;
using UnityEngine;

namespace BuilderStory.UI.Canvases
{
    public abstract class UpgradesCanvas : MonoBehaviour
    {
        public Wallet Wallet;
        public ProgressSaves Saves;

        public void Init(Wallet wallet, ProgressSaves saves)
        {
            Wallet = wallet;
            Saves = saves;
            OnCanvasInitialized();
        }

        protected void MakeUpgradeAccessible(UpgradeContainerRenderer upgrade, int cost)
        {
            bool enoughMoney = Wallet.IsEnoughMoney(cost);

            if (enoughMoney)
            {
                upgrade.SetInteractable();
            }
            else
            {
                upgrade.SetNotInteractable();
            }
        }

        protected void SubscribeToUpgrades(
               UpgradeContainerRenderer containerRenderer,
               Action<int> containerClickHandler,
               Action walletHandler)
        {
            containerRenderer.OnButtonClicked += containerClickHandler;
            Wallet.MoneyChanged += walletHandler;
        }

        protected void UnsubscribeFromUpgrades(
            UpgradeContainerRenderer containerRenderer,
            Action<int> containerClickHandler,
            Action walletHandler)
        {
            containerRenderer.OnButtonClicked -= containerClickHandler;
            Wallet.MoneyChanged -= walletHandler;
        }

        protected void BuyUpgrade(int cost, Action upgradeHandler)
        {
            Wallet.SpendMoney(cost);
            upgradeHandler?.Invoke();
            UpdateUI();
        }

        protected virtual void OnCanvasInitialized()
        {
            UpdateUI();
        }

        protected virtual void OnEnable()
        {
            SubscribeToEvents();
            UpdateUI();
        }

        protected virtual void OnDisable()
        {
            UnsubscribeFromEvents();
        }

        protected abstract void UpdateUI();

        protected abstract void SubscribeToEvents();

        protected abstract void UnsubscribeFromEvents();
    }
}