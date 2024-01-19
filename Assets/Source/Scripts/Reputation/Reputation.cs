using System;
using System.Diagnostics;

namespace BuilderStory
{
    public class Reputation
    {
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
            if (Current + 1 > Max)
            {
                return;
            }

            Current += 1;
            ReputationChanged?.Invoke();
        }
    }
}
