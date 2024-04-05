using System;
using System.Collections;
using UnityEngine;

namespace BuilderStory
{
    [Serializable]
    public class ProgressSaves
    {
        private const string LeaderbordName = "reputatuion";

        private ProgressModel _progressModel;
        private PlayerModel _playerModel;
        private WorkerModel _workerModel;

        public ProgressSaves()
        {
            _progressModel = new ProgressModel();
            _playerModel = new PlayerModel();
            _workerModel = new WorkerModel();
        }

        public event Action DataLoaded;

        public event Action<float> PlayerSpeedChanged;

        public event Action<int> PlayerCapacityChanged;

        public event Action<float> WorkersSpeedChanged;

        public event Action<int> WorkersCapacityChanged;

        public event Action<int> WorkersCountChanged;

        public int Money => _progressModel.Money;

        public int Reputation => _progressModel.Reputation;

        public float MoneyMultiplier => _progressModel.MoneyMultiplier;

        public int Level => _progressModel.Level;

        public float PlayerSpeed => _playerModel.Speed;

        public int PlayerSpeedLevel => _playerModel.SpeedLevel;

        public int PlayerSpeedCost => _playerModel.SpeedUpgradeCost;

        public int PlayerCapacity => _playerModel.Capacity;

        public int PlayerCapacityLevel => _playerModel.CapacityLevel;

        public int PlayerCapacityCost => _playerModel.CapacityUpgradeCost;

        public int WorkersCount => _workerModel.Count;

        public int WorkersCountLevel => _workerModel.CountLevel;

        public int WorkersCountCost => _workerModel.CountUpgradeCost;

        public float WorkersSpeed => _workerModel.Speed;

        public int WorkersSpeedLevel => _workerModel.SpeedLevel;

        public int WorkersSpeedCost => _workerModel.SpeedUpgradeCost;

        public int WorkersCapacity => _workerModel.Capacity;

        public int WorkersCapacityLevel => _workerModel.CapacityLevel;

        public int WorkersCapacityCost => _workerModel.CapacityUpgradeCost;

        public void LoadData(MonoBehaviour objectInstance)
        {
#if UNITY_EDITOR == true
            objectInstance.StartCoroutine(SimulateInit());
            return;
#else
            Agava.YandexGames.PlayerAccount.GetCloudSaveData((data) =>
            {
                var saves = new ProgressDTO();

                var json = JsonUtility.FromJson<ProgressDTO>(data);

                _playerModel = new PlayerModel(json.PlayerSpeedLevel, json.PlayerCapacityLevel);
                
                _workerModel = new WorkerModel(
                    json.WorkersCountLevel, 
                    json.WorkersSpeedLevel, 
                    json.WorkersCapacityLevel);

                _progressModel = new ProgressModel(
                    json.Reputation, 
                    json.Money, 
                    json.MoneyMultiplier, 
                    json.Level);

                DataLoaded?.Invoke();
            });

            return;
#endif
        }

        private IEnumerator SimulateInit()
        {
            yield return new WaitForSeconds(1);
            DataLoaded?.Invoke();
        }

        public void ResetData()
        {
            _progressModel = new ProgressModel();
            _playerModel = new PlayerModel();
            _workerModel = new WorkerModel();

#if UNITY_EDITOR != true
            ProgressDTO Saves = new ProgressDTO
            {
                PlayerSpeedLevel = _playerModel.SpeedLevel,
                PlayerCapacityLevel = _playerModel.CapacityLevel,
                WorkersCountLevel = _workerModel.Count,
                WorkersSpeedLevel = _workerModel.SpeedLevel,
                WorkersCapacityLevel = _workerModel.CapacityLevel,
                Reputation = _progressModel.Reputation,
                Money = _progressModel.Money,
                MoneyMultiplier = _progressModel.MoneyMultiplier,
                Level = _progressModel.Level
            };

            Agava.YandexGames.Leaderboard.SetScore(LeaderbordName, 0);
            Agava.YandexGames.PlayerAccount.SetCloudSaveData(JsonUtility.ToJson(Saves));
#endif
        }

        public void SaveData()
        {
#if UNITY_EDITOR == true
            return;
#else
            ProgressDTO Saves = new ProgressDTO
            {
                PlayerSpeedLevel = _playerModel.SpeedLevel,
                PlayerCapacityLevel = _playerModel.CapacityLevel,
                WorkersCountLevel = _workerModel.CountLevel,
                WorkersSpeedLevel = _workerModel.SpeedLevel,
                WorkersCapacityLevel = _workerModel.CapacityLevel,
                Reputation = _progressModel.Reputation,
                Money = _progressModel.Money,
                MoneyMultiplier = _progressModel.MoneyMultiplier,
                Level = _progressModel.Level
            };

            Agava.YandexGames.PlayerAccount.SetCloudSaveData(JsonUtility.ToJson(Saves));
            Agava.YandexGames.Leaderboard.SetScore(LeaderbordName, _progressModel.Reputation);
#endif
        }

        public void AddMoney(int money)
        {
            _progressModel.AddMoney(money);
        }

        public void SpendMoney(int money)
        {
            _progressModel.SpendMoney(money);
        }

        public void AddReputation(int reputation)
        {
            _progressModel.AddReputation(reputation);
        }

        public void UpgradePlayerSpeed()
        {
            _playerModel.UpgradeSpeed();
            PlayerSpeedChanged?.Invoke(_playerModel.Speed);
        }

        public void UpgradePlayerCapacity()
        {
            _playerModel.UpgradeCapacity();
            PlayerCapacityChanged?.Invoke(_playerModel.Capacity);
        }

        public void UpgradeWorkersCount()
        {
            _workerModel.UpgradeCount();
            WorkersCountChanged?.Invoke(_workerModel.Count);
        }

        public void UpgradeWorkersSpeed()
        {
            _workerModel.UpgradeSpeed();
            WorkersSpeedChanged?.Invoke(_workerModel.Speed);
        }

        public void UpgradeWorkersCapacity()
        {
            _workerModel.UpgradeCapacity();
            WorkersCapacityChanged?.Invoke(_workerModel.Capacity);
        }

        public void NextLevel()
        {
            _progressModel.NextLevel();
        }

        public void UpdateLeaderbord()
        {
            Agava.YandexGames.Leaderboard.SetScore(LeaderbordName, Reputation);
        }

        public void SetMoneyMultiplier(int multiplier)
        {
            if (multiplier < 1)
            {
                return;
            }

            _progressModel.SetMoneyMultiplier(multiplier);
        }

        public void ResetMoneyMultiplier()
        {
            _progressModel.ResetMoneyMultiplier();
        }
    }
}
