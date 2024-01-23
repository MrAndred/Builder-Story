using Agava.YandexGames;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace BuilderStory
{
    public class GameRoot : MonoBehaviour
    {
        private string _lastLevelName = "Test Level 1";

        [SerializeField] private Slider _loader;
        [SerializeField] private Lean.Localization.LeanLocalization _localization;

        private IEnumerator Start()
        {
            yield return YandexGamesSdk.Initialize();

            Lean.Localization.LeanLocalization.SetCurrentLanguageAll(LocalizationUtil.Languages[YandexGamesSdk.Environment.i18n.lang]);
            
            yield return StartCoroutine(LoadSceneWithProgressBar(_lastLevelName));
        }

        private IEnumerator LoadSceneWithProgressBar(string sceneName)
        {
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);

            _loader.value = 0;

            while (asyncOperation.isDone == false)
            {
                _loader.value = asyncOperation.progress;

                yield return null;
            }
        }
    }
}
