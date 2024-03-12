using UnityEngine;

public class AreaEffect : PeriodicEffect
{
    [field: SerializeField]
    public float Radius { get; private set; }

    public override void Invoke(CastState castState)
    {
        var position = castState.GetTargetPosition();
        var radius = Radius;

        if (castState.Payload is PointCastStatePayload payload && payload.Radius > 0)
        {
            radius = payload.Radius;
        }

        foreach (var worldObject in Physics2DUtils
            .GetWorldObjectsInRadius(position, radius)
            .GetValidTargets(castState.Source))
        {
            castState.Target = worldObject;
            base.Invoke(castState);
        }
    }
}
