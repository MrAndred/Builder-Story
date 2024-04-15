using System;
using BuilderStory.Saves;

namespace BuilderStory.WalletSystem
{
    public class Wallet
    {
        private ProgressSaves _progressSaves;

        public Wallet(ProgressSaves progressSaves)
        {
            _progressSaves = progressSaves;
        }

        public event Action MoneyChanged;

        public int Money => _progressSaves.Money;

        public void AddMoney(int money)
        {
            _progressSaves.AddMoney(money);
            MoneyChanged?.Invoke();
        }

        public bool SpendMoney(int money)
        {
            _progressSaves.SpendMoney(money);
            MoneyChanged?.Invoke();
            return true;
        }

        public bool IsEnoughMoney(int money)
        {
            if (money < 0)
            {
                return false;
            }

            return _progressSaves.Money >= money;
        }
    }
}
