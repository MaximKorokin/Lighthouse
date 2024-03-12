using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class WorldObjectsInteractionUtils
{
    public static IEnumerable<WorldObject> GetValidTargets(this IEnumerable<WorldObject> worldObjects, WorldObject source)
    {
        var validator = source.GetComponent<ValidatorBase>();
        if (validator == null)
        {
            return worldObjects;
        }
        else
        {
            return worldObjects.Where(x => validator.IsValidTarget(x));
        }
    }

    public static bool IsValidTarget(this WorldObject worldObject, WorldObject source)
    {
        var validator = source.GetComponent<ValidatorBase>();
        if (validator == null)
        {
            return true;
        }
        else
        {
            return validator.IsValidTarget(worldObject);
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
}
