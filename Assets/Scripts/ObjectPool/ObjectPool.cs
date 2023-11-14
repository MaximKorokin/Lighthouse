using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectPool<T, P> : MonoBehaviour where T : Component
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

    public T Take(P param)
    {
        if (Pool.Count == 0)
        {
            Create();
        }
        var obj = Pool.Pop();
        Initialize(obj, param);
        return obj;
    }

    public void Return(T obj)
    {
        if (!AllObjects.Contains(obj))
        {
            return;
        }
        Pool.Push(obj);
        obj.transform.parent = transform;
        Deinitialize(obj);
        obj.gameObject.SetActive(false);
    }

    protected abstract void Initialize(T obj, P param);
    protected abstract void Deinitialize(T obj);
}
