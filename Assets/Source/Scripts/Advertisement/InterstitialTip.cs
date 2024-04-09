using System.Collections;
using BuilderStory.Pause;
using TMPro;
using UnityEngine;

namespace BuilderStory.Advertisement
{
    public class InterstitialTip : MonoBehaviour
    {
        private const string AdShowTimePhraseKey = "ad_show_time";
        private const int Delay = 1;
        private const int SecondsToShowAd = 3;

        [SerializeField] private TMP_Text _text;

        private Coroutine _coroutine;
        private PauseSystem _pauseSystem;

        public void Init(PauseSystem pauseSystem)
        {
            _pauseSystem = pauseSystem;
        }

        public void ShowPopup()
        {
            gameObject.SetActive(true);
            _pauseSystem.AdPauseGame();
            _coroutine = StartCoroutine(InitPopupInterstitialAd());
        }

        public void Show()
        {
            _pauseSystem.AdPauseGame();
#if UNITY_EDITOR == true
            ResumeGame();
            return;
#else
            Agava.YandexGames.InterstitialAd.Show(
                OnInterstitalAdOpened,
                OnInterstitalAdClosed,
                OnInterstitalError,
                null
            );
#endif
        }

        private IEnumerator InitPopupInterstitialAd()
        {
            var delay = new WaitForSecondsRealtime(Delay);

            for (int i = SecondsToShowAd; i >= 0; i--)
            {
                Render(i);
                yield return delay;
            }

#if UNITY_EDITOR == true
            StartCoroutine(SimulateAd());
#else
            Agava.YandexGames.InterstitialAd.Show(
                OnInterstitalAdOpened,
                OnInterstitalAdClosed,
                OnInterstitalError,
                null
            );
#endif
        }

        private void Render(int secondsLeft)
        {
            string text = Lean.Localization.LeanLocalization.GetTranslationText(AdShowTimePhraseKey);
            _text.text = string.Format(text, secondsLeft);
        }

        private void ResumeGame()
        {
            _pauseSystem.AdResumeGame();
            gameObject.SetActive(false);

            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }
        }

        private void OnInterstitalAdOpened()
        {
            _pauseSystem.AdPauseGame();
        }

        private void OnInterstitalAdClosed(bool closed)
        {
            ResumeGame();
        }

        private void OnInterstitalError(string err)
        {
            if (string.IsNullOrEmpty(err) == false)
            {
                Debug.LogError(err);
                ResumeGame();
            }
        }

        private IEnumerator SimulateAd()
        {
            yield return new WaitForSecondsRealtime(Delay);
            ResumeGame();
        }
    }
}
