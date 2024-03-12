using System;
using UnityEngine;

public abstract class EndingEffect : ComplexEffect
{
    [field: SerializeReference]
    private Effect[] EndEffects { get; set; }

    public event Action Ending;

    public virtual void InvokeEnd(CastState castState)
    {
        Ending?.Invoke();
        foreach (var endEffect in EndEffects)
        {
            endEffect.Invoke(castState);
        }
    }
}
