using UnityEngine;

namespace NF.ObjectPooling.Runtime
{
    [System.Serializable]
    public struct PoolPreloadable
    {
        public GameObject PrefabToLoad;
        public int Quantity;
    }
}