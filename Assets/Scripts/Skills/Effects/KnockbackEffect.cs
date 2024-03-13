using UnityEngine;

public class KnockbackEffect : ControllerOverrideEffect
{
    protected override Vector2 GetDirection(CastState castState)
    {
        return castState.Target.transform.position - castState.Source.transform.position;
    }
}
