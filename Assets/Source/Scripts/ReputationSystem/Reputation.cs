using System;
using BuilderStory.Saves;

namespace BuilderStory.ReputationSystem
{
    public class Reputation
    {
        private const int AddReputation = 1;

        private ProgressSaves _progressSaves;

        public Reputation(ProgressSaves progressSaves, int max)
        {
            _progressSaves = progressSaves;

            Min = _progressSaves.Reputation;
            Max = Min + max;
        }

        public event Action Changed;

        public event Action ReachedMax;

        public int Max { get; private set; }

        public int Min { get; private set; }

        public int Current => _progressSaves.Reputation;

        public void Add()
        {
            if (_progressSaves.Reputation + AddReputation > Max)
            {
                return;
            }

            _progressSaves.AddReputation(AddReputation);
            Changed?.Invoke();

            if (_progressSaves.Reputation >= Max)
            {
                ReachedMax?.Invoke();
            }
        }
    }
}
