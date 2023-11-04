using System;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "ConditionalEffect", menuName = "ScriptableObjects/Effects/ConditionalEffect", order = 1)]
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

    public override void InvokeEnd(CastState castState)
    {
        if (CanInvoke(castState))
        {
            base.InvokeEnd(castState);
        }
    }

    private bool CanInvoke(CastState castState)
    {
        if (_conditions == EffectCondition.None)
        {
            return false;
        }

        var direction = castState.InitialSource.transform.position - castState.Target.transform.position;
        if ((_conditions & EffectCondition.InActionRange) == EffectCondition.InActionRange &&
            castState.InitialSource.ActionRange * castState.InitialSource.ActionRange < direction.sqrMagnitude)
        {
            return false;
        }
        if ((_conditions & EffectCondition.IsReachable) == EffectCondition.IsReachable &&
            Physics2D.RaycastAll(castState.InitialSource.transform.position, direction.normalized, castState.InitialSource.ActionRange)
                .TakeWhile(x => x.transform.gameObject != castState.Target.gameObject)
                .OrderBy(x => x.distance).Any(x => x.transform.gameObject.GetComponent<Obstacle>() != null))
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
