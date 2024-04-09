using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace BuilderStory.UI
{
    public class BoosterRenderer : MonoBehaviour
    {
        private const float HideDelay = 30;
        private const float Duration = 1.5f;

        [SerializeField] private RectTransform _container;
        [SerializeField] private Button _button;

        [SerializeField] private float _startXPosition;
        [SerializeField] private float _endXPosition;

        private bool _isShowing = false;

        public event Action<BoosterRenderer> OnClick;

        private void OnEnable()
        {
            _button.onClick.AddListener(OnClickHandler);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnClickHandler);
        }

        public void Show()
        {
            if (_isShowing)
            {
                return;
            }

            _isShowing = true;
            gameObject.SetActive(true);
            _container.DOAnchorPosX(_endXPosition, Duration);

            StartCoroutine(DelayHide());
        }

        public void Hide()
        {
            if (_isShowing == false)
            {
                return;
            }

            _isShowing = false;
            _container
                .DOAnchorPosX(_startXPosition, Duration)
                .OnComplete(() => gameObject.SetActive(false));
        }

        private IEnumerator DelayHide()
        {
            yield return new WaitForSeconds(HideDelay);
            Hide();
        }

        private void OnClickHandler()
        {
            OnClick?.Invoke(this);
        }
    }
}
