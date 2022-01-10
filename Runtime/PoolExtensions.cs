using UnityEngine;

namespace NF.ObjectPooling.Runtime
{
    public static class PoolExtensions
    {
        public static GameObject FromPool(this GameObject instance) => PoolManager.GetPrefabPool(instance).Get();
        public static GameObject FromPool(this GameObject instance, Vector3 position) => PoolManager.GetPrefabPool(instance).Get(position);
        public static GameObject FromPool(this GameObject instance, Vector3 position, Quaternion rotation) => PoolManager.GetPrefabPool(instance).Get(position, rotation);
        public static GameObject FromPool(this GameObject instance, Transform parent) => PoolManager.GetPrefabPool(instance).Get(parent);
        public static GameObject FromPool(this GameObject instance, Transform parent, Vector3 position) => PoolManager.GetPrefabPool(instance).Get(parent, position);
        public static GameObject FromPool(this GameObject instance, Transform parent, Vector3 position, Quaternion rotation) => PoolManager.GetPrefabPool(instance).Get(parent, position, rotation);

        public static T FromPool<T>(this GameObject instance) where T : Component =>
            FromPool(instance).GetComponent<T>();
        
        public static T FromPool<T>(this GameObject instance, Vector3 position) where T : Component =>
            FromPool(instance, position).GetComponent<T>();
        
        public static T FromPool<T>(this GameObject instance, Vector3 position, Quaternion rotation) where T : Component =>
            FromPool(instance, position, rotation).GetComponent<T>();
        
        public static T FromPool<T>(this GameObject instance, Transform parent) where T : Component =>
            FromPool(instance, parent).GetComponent<T>();
        
        public static T FromPool<T>(this GameObject instance, Transform parent, Vector3 position) where T : Component =>
            FromPool(instance, parent, position).GetComponent<T>();
        
        public static T FromPool<T>(this GameObject instance, Transform parent, Vector3 position, Quaternion rotation) where T : Component =>
            FromPool(instance, parent, position, rotation).GetComponent<T>();
        
        public static void Preload(this GameObject instance, int count) => PoolManager.GetPrefabPool(instance).Preload(count);
        
        public static void Release(this GameObject instance)
        {
            if (PoolManager.TryGetInstancePool(instance, out Pool pool))
                pool.Return(instance);
            else
                Object.Destroy(instance);
        }
        
    }
}