using UnityEngine;
using UnityEngine.UI;

namespace BuilderStory.Audio
{
    public class MusicToggler : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Image _image;
        [SerializeField] private Sprite _onSprite;
        [SerializeField] private Sprite _offSprite;

        private AudioManager _audioManager;

        private void OnEnable()
        {
            _button.onClick.AddListener(OnMusicHandlerClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnMusicHandlerClick);
        }

        public void Init(AudioManager audioManager)
        {
            _audioManager = audioManager;

            Render();
        }

        private void OnMusicHandlerClick()
        {
            _audioManager.ToggleMute();

            Render();
        }

        private void Render()
        {
            bool isMuted = _audioManager.IsMuted;

            if (isMuted)
            {
                _image.sprite = _offSprite;
            }
            else
            {
                _image.sprite = _onSprite;
            }
        }
    }
}
