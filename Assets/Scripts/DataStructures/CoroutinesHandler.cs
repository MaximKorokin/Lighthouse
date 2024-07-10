using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutinesHandler : MonoBehaviorSingleton<CoroutinesHandler>
{
    private static readonly Dictionary<object, Coroutine> _uniqueCoroutines = new();

    public static void StartUniqueCoroutine(object obj, IEnumerator enumerator)
    {
        if (Instance == null)
        {
            if (ReferenceEquals(Instance, null))
            {
                Logger.Warn($"{nameof(Instance)} of {nameof(CoroutinesHandler)} is null. Coroutine will not start.");
            }
            return;
        }

        if (_uniqueCoroutines.TryGetValue(obj, out var coroutine) && coroutine != null)
        {
            Instance.StopCoroutine(coroutine);
        }
        _uniqueCoroutines[obj] = Instance.StartCoroutine(enumerator);
    }
}
