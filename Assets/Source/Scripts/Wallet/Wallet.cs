using System;

namespace BuilderStory
{
    public class Wallet
    {
        private int _money = 0;
        private float _moneyMultiplier = 1f;
        private ProgressSaves _progressSaves;

        public Wallet(ProgressSaves progressSaves)
        {
            _money = progressSaves.Money;
            _moneyMultiplier = progressSaves.MoneyMultiplier;
        }

        public event Action MoneyChanged;

        public int Money => _money;

        public void AddMoney(int money)
        {
            if (money < 0)
            {
                return;
            }

            _money += UnityEngine.Mathf.RoundToInt(money * _progressSaves.MoneyMultiplier);
            MoneyChanged?.Invoke();
        }

        public bool TrySpendMoney(int money)
        {
            if (IsEnoughMoney(money) == false)
            {
                return false;
            }

            _money -= money;
            MoneyChanged?.Invoke();
            return true;
        }

        private bool IsEnoughMoney(int money)
        {
            return _money >= money;
        }
    }
}
