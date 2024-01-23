using System;
using System.Diagnostics;

namespace BuilderStory
{
    public class Reputation
    {
        private const int AddReputation = 1;

        public Reputation(int current, int max, int moneyMultiplier)
        {
            Max = max;
            Current = current;
            MoneyMultiplier = moneyMultiplier;
        }

        public event Action ReputationChanged;
        
        public int MoneyMultiplier { get; private set; }

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
