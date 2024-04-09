using System.Collections;
using Agava.YandexGames;
using Lean.Localization;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace BuilderStory.Leaderbord
{
    public class LBPlayerContainer : MonoBehaviour
    {
        private const string AnonymousTranslation = "anonymous";

        [SerializeField] private Image _avatar;
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _score;
        [SerializeField] private TMP_Text _rank;

        [SerializeField] private Sprite _defaultAvatar;

        public void Render(LeaderboardEntryResponse entry)
        {
            string anonymous = LeanLocalization.GetTranslationText(AnonymousTranslation);

            _name.text = string.IsNullOrEmpty(entry.player.publicName)
                ? anonymous : entry.player.publicName;

            _rank.text = entry.rank.ToString();
            _score.text = entry.score.ToString();

            SetImage(entry.player.profilePicture);
        }

        private void SetImage(string profilePicture)
        {
            if (profilePicture != null)
            {
                StartCoroutine(DownloadImageCoroutine(profilePicture));
            }
            else
            {
                _avatar.sprite = _defaultAvatar;
            }
        }

        private IEnumerator DownloadImageCoroutine(string profilePictureUrl)
        {
            using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(profilePictureUrl))
            {
                yield return uwr.SendWebRequest();

                if (uwr.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError("Ошибка загрузки изображения: " + uwr.error);
                }
                else
                {
                    Texture2D texture = DownloadHandlerTexture.GetContent(uwr);
                    _avatar.sprite = Sprite.Create(
                        texture,
                        new Rect(0.0f, 0.0f, texture.width, texture.height),
                        new Vector2(0.5f, 0.5f));
                }
            }
        }
    }
}
