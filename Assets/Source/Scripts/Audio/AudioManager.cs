using UnityEngine;

namespace BuilderStory
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance { get; private set; }

        private AudioSource musicSource;
        private AudioSource sfxSource;

        public bool IsMuted => musicSource.mute;

        public void Init()
        {
            bool muted = PlayerPrefs.GetInt("Muted", 0) == 1;

            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }

            if (musicSource == null)
            {
                musicSource = gameObject.AddComponent<AudioSource>();
            }

            musicSource.loop = true;
            musicSource.volume = 0.35f;

            if (sfxSource == null)
            {
                sfxSource = gameObject.AddComponent<AudioSource>();
            }

            sfxSource.loop = false;

            sfxSource.mute = muted;
            musicSource.mute = muted;
        }

        public void PlayMusic(AudioClip musicClip)
        {
            musicSource.clip = musicClip;
            musicSource.Play();
        }

        public void PlaySFX(AudioClip sfxClip)
        {
            sfxSource.PlayOneShot(sfxClip);
        }

        public void ToggleMute()
        {
            bool newMute = !IsMuted;

            musicSource.mute = newMute;
            sfxSource.mute = newMute;

            PlayerPrefs.SetInt("Muted", newMute ? 1 : 0);
        }

        public void PauseAll()
        {
            musicSource.Pause();
            sfxSource.Pause();
        }

        public void ResumeAll()
        {
            musicSource.UnPause();
            sfxSource.UnPause();
        }
    }
}