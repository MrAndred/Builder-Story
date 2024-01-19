using UnityEngine;
using Agava.YandexGames;
using UnityEngine.SceneManagement;
using System.Collections;

namespace BuilderStory
{
    public class GameRoot : MonoBehaviour
    {
        private string _lastLevelName = "Test Level 1";

        private IEnumerator Start()
        {
            yield return YandexGamesSdk.Initialize();

            SceneManager.LoadScene(_lastLevelName);
        }
    }
}
