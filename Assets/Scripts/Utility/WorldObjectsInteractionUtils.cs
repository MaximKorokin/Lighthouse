using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class WorldObjectsInteractionUtils
{
    public static IEnumerable<WorldObject> GetValidTargets(this IEnumerable<WorldObject> worldObjects, WorldObject source)
    {
        if (!source.TryGetComponent<WorldObjectInteractingTriggerDetector>(out var detector))
        {
            return worldObjects;
        }
        else
        {
            return worldObjects.Where(x => detector.IsValidTarget(x));
        }
    }

    public static bool IsValidTarget(this WorldObject worldObject, WorldObject source)
    {
        if (!source.TryGetComponent<WorldObjectInteractingTriggerDetector>(out var detector))
        {
            return true;
        }
        else
        {
            return detector.IsValidTarget(worldObject);
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

    public static void SetRigidbodyCollisions(this MovableWorldObject movable, bool value)
    {
        movable.RigidbodyExtender.SetExcludeLayers(-1 ^ LayerMask.GetMask(Constants.ObstacleLayerName, Constants.NoIgnoreLayerName), !value);
        movable.ReloadPhysicsState();
    }
}
