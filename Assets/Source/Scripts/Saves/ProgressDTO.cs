using System;

namespace BuilderStory
{
    [Serializable]
    public class ProgressDTO
    {
        public int Money = 0;

        public float MoneyMultiplier = 1;

        public int Level = 1;

        public int Reputation = 0;

        public int PlayerSpeedLevel = 1;

        public int PlayerCapacityLevel = 1;

        public int WorkersCountLevel = 1;

        public int WorkersSpeedLevel = 1;

        public int WorkersCapacityLevel = 1;
    }
}
