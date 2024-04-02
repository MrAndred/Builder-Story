using System.Collections;
using UnityEngine;

namespace BuilderStory
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

            Build();
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
