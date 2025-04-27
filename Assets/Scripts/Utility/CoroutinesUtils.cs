using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

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
        // SafeCoroutine uses wrapper variable, so it is declared before creation
        CoroutineWrapper wrapper = null;
        wrapper = new(behaviour.StartCoroutine(SafeCoroutine()), finalAction);
        return wrapper;

        IEnumerator SafeCoroutine()
        {
            // Needs this condition in case of enumerator is empty and therefore wrapper is not created before Stop call
            if (enumerator.MoveNext())
            {
                yield return enumerator.Current;
            }
            else
            {
                yield return null;
            }

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

    public static IEnumerator WaitForSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }

    public static IEnumerator YieldNull()
    {
        yield return null;
    }

    public static IEnumerator WaitForAsyncOperation(AsyncOperation operation)
    {
        while (operation != null && !operation.isDone)
        {
            yield return null;
        }
    }

    public static IEnumerator TilemapAlphaCoroutine(Tilemap tilemap, float targetAlpha, float stepMultiplier, float time)
    {
        yield return new WaitForSeconds(0.1f);
        while (tilemap.color.a != targetAlpha)
        {
            var step = stepMultiplier / time * Time.deltaTime;
            var newAlpha = stepMultiplier > 0
                ? Mathf.Clamp(tilemap.color.a + step, 0, targetAlpha)
                : Mathf.Clamp(tilemap.color.a + step, targetAlpha, 1);
            tilemap.color = new(tilemap.color.r, tilemap.color.g, tilemap.color.b, newAlpha);
            yield return new WaitForEndOfFrame();
        }
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
