using UnityEngine;

public class ComplexEffect : Effect
{
    [field: SerializeReference]
    public Effect[] Effects { get; private set; }

    public override void Invoke(CastState castState)
    {
        foreach (var effect in Effects)
        {
            effect.Invoke(castState);
        }
    }
}
