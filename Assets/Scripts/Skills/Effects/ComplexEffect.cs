using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ComplexEffect", menuName = "ScriptableObjects/Effects/ComplexEffect", order = 1)]
public class ComplexEffect : Effect
{
    [field: SerializeField]
    public Effect[] Effects { get; private set; }
    [field: SerializeField]
    public Effect[] EndEffects { get; private set; }

    public event Action Ending;

    public override void Invoke(CastState castState)
    {
        foreach (var effect in Effects)
        {
            effect.Invoke(castState);
        }
    }

    public virtual void InvokeEnd(CastState castState)
    {
        Ending?.Invoke();
        foreach (var endEffect in EndEffects)
        {
            endEffect.Invoke(castState);
        }
    }
}
