using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class EffectUtils
{
    public static WorldObject GetTarget(this CastState castState)
    {
        return castState.TargetingType switch
        {
            TargetingType.Source => castState.Source,
            TargetingType.Target => castState.Target,
            TargetingType.Point => castState.Target,
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

    public static DestroyableWorldObject GetDestroyableTarget(this CastState state)
    {
        return state.GetTarget() as DestroyableWorldObject;
    }

    public static MovableWorldObject GetMovableTarget(this CastState state)
    {
        return state.GetTarget() as MovableWorldObject;
    }

    public static ControllerBase GetTargetController(this CastState state)
    {
        var target = state.GetTarget();
        return target == null ? null : target.GetComponent<ControllerBase>();
    }

    public static string GetIdentifier(this Effect effect, IEnumerable<string> additionalStrings)
    {
        return string.Join('_', effect.GetHashCode().ToString().YieldWith(additionalStrings));
    }

    public static string GetIdentifier(this Effect effect, params Component[] additionalComponents)
    {
        return GetIdentifier(effect, additionalComponents.Select(x => x.GetInstanceID().ToString()));
    }
}
