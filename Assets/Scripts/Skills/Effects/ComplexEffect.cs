using System;
using UnityEngine;

public abstract class ComplexEffect : Effect
{
    [field: SerializeField]
    public Effect[] Effects { get; private set; }
    [field: SerializeField]
    public Effect[] EndEffects { get; private set; }

    public event Action Ending;

    public override void Invoke(WorldObject source, WorldObject target)
    {
        foreach (var effect in Effects)
        {
            effect.Invoke(source, target);
        }
    }

    public virtual void InvokeEnd(WorldObject source, WorldObject target)
    {
        Ending?.Invoke();
        foreach (var endEffect in EndEffects)
        {
            endEffect.Invoke(source, target);
        }
    }
}
