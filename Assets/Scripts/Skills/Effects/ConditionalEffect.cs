using System;
using UnityEngine;

public class ConditionalEffect : ComplexEffect
{
    [SerializeField]
    private EffectCondition _conditions;

    public override void Invoke(CastState castState)
    {
        if (CanInvoke(castState))
        {
            base.Invoke(castState);
        }
    }

    private bool CanInvoke(CastState castState)
    {
        if (_conditions == EffectCondition.None)
        {
            return true;
        }

        var direction = (Vector2)castState.Target.transform.position - (Vector2)castState.InitialSource.transform.position;
        if (_conditions.HasFlag(EffectCondition.InActionRange) &&
            castState.InitialSource.ActionRange * castState.InitialSource.ActionRange < direction.sqrMagnitude)
        {
            return false;
        }
        if (_conditions.HasFlag(EffectCondition.IsAccessible) &&
            castState.Target.gameObject.IsBlockedByObstacle(
                castState.InitialSource.transform.position,
                direction.normalized,
                castState.InitialSource.ActionRange))
        {
            return false;
        }
        if (_conditions.HasFlag(EffectCondition.IsSourceAlive) &&
            castState.InitialSource is DestroyableWorldObject destroyable && !destroyable.IsAlive)
        {
            return false;
        }
        return true;
    }
}

[Flags]
public enum EffectCondition
{
    None = 0,
    InActionRange = 1,
    IsAccessible = 2,
    IsSourceAlive = 4,
}
