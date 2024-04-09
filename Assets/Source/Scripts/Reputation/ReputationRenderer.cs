using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BuilderStory.ReputationSystem
{
    public class ReputationRenderer : MonoBehaviour
    {
        private const float FillingDuration = 0.5f;

        [SerializeField] private Slider _reputationSlider;
        [SerializeField] private TMP_Text _levelNumber;

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

        public void Init(Reputation levelReputation, float minLevel, int level)
        {
            _reputationSlider.value = minLevel;
            _reputationSlider.minValue = minLevel;
            _reputationSlider.maxValue = levelReputation.Max + minLevel;
            _levelReputation = levelReputation;

            _levelReputation.ReputationChanged += Render;

            _isInitialized = true;

            _levelNumber.text = level.ToString();
        }

        public void Render()
        {
            _filling?.Kill();
            _filling = _reputationSlider
                .DOValue(_levelReputation.Current, FillingDuration)
                .SetEase(Ease.Linear);
        }
    }
}
