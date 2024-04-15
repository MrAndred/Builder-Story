using System;

namespace BuilderStory.Saves
{
    [Serializable]
    public class ProgressDTO
    {
        public ProgressDTO(
            int money,
            float moneyMultiplier,
            int level,
            int reputation,
            int playerSpeedLevel,
            int playerCapacityLevel,
            int workersCountLevel,
            int workersSpeedLevel,
            int workersCapacityLevel)
        {
            Money = money;
            MoneyMultiplier = moneyMultiplier;
            Level = level;
            Reputation = reputation;
            PlayerSpeedLevel = playerSpeedLevel;
            PlayerCapacityLevel = playerCapacityLevel;
            WorkersCountLevel = workersCountLevel;
            WorkersSpeedLevel = workersSpeedLevel;
            WorkersCapacityLevel = workersCapacityLevel;
        }

        public int Money { get; } = 0;

        public float MoneyMultiplier { get; } = 1;

        public int Level { get; } = 1;

        public int Reputation { get; } = 0;

        public int PlayerSpeedLevel { get; } = 1;

        public int PlayerCapacityLevel { get; } = 1;

        public int WorkersCountLevel { get; } = 1;

        public int WorkersSpeedLevel { get; } = 1;

        public int WorkersCapacityLevel { get; } = 1;
    }
}
