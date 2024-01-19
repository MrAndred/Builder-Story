using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace BuilderStory
{
    public class PlayerRenderer : MonoBehaviour
    {
        [SerializeField] private ContractRenderer _contractRenderer;
        [SerializeField] private ContractInfoRenderer _contractInfoRenderer;

        public event Action OnContractAccepted;

        private void OnEnable()
        {
            _contractInfoRenderer.OnInfoClicked += InfoClicked;
            _contractRenderer.OnCloseClicked += ContractClosed;
            _contractRenderer.OnBuildClicked += ContractAccepted;
        }

        private void OnDisable()
        {
            _contractInfoRenderer.OnInfoClicked -= InfoClicked;
            _contractRenderer.OnCloseClicked -= ContractClosed;
            _contractRenderer.OnBuildClicked -= ContractAccepted;
        }

        public void Show(IBuildable buildable)
        {
            _contractInfoRenderer.Show();
        }

        public void Hide()
        {
            _contractInfoRenderer.Hide();
        }

        private void InfoClicked()
        {
            _contractInfoRenderer.Hide();
            _contractRenderer.Show();
        }

        private void ContractClosed()
        {
            _contractInfoRenderer.Show();
            _contractRenderer.Hide();
        }

        private void ContractAccepted()
        {
            OnContractAccepted?.Invoke();

            _contractInfoRenderer.Hide();
            _contractRenderer.Hide();
        }
    }
}
