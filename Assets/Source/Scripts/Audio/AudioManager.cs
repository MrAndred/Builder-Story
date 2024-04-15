using UnityEngine;

namespace BuilderStory.Audio
{
    public class AudioManager : MonoBehaviour
    {
        private const string Muted = "Muted";
        private const int MuteOff = 0;
        private const int MuteOn = 1;
        private const float MusicVolume = 0.35f;

        private AudioSource _musicSource;

        private AudioSource _sfxSource;

        public bool IsMuted => _musicSource.mute;

        public void Init()
        {
            bool muted = PlayerPrefs.GetInt(Muted, MuteOff) == MuteOn;

            if (_musicSource == null)
            {
                _musicSource = gameObject.AddComponent<AudioSource>();
            }

            _musicSource.loop = true;
            _musicSource.volume = MusicVolume;

            if (_sfxSource == null)
            {
                _sfxSource = gameObject.AddComponent<AudioSource>();
            }

            _sfxSource.loop = false;

            _sfxSource.mute = muted;
            _musicSource.mute = muted;
        }

        public void PlayMusic(AudioClip musicClip)
        {
            _musicSource.clip = musicClip;
            _musicSource.Play();
        }

        public void PlaySFX(AudioClip sfxClip)
        {
            _sfxSource.PlayOneShot(sfxClip);
        }

        public void ToggleMute()
        {
            bool newMute = !IsMuted;

            _musicSource.mute = newMute;
            _sfxSource.mute = newMute;

            PlayerPrefs.SetInt(Muted, newMute ? MuteOn : MuteOff);
        }

        public void PauseAll()
        {
            _musicSource.Pause();
            _sfxSource.Pause();
        }

        public void ResumeAll()
        {
            _musicSource.UnPause();
            _sfxSource.UnPause();
        }
    }
}