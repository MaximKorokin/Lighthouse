using UnityEngine;

public static class RequireUtils
{
    public static T GetRequiredComponent<T>(this GameObject behaviour) where T : Component
    {
        if (!behaviour.TryGetComponent<T>(out var component))
        {
            Logger.Error($"GameObject {behaviour.name} doesn't contain required component {typeof(T)}");
        }
        return component;
    }

    public static T GetRequiredComponent<T>(this Component behaviour) where T : Component
    {
        if (!behaviour.TryGetComponent<T>(out var component))
        {
            Logger.Error($"Component {behaviour.name} doesn't contain required component {typeof(T)}");
        }
        return component;
    }

    public static TTarget CastRequired<TSource, TTarget>(TSource sourceOject)
    {
        if (sourceOject is TTarget targetObject)
        {
            return targetObject;
        }
        Logger.Error($"Object {sourceOject} of type {typeof(TSource)} is not of type {typeof(TTarget)}");
        return default;
    }
}
