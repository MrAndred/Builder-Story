using System.Collections;
using BuilderStory.ReputationSystem;
using BuilderStory.Struct;
using BuilderStory.WalletSystem;
using UnityEngine;

namespace BuilderStory.Builder
{
    public class BuildContract
    {
        private const float SendWorkerInterval = 1.5f;

        private Structure _structure;

        private Wallet _wallet;
        private Reputation _reputation;

        private Coroutine _building;

        public BuildContract(Structure structure, Wallet wallet, Reputation reputation)
        {
            _structure = structure;
            _wallet = wallet;
            _reputation = reputation;
        }

        public void Build()
        {
            if (_building != null)
            {
                _structure.StopCoroutine(BuildCoroutine());
            }

            _building = _structure.StartCoroutine(BuildCoroutine());
        }

        private IEnumerator BuildCoroutine()
        {
            var delay = new WaitForSeconds(SendWorkerInterval);
            _structure.StartBuild(_wallet, _reputation);
            yield return delay;
        }
    }
}
