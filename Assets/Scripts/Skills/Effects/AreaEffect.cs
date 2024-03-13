using UnityEngine;

public class AreaEffect : PeriodicEffect
{
    [field: SerializeField]
    public float Radius { get; private set; }

    public override void Invoke(CastState castState)
    {
        foreach (var worldObject in Physics2DUtils
            .GetWorldObjectsInRadius(castState.GetTargetPosition(), Radius)
            .GetValidTargets(castState.Source))
        {
            castState.Target = worldObject;
            base.Invoke(castState);
        }
    }
}
