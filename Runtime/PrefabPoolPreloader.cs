using UnityEngine;

namespace NF.ObjectPooling.Runtime
{
    public class PrefabPoolPreloader : MonoBehaviour
    {
        [SerializeField] private PoolPreloadable[] preloadables;
        
        private void Awake()
        {
            foreach (var item in preloadables)
                item.PrefabToLoad.Preload(item.Quantity);
        }
    }
}