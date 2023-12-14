using UnityEngine;

public class KnockbackEffect : MoveOverrideEffect
{
    protected override Vector2 GetDirection(CastState castState)
    {
        return castState.Target.transform.position - castState.Source.transform.position;
    }

    protected override WorldObject GetMoveTarget(CastState castState)
    {
        return castState.Target;
    }
}
