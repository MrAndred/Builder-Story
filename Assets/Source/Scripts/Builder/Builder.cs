using System.Collections;
using UnityEngine;

namespace BuilderStory
{
    public class Builder : MonoBehaviour
    {
        private const float _sendWorkerInterval = 1.5f;

        private Structure _structure;
        private Navigator _navigator;
        private Worker[] _workers;

        private Coroutine _building;

        public void Init(Structure structure, Worker[] workers, Navigator navigator)
        {
            _workers = workers;
            _structure = structure;
            _navigator = navigator;

            Build();
        }

        public void Build()
        {
            if (_building != null)
            {
                StopCoroutine(_building);
            }

            _building = StartCoroutine(BuildCoroutine());
        }

        private IEnumerator BuildCoroutine()
        {
            var delay = new WaitForSeconds(_sendWorkerInterval);

            while (_structure.IsBuilt() == false)
            {
                if (
                    TryGetFreeWorker(out Worker worker) == false
                    || _structure.TryGetBuildMaterial(out BuildMaterial material) == false
                    || _navigator.TryGetMaterialPosition(material, out Transform warehousePoint) == false
                )
                {
                    yield return delay;
                    continue;
                }

                var trashPoint = _navigator.GetRandomTrashPoint();

                worker.InstallMaterial(_structure.transform, warehousePoint, trashPoint);
                yield return delay;
            }
        }

        private bool TryGetFreeWorker(out Worker result)
        {
            foreach (var worker in _workers)
            {
                if (worker.IsBusy == false)
                {
                    result = worker;
                    return true;
                }
            }

            result = null;
            return result != null;
        }
    }
}
