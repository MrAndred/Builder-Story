namespace BuilderStory.Saves
{
    public class ProgressModel
    {
#if UNITY_EDITOR
        private const int DefaultMoney = 100000;
#else
        private const int DefaultMoney = 0;
#endif

        private const float DefaultMoneyMultiplier = 1f;
        private const int DefaultLevel = 1;
        private const int DefaultReputation = 0;

        public ProgressModel()
        {
            Reputation = DefaultReputation;
            Money = DefaultMoney;
            MoneyMultiplier = DefaultMoneyMultiplier;
            Level = DefaultLevel;
        }

        public ProgressModel(int reputation, int money, float moneyMultiplier, int level)
        {
            Reputation = reputation;
            Money = money;
            MoneyMultiplier = moneyMultiplier;
            Level = level;
        }

        public int Reputation { get; private set; }

        public int Money { get; private set; }

        public float MoneyMultiplier { get; private set; }

        public int Level { get; private set; }

        public void AddMoney(int money)
        {
            if (money < 0)
            {
                return;
            }

            Money += UnityEngine.Mathf.RoundToInt(money * MoneyMultiplier);
        }

        public void SpendMoney(int money)
        {
            if (IsEnoughMoney(money) == false)
            {
                return;
            }

            Money -= money;
        }

        public void AddReputation(int reputation)
        {
            if (reputation < 0)
            {
                return;
            }

            Reputation += reputation;
        }

        public void NextLevel()
        {
            Level++;
        }

        public void SetMoneyMultiplier(float multiplier)
        {
            MoneyMultiplier = multiplier;
        }

        public void ResetMoneyMultiplier()
        {
            MoneyMultiplier = DefaultMoneyMultiplier;
        }

        private bool IsEnoughMoney(int money)
        {
            return Money >= money;
        }
    }
}
