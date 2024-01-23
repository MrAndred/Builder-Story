using System;
using UnityEngine;

namespace BuilderStory
{
    public class PlayerRenderer : MonoBehaviour
    {
        [SerializeField] private ContractRenderer _contractRenderer;
        [SerializeField] private ContractInfoRenderer _contractInfoRenderer;

        private Structure _structure;

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

        public void Show(Structure structure)
        {
            _contractInfoRenderer.Show();
            _structure = structure;
        }

        public void Hide()
        {
            _contractInfoRenderer.Hide();
        }

        private void InfoClicked()
        {
            _contractInfoRenderer.Hide();
            _contractRenderer.Show(_structure);
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
