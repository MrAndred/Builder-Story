using System.Collections.Generic;
using UnityEngine;

namespace BuilderStory
{
    public class ObjectPool<T> where T : MonoBehaviour
    {
        private T _prefab;
        private int _poolSize;
        private Transform _parent;

        private List<T> _pool;

        public IReadOnlyCollection<T> Pool => _pool;

        public ObjectPool(T prefab, int poolSize, Transform parent)
        {
            _prefab = prefab;
            _poolSize = poolSize;
            _parent = parent;
            _pool = InitializePool();
        }

        public ObjectPool(T[] prefabs, int poolSize, Transform parent)
        {
            _poolSize = poolSize;
            _parent = parent;
            _pool = InitializePool(prefabs);
        }

        public T GetAvailable()
        {
            foreach (var instance in _pool)
            {
                if (!instance.gameObject.activeSelf)
                {
                    return instance;
                }
            }

            var newInstance = GameObject.Instantiate(_prefab, _parent);
            _pool.Add(newInstance);
            return newInstance;
        }

        public void Reset()
        {
            foreach (var instance in _pool)
            {
                instance.gameObject.SetActive(false);
            }
        }

        private List<T> InitializePool()
        {
            var pool = new List<T>();

            for (int i = 0; i < _poolSize; i++)
            {
                var instance = GameObject.Instantiate(_prefab, _parent);
                pool.Add(instance);
                instance.gameObject.SetActive(false);
            }

            return pool;
        }

        private List<T> InitializePool(T[] prefabs)
        {
            var pool = new List<T>();

            for (int i = 0; i < _poolSize; i++)
            {
                int preabIndex = Random.Range(0, prefabs.Length);

                var instance = GameObject.Instantiate(prefabs[preabIndex], _parent);
                pool.Add(instance);
                instance.gameObject.SetActive(false);
            }

            return pool;
        }
    }
}