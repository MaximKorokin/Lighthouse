using System;
using UnityEngine;

public static class LazyUtils
{
    public static T LazyGetComponent<T>(this GameObject gameObject, ref T component) where T : Component
    {
        if (component == null)
        {
            component = gameObject.GetRequiredComponent<T>();
        }
        return component;
    }

    public static T LazyInitialize<T>(this object _, ref T obj, Func<T> factory) where T : class
    {
        if (obj == null)
        {
            obj = factory();
        }
        return obj;
    }
}
