using System.Collections.Generic;
using UnityEngine;

namespace NF.ObjectPooling.Runtime
{
    public static class PoolManager
    {
        private const int INITIAL_PREFAB_POOL_SIZE = 128;
        private const int INITIAL_INSTANCE_POOL_SIZE = 512;
    
        private static readonly Dictionary<GameObject, Pool> PrefabPools = new Dictionary<GameObject, Pool>(INITIAL_PREFAB_POOL_SIZE);
        private static readonly Dictionary<GameObject, Pool> InstancesPools = new Dictionary<GameObject, Pool>(INITIAL_INSTANCE_POOL_SIZE);
        private static readonly Transform PoolParent;

        static PoolManager()
        {
            GameObject poolContainer = new GameObject("Pool Container");
            Object.DontDestroyOnLoad(poolContainer);
            PoolParent = poolContainer.transform;
        } 
        
        public static Pool GetPrefabPool(GameObject prefab)
        {
            if (PrefabPools.TryGetValue(prefab, out Pool pool))
                return pool;

            pool = new Pool(prefab, PoolParent, OnInstanceCreated);
            PrefabPools.Add(prefab, pool);
            return pool;
        }

        public static bool TryGetInstancePool(GameObject instance, out Pool pool) =>
            InstancesPools.TryGetValue(instance, out pool);

        private static void OnInstanceCreated(GameObject instance, Pool pool) => InstancesPools.Add(instance, pool);
    }
}