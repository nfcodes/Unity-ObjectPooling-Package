using System;
using System.Collections.Generic;
using UnityEngine;

namespace NF.ObjectPooling.Runtime
{
    public static class PoolManager
    {
        public const int INITIAL_POOL_SIZE = 64;

        private static readonly Dictionary<Type, Pool> Pools = new Dictionary<Type, Pool>(INITIAL_POOL_SIZE);
        private static readonly Transform PoolParent;

        static PoolManager()
        {
            GameObject poolContainer = new GameObject("Pool Container");
            UnityEngine.Object.DontDestroyOnLoad(poolContainer);
            PoolParent = poolContainer.transform;
        } 
        
        public static Pool GetPool(GameObject prefab)
        {
            Type type = typeof(GameObject);
            if (Pools.TryGetValue(type, out Pool pool))
                return pool;

            pool = new Pool(prefab, PoolParent);
            Pools.Add(type, pool);
            return pool;
        }
    }
}