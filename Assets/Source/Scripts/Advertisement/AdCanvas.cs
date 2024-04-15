using System.Collections;
using BuilderStory.Pause;
using UnityEngine;

namespace BuilderStory.Advertisement
{
    public class AdCanvas : MonoBehaviour
    {
#if UNITY_EDITOR
        private const float SecondsDelayToShowInterstitialAd = 75;
#else
        private const float SecondsDelayToShowInterstitialAd = 75;
#endif

        [SerializeField] private InterstitialTip _interTip;

        private Coroutine _cronInterstitalAd;

        private void OnEnable()
        {
            if (_cronInterstitalAd == null)
            {
                _cronInterstitalAd = StartCoroutine(CronShowInterstitial());
            }
        }

        private void OnDisable()
        {
            if (_cronInterstitalAd != null)
            {
                StopCoroutine(_cronInterstitalAd);
            }
        }

        public void Init(PauseSystem pauseSystem)
        {
            _interTip.Init(pauseSystem);
            ShowInterstitialAd();
        }

        private void ShowPopupInterstitialAd()
        {
            _interTip.ShowPopup();
        }

        private void ShowInterstitialAd()
        {
            _interTip.Show();
        }

        private IEnumerator CronShowInterstitial()
        {
            while (gameObject.activeSelf)
            {
                yield return new WaitForSeconds(SecondsDelayToShowInterstitialAd);
                ShowPopupInterstitialAd();
            }
        }
    }
}
