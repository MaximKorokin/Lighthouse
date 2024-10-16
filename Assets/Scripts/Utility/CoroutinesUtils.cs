﻿using System;
using System.Collections;
using UnityEngine;

public static class CoroutinesUtils
{
    public static CoroutineWrapper StartCoroutineSafe(this WorldObject worldObject, IEnumerator enumerator, Action finalAction = null)
    {
        CoroutineWrapper wrapper = null;
        worldObject.Destroyed += OnWorldObjectDestroyed;
        wrapper = (worldObject as MonoBehaviour).StartCoroutineSafe(enumerator, Cancel);
        return wrapper;

        void OnWorldObjectDestroyed()
        {
            if (wrapper != null)
            {
                worldObject.StopCoroutine(wrapper);
            }
        }

        void Cancel()
        {
            worldObject.Destroyed -= OnWorldObjectDestroyed;
            finalAction?.Invoke();
        }
    }

    public static CoroutineWrapper StartCoroutineSafe(this MonoBehaviour behaviour, IEnumerator enumerator, Action finalAction = null)
    {
        CoroutineWrapper wrapper = null;
        wrapper = new(behaviour.StartCoroutine(SafeCoroutine()), finalAction);
        return wrapper;

        IEnumerator SafeCoroutine()
        {
            while (enumerator.MoveNext())
            {
                yield return enumerator.Current;
            }
            wrapper.Stop();
        }
    }

    public static void StopCoroutine(this MonoBehaviour behaviour, CoroutineWrapper wrapper)
    {
        behaviour.StopCoroutine(wrapper.Coroutine);
        wrapper.Stop();
    }
}

public class CoroutineWrapper
{
    public Coroutine Coroutine { get; private set; }
    public Action FinalAction { get; private set; }
    public bool HasFinished { get; private set; }

    public CoroutineWrapper(Coroutine coroutine, Action finalAction = null)
    {
        Coroutine = coroutine;
        FinalAction = finalAction;
        HasFinished = false;
    }

    public void Stop()
    {
        if (!HasFinished)
        {
            HasFinished = true;
            FinalAction?.Invoke();
        }
    }
}
