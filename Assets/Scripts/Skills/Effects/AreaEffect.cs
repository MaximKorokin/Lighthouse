using UnityEngine;

public class AreaEffect : ComplexEffect
{
    [field: SerializeField]
    public float Radius { get; private set; }

    public override void Invoke(CastState castState)
    {
        foreach (var worldObject in Physics2DUtils
            .GetWorldObjectsInRadius(castState.GetTargetPosition(), Radius)
            .GetValidTargets(castState.Source, FactionsRelation.Enemy))
        {
            castState.Target = worldObject;
            base.Invoke(castState);
        }
    }
}
