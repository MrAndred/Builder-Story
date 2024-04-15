using System;
using System.Collections;
using BuilderStory.Pause;
using BuilderStory.Saves;
using BuilderStory.WalletSystem;
using UnityEngine;

namespace BuilderStory.UI
{
    public class BoostersGroup : MonoBehaviour
    {
        private const float ShowDelay = 45f;
        private const int RewardedMuliplier = 2;
        private const float MultiplierTime = 120f;
        private const int RewardedAdd = 50;

        [SerializeField] private BoosterRenderer _moneyMultiplier;
        [SerializeField] private BoosterRenderer _moneyAdd;

        private ProgressSaves _saves;
        private Wallet _wallet;
        private PauseSystem _pauseSystem;

        private void OnDisable()
        {
            _moneyMultiplier.OnClick -= MuliplierClick;
            _moneyAdd.OnClick -= AddClick;
        }

        public void Init(ProgressSaves saves, Wallet wallet, PauseSystem pauseSystem)
        {
            _saves = saves;
            _wallet = wallet;
            _pauseSystem = pauseSystem;

            _moneyAdd.OnClick += AddClick;
            _moneyMultiplier.OnClick += MuliplierClick;

            StartCoroutine(ShowBoosters());
        }

        public void MuliplierClick(BoosterRenderer booster)
        {
            ShowRewarded(OnMultiplierRewarded);
            booster.Hide();
        }

        public void AddClick(BoosterRenderer booster)
        {
            ShowRewarded(OnMoneyAddRewarded);
            booster.Hide();
        }

        private void ShowRewarded(Action onRewarded)
        {
            _pauseSystem.AdPauseGame();
#if UNITY_EDITOR
            _pauseSystem.AdResumeGame();
            return;
#else
            Agava.YandexGames.VideoAd.Show(
                OnRewardedAdOpened,
                onRewarded,
                OnRewardedClose,
                OnRewardedAdError
            );
#endif
        }

        private void OnRewardedAdOpened()
        {
            _pauseSystem.AdPauseGame();
        }

        private void OnRewardedAdError(string message)
        {
            Debug.LogWarning(message);
            _pauseSystem.AdResumeGame();
        }

        private void OnRewardedClose()
        {
            _pauseSystem.AdResumeGame();
        }

        private void OnMultiplierRewarded()
        {
            _saves.SetMoneyMultiplier(RewardedMuliplier);

            StartCoroutine(DelayedResetMultiplier());
        }

        private void OnMoneyAddRewarded()
        {
            _wallet.AddMoney(RewardedAdd);
        }

        private IEnumerator DelayedResetMultiplier()
        {
            yield return new WaitForSeconds(MultiplierTime);
            _saves.ResetMoneyMultiplier();
        }

        private IEnumerator ShowBoosters()
        {
            var delay = new WaitForSeconds(ShowDelay);

            yield return delay;
            _moneyAdd.Show();

            yield return delay;
            _moneyMultiplier.Show();
        }
    }
}
