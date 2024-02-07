using System;

namespace BuilderStory
{
    public class Reputation
    {
        private const int AddReputation = 1;

        private ProgressSaves _progressSaves;

        public Reputation(ProgressSaves progressSaves, int max)
        {
            _progressSaves = progressSaves;

            Max = max;
        }

        public event Action ReputationChanged;
        
        public int Max { get; private set; }

        public int Current => _progressSaves.Reputation;

        public void Add()
        {
            if (_progressSaves.Reputation + AddReputation > Max)
            {
                return;
            }

            _progressSaves.AddReputation(AddReputation);
            ReputationChanged?.Invoke();
        }
    }
}
