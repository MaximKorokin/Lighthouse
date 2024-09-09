using UnityEngine;

public class StatsEffect : Effect
{
    [field: SerializeField]
    public Stats StatsDelta { get; private set; }

    public override void Invoke(CastState castState)
    {
        castState.Target.Stats.Modify(StatsDelta, StatsModificationType.Add);
    }
}
