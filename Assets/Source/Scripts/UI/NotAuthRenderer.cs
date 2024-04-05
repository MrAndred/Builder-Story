using UnityEngine;
using UnityEngine.UI;

namespace BuilderStory
{
    public class NotAuthRenderer : MonoBehaviour
    {
        [SerializeField] private Button _authButton;

        private void OnEnable()
        {
            _authButton.onClick.AddListener(OnAuthButtonClicked);
        }

        private void OnDisable()
        {
            _authButton.onClick.RemoveListener(OnAuthButtonClicked);
        }

        private void OnAuthButtonClicked()
        {
            Agava.YandexGames.PlayerAccount.Authorize();
        }
    }
}
