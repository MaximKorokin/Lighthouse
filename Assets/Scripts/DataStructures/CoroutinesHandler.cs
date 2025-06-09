using System;
using System.Collections;
using System.Collections.Generic;

public class CoroutinesHandler : MonoBehaviorSingleton<CoroutinesHandler>
{
    private static readonly Dictionary<object, CoroutineWrapper> _uniqueCoroutines = new();

    public static CoroutineWrapper StartUniqueCoroutine(object obj, IEnumerator enumerator, Action finalAction = null)
    {
        if (Instance == null)
        {
            if (ReferenceEquals(Instance, null))
            {
                Logger.Warn($"{nameof(Instance)} of {nameof(CoroutinesHandler)} is null. Coroutine will not start.");
            }
            return null;
        }

        if (_uniqueCoroutines.TryGetValue(obj, out var coroutine) && !coroutine.HasFinished)
        {
            Instance.StopCoroutine(coroutine);
        }

        coroutine = Instance.StartCoroutineSafe(enumerator, (() => _uniqueCoroutines.Remove(obj)) + finalAction);
        _uniqueCoroutines[obj] = coroutine;
        return coroutine;
    }
}
