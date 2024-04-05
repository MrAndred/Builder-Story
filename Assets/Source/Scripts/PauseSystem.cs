using Agava.WebUtility;
using UnityEngine;

namespace BuilderStory
{
    public class PauseSystem : MonoBehaviour
    {
        public static PauseSystem Instance { get; private set; }

        public bool IsFocusPaused { get; private set; } = false;

        public bool IsAdPaused { get; private set; } = false;

        private void OnEnable()
        {
            Instance = this;

            WebApplication.InBackgroundChangeEvent += OnBackgroundChange;
        }

        private void OnDisable()
        {
            Instance = null;

            WebApplication.InBackgroundChangeEvent -= OnBackgroundChange;
        }

        private void OnApplicationFocus(bool focus)
        {
            if (focus == false && IsFocusPaused == false && IsAdPaused == false)
            {
                FocusPauseGame();
            }
            else if (focus == true && IsFocusPaused == true && IsAdPaused == false)
            {
                FocusResumeGame();
            }
        }

        public void FocusPauseGame()
        {
            if (IsFocusPaused == true) return;

            PauseGame(); 
            IsFocusPaused = true;
        }

        public void AdPauseGame()
        {
            if (IsAdPaused == true) return;

            PauseGame();
            IsAdPaused = true;
        }

        public void AdResumeGame()
        {
            if (IsAdPaused == false) return;

            ResumeGame();
            IsAdPaused = false;
        }

        public void FocusResumeGame()
        {
            if (IsFocusPaused == false) return;

            ResumeGame();
            IsFocusPaused = false;
        }

        private void OnBackgroundChange(bool isInBackground)
        {
            if (isInBackground == true && IsFocusPaused == false && IsAdPaused == false)
            {
                FocusPauseGame();
            }
            else if (isInBackground == false && IsFocusPaused == true && IsAdPaused == false)
            {
                FocusResumeGame();
            }
        }

        private void PauseGame()
        {
            Time.timeScale = 0f;
            AudioManager.Instance.PauseAll();
        }

        private void ResumeGame()
        {
            Time.timeScale = 1;
            AudioManager.Instance.ResumeAll();
        }
    }
}
