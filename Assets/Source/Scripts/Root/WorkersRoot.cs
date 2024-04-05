using System.Collections.Generic;
using UnityEngine;

namespace BuilderStory
{
    public class WorkersRoot : MonoBehaviour
    {
        private const int DefaultWorkersCount = 5;

        [SerializeField] private WorkerSkinPerLevel _workerTemplates;

        private ObjectPool<Worker> _workerPool;
        private List<Worker> _workers = new List<Worker>();

        private InventoryObserver _inventoryObserver;
        private Structure[] _structures;
        private Navigator _navigator;
        private ProgressSaves _saveObject;

        private void OnDisable()
        {
            foreach (var worker in _workers)
            {
                _inventoryObserver.Unsubscribe(worker.Lift);
            }
        }

        private void OnEnable()
        {
            if (_inventoryObserver != null || _workers.Count == 0)
            {
                foreach (var worker in _workers)
                {
                    _inventoryObserver.Subscribe(worker.Lift);
                }
            }
        }

        public void Init(
            int level,
            ProgressSaves saves,
            InventoryObserver inventoryObserver,
            Structure[] structures,
            Navigator navigator
            )
        {
            _inventoryObserver = inventoryObserver;
            _structures = structures;
            _navigator = navigator;
            _saveObject = saves;

            _workerPool = new ObjectPool<Worker>(_workerTemplates.GetSkin(level), DefaultWorkersCount, gameObject.transform);

            for (int i = 0; i < _saveObject.WorkersCount; i++)
            {
                var worker = _workerPool.GetAvailable();
                worker.gameObject.SetActive(true);
                worker.Init(_structures, _navigator, _saveObject);

                _workers.Add(worker);
                _inventoryObserver.Subscribe(worker.Lift);
            }

            saves.WorkersCountChanged += OnWorkersCountChanged;
        }

        private void OnWorkersCountChanged(int count)
        {
            for (int i = 0; i < count - _workerPool.ActiveCount; i++)
            {
                var worker = _workerPool.GetAvailable();
                worker.Init(_structures, _navigator, _saveObject);
                worker.gameObject.SetActive(true);

                _inventoryObserver.Subscribe(worker.Lift);
            }
        }
    }
}
