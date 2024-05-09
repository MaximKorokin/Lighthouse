using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class WorldObjectsInteractionUtils
{
    public static IEnumerable<WorldObject> GetValidTargets(this IEnumerable<WorldObject> worldObjects, WorldObject source, FactionsRelation relation)
    {
        var validator = source.GetComponent<WorldObjectValidator>();
        if (validator == null)
        {
            return worldObjects;
        }
        else
        {
            return worldObjects.Where(x => validator.IsValidTarget(x, relation));
        }
    }

    public static bool IsValidTarget(this WorldObject worldObject, WorldObject source, FactionsRelation relation)
    {
        var validator = source.GetComponent<WorldObjectValidator>();
        if (validator == null)
        {
            return true;
        }
        else
        {
            return validator.IsValidTarget(worldObject, relation);
        }
    }

    public static WorldObject GetTarget(this CastState castState)
    {
        return castState.TargetingType switch
        {
            TargetingType.Source => castState.Source,
            TargetingType.Target => castState.Target,
            _ => castState.InitialSource,
        };
    }

    public static Vector2 GetTargetPosition(this CastState castState)
    {
        return castState.TargetingType switch
        {
            TargetingType.Source => castState.Source.transform.position,
            TargetingType.Target => castState.Target.transform.position,
            TargetingType.Point => castState.Payload is PointCastStatePayload payload ? payload.Position : castState.InitialSource.transform.position,
            _ => castState.InitialSource.transform.position,
        };
    }

    public static Coroutine StartCoroutineSafe(this WorldObject worldObject, IEnumerator enumerator, Action finalAction = null)
    {
        Coroutine coroutine = null;
        var finalActionCalled = false;
        coroutine = worldObject.StartCoroutine(SafeCoroutine());
        worldObject.Destroyed += OnWorldObjectDestroyed;
        return coroutine;

        IEnumerator SafeCoroutine()
        {
            while (enumerator.MoveNext())
            {
                yield return enumerator.Current;
            }
            Cancel();
        }

        void OnWorldObjectDestroyed()
        {
            if (coroutine != null)
            {
                worldObject.StopCoroutine(coroutine);
            }
            Cancel();
        }

        void Cancel()
        {
            worldObject.Destroyed -= OnWorldObjectDestroyed;
            if (!finalActionCalled)
            {
                finalAction?.Invoke();
            }
            finalActionCalled = true;
        }
    }

    public static void OnDestroyed(this WorldObject worldObject, Action finalAction)
    {
        worldObject.Destroyed += OnWorldObjectDestroyed;

        void OnWorldObjectDestroyed()
        {
            finalAction.Invoke();
            worldObject.Destroyed -= OnWorldObjectDestroyed;
        }
    }

    public static void OnDestroying(this DestroyableWorldObject worldObject, Action finalAction)
    {
        worldObject.Destroying += OnWorldObjectDestroying;

        void OnWorldObjectDestroying()
        {
            finalAction.Invoke();
            worldObject.Destroyed -= OnWorldObjectDestroying;
        }
    }
}
