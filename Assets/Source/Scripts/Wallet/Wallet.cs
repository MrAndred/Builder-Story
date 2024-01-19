using System;

namespace BuilderStory
{
    public class Wallet
    {
        private int _money = 0;
        private Reputation _reputation;

        public Wallet(int money, Reputation reputation)
        {
            _money = money;
            _reputation = reputation;
        }

        public event Action MoneyChanged;

        public int Money => _money;

        public void AddMoney(int money)
        {
            if (money < 0)
            {
                return;
            }

            _money += money * _reputation.MoneyMultiplier;
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
