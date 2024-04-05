using UnityEngine;
using UnityEngine.UI;

namespace BuilderStory
{
    public class MusicToggler : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Image _image;
        [SerializeField] private Sprite _onSprite;
        [SerializeField] private Sprite _offSprite;

        private void OnEnable()
        {
            _button.onClick.AddListener(OnMusicHandlerClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnMusicHandlerClick);
        }

        public void Init()
        {
            Render();
        }

        private void OnMusicHandlerClick()
        {
            AudioManager.Instance.ToggleMute();

            Render();
        }
        private void Render()
        {
            bool isMuted = AudioManager.Instance.IsMuted;

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
