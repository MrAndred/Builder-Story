using Agava.YandexGames;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace BuilderStory
{
    public class GameRoot : MonoBehaviour
    {
        private const string _percentTemplate = "{0}%";
        private const float _loadingSpeed = 1.0f;
        private const float _progressThreshold = 0.85f;

        private string _lastLevelName = "Level 1";

        [SerializeField] private Slider _loader;
        [SerializeField] private Lean.Localization.LeanLocalization _localization;
        [SerializeField] private TMP_Text _percentText;

        private IEnumerator Start()
        {
#if UNITY_EDITOR == false

            yield return YandexGamesSdk.Initialize();

            Lean.Localization.LeanLocalization.SetCurrentLanguageAll(
                LocalizationUtil.Languages[YandexGamesSdk.Environment.i18n.lang]);
#endif
            yield return StartCoroutine(LoadSceneWithProgressBar(_lastLevelName));
        }

        private IEnumerator LoadSceneWithProgressBar(string sceneName)
        {
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);
            asyncOperation.allowSceneActivation = false;

            _loader.value = 0;

            while (!asyncOperation.isDone)
            {
                float progress = asyncOperation.progress < _progressThreshold ? asyncOperation.progress : 1f;
                _loader.value = Mathf.Lerp(_loader.value, progress, _loadingSpeed * Time.deltaTime);

                string percent = string.Format(_percentTemplate, (int)(_loader.value * 100));
                _percentText.text = percent;

                if (_loader.value >= _progressThreshold)
                {
                    asyncOperation.allowSceneActivation = true;
                }

                yield return null;
            }
        }
    }
}
