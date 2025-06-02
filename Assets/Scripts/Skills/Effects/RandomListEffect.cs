using UnityEngine;

public class RandomListEffect : Effect
{
    [field: Tooltip("Will invoke a random effect from list")]
    [field: SerializeReference]
    public Effect[] Effects { get; private set; }

    public override void Invoke(CastState castState)
    {
        Effects[Random.Range(0, Effects.Length)].Invoke(castState);
    }
}
