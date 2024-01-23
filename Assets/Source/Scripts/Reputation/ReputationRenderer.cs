using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace BuilderStory
{
    public class ReputationRenderer : MonoBehaviour
    {
        private const float FillingDuration = 0.5f;

        [SerializeField] private Slider _reputationSlider;

        private Reputation _levelReputation;
        private bool _isInitialized = false;
        private Tweener _filling;

        private void OnEnable()
        {
            if (_isInitialized == false)
            {
                return;
            }

            _levelReputation.ReputationChanged += Render;
        }

        private void OnDisable()
        {
            _levelReputation.ReputationChanged -= Render;
        }

        public void Init(Reputation levelReputation)
        {
            _reputationSlider.value = 0;
            _reputationSlider.minValue = 0;
            _reputationSlider.maxValue = levelReputation.Max;
            _levelReputation = levelReputation;

            _levelReputation.ReputationChanged += Render;

            _isInitialized = true;
        }

        public void Render()
        {
            _filling?.Kill();
            _filling = _reputationSlider.DOValue(_levelReputation.Current, FillingDuration).SetEase(Ease.Linear);
        }
    }
}
