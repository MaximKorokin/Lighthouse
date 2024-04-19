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
            return false;
        }

        var direction = (Vector2)castState.InitialSource.transform.position - (Vector2)castState.Target.transform.position;
        if ((_conditions & EffectCondition.InActionRange) == EffectCondition.InActionRange &&
            castState.InitialSource.ActionRange * castState.InitialSource.ActionRange < direction.sqrMagnitude)
        {
            return false;
        }
        if ((_conditions & EffectCondition.IsReachable) == EffectCondition.IsReachable &&
            castState.Target.gameObject.IsReachable(
                castState.InitialSource.transform.position,
                direction.normalized,
                castState.InitialSource.ActionRange))
        {
            return false;
        }
        return true;
    }
}

[Flags]
public enum EffectCondition
{
    None,
    InActionRange,
    IsReachable,
}
