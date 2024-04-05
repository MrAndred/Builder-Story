using Lean.Localization;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BuilderStory
{
    public class UpgradeContainerRenderer : MonoBehaviour
    {
        private const string _levelNumberTranslationName = "level_number";
        private const string _maxTranslationName = "max";

        [SerializeField] private TMP_Text _lvlNumber;

        [SerializeField] private TMP_Text _cost;
        [SerializeField] private Button _upgradeButton;

        public event Action<int> OnButtonClicked;

        private int Cost => int.Parse(_cost.text);

        private void OnEnable()
        {
            _upgradeButton.onClick.AddListener(OnButtonClickedHandler);
        }

        private void OnDisable()
        {
            _upgradeButton.onClick.RemoveListener(OnButtonClickedHandler);
        }

        public void Render(int lvl, int cost)
        {
            string lvlText = LeanLocalization.GetTranslationText(_levelNumberTranslationName);

            _lvlNumber.text = string.Format(lvlText, lvl);

            if (cost < 0)
            {
                _cost.text = LeanLocalization.GetTranslationText(_maxTranslationName);
                SetNotInteractable();
            }
            else
            {
                _cost.text = cost.ToString();
            }
        }

        public void SetInteractable()
        {
            _upgradeButton.interactable = true;
        }

        public void SetNotInteractable()
        {
            _upgradeButton.interactable = false;
        }


        private void OnButtonClickedHandler()
        {
            OnButtonClicked?.Invoke(Cost);
        }
    }
}
