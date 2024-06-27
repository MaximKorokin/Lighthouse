using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutinesHandler : MonoBehaviorSingleton<CoroutinesHandler>
{
    private static readonly Dictionary<object, Coroutine> _uniqueCoroutines = new();

    public void StartUniqueCoroutine(object obj, IEnumerator enumerator)
    {
        if (Instance == null)
        {
            return;
        }

        if (_uniqueCoroutines.TryGetValue(obj, out var coroutine) && coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        _uniqueCoroutines[obj] = StartCoroutine(enumerator);
    }
}
