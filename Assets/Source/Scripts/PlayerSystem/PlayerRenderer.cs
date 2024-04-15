using System;
using BuilderStory.Builder;
using BuilderStory.Struct;
using UnityEngine;

namespace BuilderStory.PlayerSystem
{
    public class PlayerRenderer : MonoBehaviour
    {
        [SerializeField] private ContractRenderer _contractRenderer;
        [SerializeField] private ContractInfoRenderer _contractInfoRenderer;

        private Structure _structure;

        public event Action ContractAccepted;

        private void OnEnable()
        {
            _contractInfoRenderer.InfoClicked += OnInfoClicked;
            _contractRenderer.ClickedClose += OnContractClosed;
            _contractRenderer.ClickedBuild += OnContractAccepted;
        }

        private void OnDisable()
        {
            _contractInfoRenderer.InfoClicked -= OnInfoClicked;
            _contractRenderer.ClickedClose -= OnContractClosed;
            _contractRenderer.ClickedBuild -= OnContractAccepted;
        }

        public void Show(Structure structure)
        {
            _contractInfoRenderer.Show();
            _structure = structure;
        }

        public void HideContract()
        {
            _contractInfoRenderer.Hide();
        }

        public void HideAll()
        {
            _contractInfoRenderer.Hide();
            _contractRenderer.Hide();
        }

        private void OnInfoClicked()
        {
            _contractInfoRenderer.Hide();
            _contractRenderer.Show(_structure);
        }

        private void OnContractClosed()
        {
            _contractInfoRenderer.Show();
            _contractRenderer.Hide();
        }

        private void OnContractAccepted()
        {
            ContractAccepted?.Invoke();

            _contractInfoRenderer.Hide();
            _contractRenderer.Hide();
        }
    }
}
