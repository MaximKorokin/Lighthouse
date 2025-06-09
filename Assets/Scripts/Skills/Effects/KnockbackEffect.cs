using UnityEngine;

public class KnockbackEffect : MoveOverrideEffect
{
    [SerializeField]
    private bool _toSource = false;

    protected override Vector2 GetDirection(CastState castState)
    {
        if (_toSource)
            return (Vector2)castState.Source.transform.position - castState.GetTargetPosition();
        else
            return castState.GetTargetPosition() - (Vector2)castState.Source.transform.position;
    }

    protected override void StartOverride(CastState castState)
    {
        var movable = castState.GetMovableTarget();
        if (movable != null)
        {
            castState.Cache.Set(this.GetIdentifier(movable), movable.CanFlip);
            movable.CanFlip = false;
        }
        base.StartOverride(castState);
    }

    protected override void StopOverride(CastState castState)
    {
        base.StopOverride(castState);
        var movable = castState.GetMovableTarget();
        if (movable != null)
        {
            movable.CanFlip = castState.Cache.Get<bool>(this.GetIdentifier(movable));
        }
    }
}
