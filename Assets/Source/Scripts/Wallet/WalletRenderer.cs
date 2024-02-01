using TMPro;
using UnityEngine;

namespace BuilderStory
{
    public class WalletRenderer : MonoBehaviour
    {
        [SerializeField] private TMP_Text _money;

        private Wallet _wallet;
        private bool _isInitialized;

        private void OnEnable()
        {
            if (_wallet != null)
            {
                _wallet.MoneyChanged += Render;
            }

        }

        private void OnDisable()
        {
            _wallet.MoneyChanged -= Render;
        }

        public void Init(Wallet wallet)
        {
            _wallet = wallet;

            _wallet.MoneyChanged += Render;
            Render();
        }

        public void Render()
        {
            _money.text = _wallet.Money.ToString();
        }
    }
}
