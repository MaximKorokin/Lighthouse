using UnityEngine;

public abstract class ComplexEffect : Effect
{
    [field: SerializeField]
    public Effect[] Effects { get; private set; }
    [field: SerializeField]
    public Effect[] EndEffects { get; private set; }

    public override void Invoke(WorldObject source, WorldObject target)
    {
        foreach (var effect in Effects)
        {
            effect.Invoke(source, target);
        }
    }
}
