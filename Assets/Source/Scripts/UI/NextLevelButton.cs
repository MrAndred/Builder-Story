using System.Collections;
using BuilderStory.Pause;
using BuilderStory.Saves;
using BuilderStory.Util;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace BuilderStory.UI
{
    public class NextLevelButton : MonoBehaviour
    {
        private const float Duration = 1.5f;

        [SerializeField] private RectTransform _container;
        [SerializeField] private Button _button;

        [SerializeField] private float _startXPosition;
        [SerializeField] private float _endXPosition;

        private ProgressSaves _progressSaves;
        private PauseSystem _pauseSystem;

        private void OnEnable()
        {
            _button.onClick.AddListener(Clicked);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(Clicked);
        }

        public void Init(ProgressSaves progressSaves, PauseSystem pauseSystem)
        {
            _progressSaves = progressSaves;
        }

        public void Show()
        {
#if UNITY_EDITOR == false
            Agava.YandexGames.ReviewPopup.CanOpen(ShowReviewPopup);
#endif

            gameObject.SetActive(true);
            _container.DOAnchorPosX(_endXPosition, Duration);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
            _container
                .DOAnchorPosX(_startXPosition, Duration)
                .OnComplete(() => gameObject.SetActive(false));
        }

        private void Clicked()
        {
            _progressSaves.ResetMoneyMultiplier();
#if UNITY_EDITOR == false
            _adCanvas.ShowInterstitialAd();
            _progressSaves.NextLevel();
            _progressSaves.UpdateLeaderbord();

            _progressSaves.SaveData();
#endif
            StartCoroutine(LoadNextLevel());
        }

        private void ShowReviewPopup(bool canOpen, string reason)
        {
            if (canOpen)
            {
                Agava.YandexGames.ReviewPopup.Open();
            }
            else
            {
                Debug.LogWarning("Review popup can't be opened. Reason: " + reason);
            }
        }

        private IEnumerator LoadNextLevel()
        {
            var seconds = 1;
            var delay = new WaitForSecondsRealtime(seconds);

            while (_pauseSystem.IsFocusPaused || _pauseSystem.IsAdPaused)
            {
                yield return delay;
            }

            int level = BuilderStoryUtil.CalculateLevelIndex(_progressSaves.Level);
            SceneManager.LoadSceneAsync(level);
        }
    }
}
