using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BuilderStory
{
    public class ReputationRenderer : MonoBehaviour
    {
        [SerializeField] private Slider _reputationSlider;

        private Reputation _levelReputation;
        private bool _isInitialized = false;

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
            _reputationSlider.value = _levelReputation.Current;
        }
    }
}
