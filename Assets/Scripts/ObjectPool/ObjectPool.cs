using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectPool<T, P> : Singleton<ObjectPool<T, P>> where T : Component
{
    [field: SerializeField]
    protected T Object { get; private set; }

    protected HashSet<T> AllObjects { get; private set; } = new();
    protected Stack<T> Pool { get; private set; } = new();

    protected virtual T Create()
    {
        var obj = Instantiate(Object, transform);
        obj.gameObject.SetActive(false);
        AllObjects.Add(obj);
        Pool.Push(obj);
        return obj;
    }

    public static T Take(P param)
    {
        if (Instance.Pool.Count == 0)
        {
            Instance.Create();
        }
        var obj = Instance.Pool.Pop();
        Instance.Initialize(obj, param);
        return obj;
    }

    public static void Return(T obj)
    {
        if (Instance == null || !Instance.AllObjects.Contains(obj))
        {
            return;
        }

        Instance.Pool.Push(obj);
        obj.transform.SetParent(Instance.transform, false);
        obj.transform.localScale = Vector3.one;
        Instance.Deinitialize(obj);
        obj.gameObject.SetActive(false);
    }

    protected abstract void Initialize(T obj, P param);
    protected abstract void Deinitialize(T obj);
}
