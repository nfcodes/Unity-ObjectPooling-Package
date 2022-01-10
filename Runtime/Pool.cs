using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace NF.ObjectPooling.Runtime
{
    public sealed class Pool
    {
        private const int INITIAL_INSTANCES_SIZE = 128;
        private readonly Action<GameObject, Pool> _onInstanceCreated;
        
        private readonly Stack<GameObject> _instances;
        private readonly GameObject _prefab;
        private readonly Transform _poolParent;
        private readonly Vector3 _initialPosition = default;
        private readonly Quaternion _initialRotation = default;
        private readonly Vector3 _initialScale = default;

        public Pool(GameObject prefab, Transform poolParent, Action<GameObject, Pool> onInstanceCreated)
        {
            _instances = new Stack<GameObject>(INITIAL_INSTANCES_SIZE);
            
            _prefab = prefab;
            _poolParent = poolParent;
            _onInstanceCreated = onInstanceCreated;
            _initialPosition = prefab.transform.position;
            _initialRotation = prefab.transform.rotation;
            _initialScale = prefab.transform.localScale;
        }

        public void Preload(int count)
        {
            for (int i = 0; i < count; i++)
            {
                GameObject instance = PopulatePrefab();
                instance.transform.SetParent(_poolParent);
                _instances.Push(instance);
            }
        }

        private GameObject PopulatePrefab()
        {
            GameObject instance = Object.Instantiate(_prefab);
            _onInstanceCreated(instance, this);
            instance.SetActive(false);
            return instance;
        }

        public GameObject Get() => Get(null);
        public GameObject Get(Vector3 position) => Get(null, position);
        public GameObject Get(Vector3 position, Quaternion rotation) => Get(null, position, rotation);
        public GameObject Get(Transform parent) => Get(parent, _initialPosition);
        public GameObject Get(Transform parent, Vector3 position) => Get(parent, position, _initialRotation);
        
        public GameObject Get(Transform parent, Vector3 position, Quaternion rotation)
        {
            GameObject instance = GetFromPool();
            InitializeInstance(instance, parent, position, rotation);
            return instance;
        }

        private void InitializeInstance(GameObject instance, Transform parent, Vector3 position, Quaternion rotation)
        {
            Transform transformCache = instance.transform;
            transformCache.SetPositionAndRotation(position, rotation);
            transformCache.SetParent(parent);
            instance.SetActive(true);

            if (parent == null)
                MoveParentlessInstanceToActiveScene(instance);

            if (instance.TryGetComponent(out IReusablePrefab reusable))
                reusable.OnGet();
        }

        private void MoveParentlessInstanceToActiveScene(GameObject instance)
        {
            Scene activeScene = SceneManager.GetActiveScene();
            SceneManager.MoveGameObjectToScene(instance, activeScene);
        }

        private GameObject GetFromPool()
        {
            int count = _instances.Count;
            
            if (count > 0)
            {
                while (count > 0)
                {
                    GameObject instance = _instances.Pop();
                    
                    if (instance != null)
                        return instance;
                    
                    count--;
                }
            }
            
            Preload(1);
            return _instances.Pop();
        }

        public void Return(GameObject instance)
        {
            if (instance.TryGetComponent(out IReusablePrefab reusable))
                reusable.OnRelease();
            
            instance.SetActive(false);
            
            Transform transformCache = instance.transform;
            transformCache.SetPositionAndRotation(Vector3.zero, _initialRotation);
            transformCache.localScale = _initialScale;
            transformCache.SetParent(_poolParent);
            _instances.Push(instance);
        }
    }
}