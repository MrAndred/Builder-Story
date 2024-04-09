using Agava.WebUtility;
using BuilderStory.Audio;
using UnityEngine;

namespace BuilderStory.Pause
{
    public class PauseSystem : MonoBehaviour
    {
        private AudioManager _audioManager;

        public PauseSystem(AudioManager audioManager)
        {
            _audioManager = audioManager;
        }

        public bool IsFocusPaused { get; private set; } = false;

        public bool IsAdPaused { get; private set; } = false;

        private void OnEnable()
        {
            WebApplication.InBackgroundChangeEvent += OnBackgroundChange;
        }

        private void OnDisable()
        {
            WebApplication.InBackgroundChangeEvent -= OnBackgroundChange;
        }

        private void OnApplicationFocus(bool focus)
        {
            if (focus == false && IsFocusPaused == false && IsAdPaused == false)
            {
                FocusPauseGame();
            }
            else if (focus && IsFocusPaused && IsAdPaused == false)
            {
                FocusResumeGame();
            }
        }

        public void FocusPauseGame()
        {
            if (IsFocusPaused)
            {
                return;
            }

            PauseGame();
            IsFocusPaused = true;
        }

        public void AdPauseGame()
        {
            if (IsAdPaused)
            {
                return;
            }

            PauseGame();
            IsAdPaused = true;
        }

        public void AdResumeGame()
        {
            if (IsAdPaused == false)
            {
                return;
            }

            ResumeGame();
            IsAdPaused = false;
        }

        public void FocusResumeGame()
        {
            if (IsFocusPaused == false)
            {
                return;
            }

            ResumeGame();
            IsFocusPaused = false;
        }

        private void OnBackgroundChange(bool isInBackground)
        {
            if (isInBackground && IsFocusPaused == false && IsAdPaused == false)
            {
                FocusPauseGame();
            }
            else if (isInBackground == false && IsFocusPaused && IsAdPaused == false)
            {
                FocusResumeGame();
            }
        }

        private void PauseGame()
        {
            Time.timeScale = 0f;
            _audioManager.PauseAll();
        }

        private void ResumeGame()
        {
            Time.timeScale = 1;
            _audioManager.ResumeAll();
        }
    }
}
