using System.Collections;
using Agava.YandexGames;
using BuilderStory.Loader;
using BuilderStory.Pool;
using BuilderStory.UI;
using DG.Tweening;
using Lean.Localization;
using UnityEngine;
using UnityEngine.UI;

namespace BuilderStory.Leaderbord
{
    public class LeaderbordRenderer : MonoBehaviour
    {
        private const string LeaderbordName = "reputatuion";
        private const float OpenDuration = 0.35f;
        private const float CloseDuration = 0.35f;
        private const int PlayersCount = 10;

        [SerializeField] private CanvasGroup _leaderbordView;

        [SerializeField] private Button _openView;
        [SerializeField] private Button[] _closeButtons;

        [SerializeField] private RectTransform _emptyTip;
        [SerializeField] private NotAuthRenderer _notAuthorizedTip;
        [SerializeField] private LoadRenderer _loadingTip;

        [SerializeField] private RectTransform _content;
        [SerializeField] private LBPlayerContainer _playerContainerPrefab;

        private Tweener _openTweener;
        private Tweener _closeTweener;

        private ObjectPool<LBPlayerContainer> _playerContainerPool;

        private void OnEnable()
        {
            foreach (var button in _closeButtons)
            {
                button.onClick.AddListener(OnCloseCanvasButtonClicked);
            }
        }

        private void OnDisable()
        {
            foreach (var button in _closeButtons)
            {
                button.onClick.RemoveListener(OnCloseCanvasButtonClicked);
            }
        }

        private void OnDestroy()
        {
            _openView.onClick.RemoveListener(OnOpenButtonClicked);
        }

        public void Init()
        {
            _playerContainerPool = new ObjectPool<LBPlayerContainer>(
                _playerContainerPrefab,
                PlayersCount,
                _content);

            _openView.onClick.AddListener(OnOpenButtonClicked);

            _emptyTip.gameObject.SetActive(false);
            _loadingTip.gameObject.SetActive(false);

            _leaderbordView.gameObject.SetActive(false);
            _leaderbordView.transform.localScale = Vector3.zero;
        }

        private void OnOpenButtonClicked()
        {
            _leaderbordView.gameObject.SetActive(true);
            _loadingTip.gameObject.SetActive(true);

            GetLeaderbordData();

            _openTweener?.Kill();
            _openTweener = _leaderbordView.transform
                .DOScale(Vector3.one, OpenDuration)
                .SetEase(Ease.OutBack);

            _openView.gameObject.SetActive(false);
        }

        private void OnCloseCanvasButtonClicked()
        {
            _closeTweener?.Kill();

            _closeTweener = _leaderbordView.transform
                .DOScale(Vector3.zero, CloseDuration)
                .SetEase(Ease.InBack)
                .OnComplete(() =>
                    {
                        _leaderbordView.gameObject.SetActive(false);
                        _emptyTip.gameObject.SetActive(false);
                    });

            _openView.gameObject.SetActive(true);
        }

        private void GetLeaderbordData()
        {
           _playerContainerPool.Reset();

#if UNITY_EDITOR

           StartCoroutine(SimulateGetData());

           return;
#else
           if (PlayerAccount.IsAuthorized == false)
           {
               _emptyTip.gameObject.SetActive(false);
               _loadingTip.gameObject.SetActive(false);
               _notAuthorizedTip.gameObject.SetActive(true);
               return;
           }

           Leaderboard.GetEntries(LeaderbordName, (result) =>
           {
              int length = result.entries.Length > PlayersCount ? PlayersCount : result.entries.Length;

              if (length == 0)
              {
                  _emptyTip.gameObject.SetActive(true);
                  _loadingTip.gameObject.SetActive(false);
                  return;
              }

              for (int i = 0; i < length; i++)
              {
                  var entry = result.entries[i];
                  if (entry.score == 0)
                  {
                      continue;
                  }

                  var container = _playerContainerPool.GetAvailable();
                  container.gameObject.SetActive(true);
                  container.Render(entry);
              }

              _loadingTip.gameObject.SetActive(false);
           });
#endif
        }

#if UNITY_EDITOR
        private IEnumerator SimulateGetData()
        {
            float delay = 1f;

            yield return new WaitForSeconds(delay);

            _loadingTip.gameObject.SetActive(false);
            _emptyTip.gameObject.SetActive(true);
        }
#endif
    }
}
