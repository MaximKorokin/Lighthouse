using UnityEngine;

public class EscapingDashEffect : DashEffect
{
    protected override Vector2 GetDirection(CastState castState)
    {
        var escapeDirection = castState.Source.transform.position - castState.Target.transform.position;

        return escapeDirection;
    }
}
