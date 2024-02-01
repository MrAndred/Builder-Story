using System;

namespace BuilderStory
{
    public class Reputation
    {
        private const int AddReputation = 1;

        public Reputation(int current, int max, float moneyMultiplier)
        {
            Max = max;
            Current = current;
            MoneyMultiplier = moneyMultiplier;
        }

        public event Action ReputationChanged;
        
        public float MoneyMultiplier { get; private set; }

        public int Max { get; private set; }

        public int Current { get; private set; }

        public void Add()
        {
            if (Current + AddReputation > Max)
            {
                return;
            }

            Current += AddReputation;
            ReputationChanged?.Invoke();
        }
    }
}
