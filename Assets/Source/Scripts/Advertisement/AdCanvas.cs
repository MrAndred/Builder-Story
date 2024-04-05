using System.Collections;
using UnityEngine;

namespace BuilderStory
{
    public class AdCanvas : MonoBehaviour
    {
#if UNITY_EDITOR == true
        private const float SecondsDelayToShowInterstitialAd = 75;
#else
        private const float SecondsDelayToShowInterstitialAd = 75;
#endif

        [SerializeField] private InterstitialTip _interTip;

        private Coroutine _coroutine;
        private Coroutine _cronInterstitalAd;

        private void OnEnable()
        {
            if (_coroutine == null)
            {
                _cronInterstitalAd = StartCoroutine(CronShowInterstitial());
            }
        }

        private void OnDisable()
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }

            if (_cronInterstitalAd != null)
            {
                StopCoroutine(_cronInterstitalAd);
            }
        }

        public void Init()
        {
            ShowInterstitialAd();
        }

        public void ShowPopupInterstitialAd()
        {
            _interTip.ShowPopup();
        }

        public void ShowInterstitialAd()
        {
            _interTip.Show();
        }

        private IEnumerator CronShowInterstitial()
        {
            while (gameObject.activeSelf == true)
            {
                yield return new WaitForSeconds(SecondsDelayToShowInterstitialAd);
                ShowPopupInterstitialAd();
            }
        }
    }
}
