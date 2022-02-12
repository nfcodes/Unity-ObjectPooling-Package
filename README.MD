# Unity-ObjectPooling-Package

Custom pooling implementation for my usage. If you would like to use the package, below is the little instruction how to do it.

1. [Features](#features)
1. [Installation](#installation)
1. [Usage](#usage)


# Features

- Extension methods for easily and fluent getting objects from a pool and returning it.
- Prewarming using In Scene `PrefabPoolPreloader` or by the fluent API.
- `OnGet` and `OnRelease` callbacks for maintaining object members when getting or releasing the object.

# Installation

## Package Manager (Recommended way)

Open Unity Package Manager, and then select ``'Add package from git...'``. Then just simply paste the link do this repository ``https://github.com/nfcodes/Unity-ObjectPooling-Package.git``

## Manually Installation

You just have to manually download all source files, and put in somewhere inside your Unity project.

# Usage

## Directly calling PoolManager

### How to get the pool
```csharp
public class ExampleClass : MonoBehaviour
{
   public GameObject MyPrefab;

    private void Awake()
    {
        Pool pool = PoolManager.GetPrefabPool(MyPrefab);
    }
}
```

Then on the pool you can use one of the following methods:

```csharp
pool.Get() // To get object from the pool
pool.Preload(count) // To preload object to the pool
pool.Return(instance) // To return object to the pool
```

## Using extension methods on GameObject

### Example how to use extension methods
```csharp
public class ExampleClass : MonoBehaviour
{
   public GameObject MyPrefab;

    private void Awake()
    {
        MyPrefab.Praload(5) // This will preload 5 prefabs
        MyPrefab.FromPool(MyPrefab) // This will return object from pool
        MyPrefab.FromPool(MyPrefab, new Vector3(10, 10, 10)) // This will return object from pool, and set it's position to (10, 10, 10). See code for more overloads.
        MyPrefab.Release() // This will return prefab to the pool or destory it when was created by GameObject.Instantiate method.
    }
}
```

`FroomPool` method has many overloads. Check source to get more information.

