using UnityEngine;

public class DashEffect : MoveOverrideEffect
{
    protected override Vector2 GetDirection(CastState castState)
    {
        Vector2 direction;
        if (castState.Source == castState.Target)
        {
            direction = (castState.Source as MovableWorldObject).TurnDirection.normalized;
        }
        else
        {
            direction = castState.Target.transform.position - castState.Source.transform.position;
        }
        return direction;
    }

    protected override WorldObject GetMoveTarget(CastState castState)
    {
        return castState.Source;
    }

    protected override void StartOverride(CastState castState)
    {
        base.StartOverride(castState);
        if (GetMoveTarget(castState) is MovableWorldObject movable)
        {
            movable.SetRigidbodyCollisions(false);
        } 
    }

    protected override void StopOverride(WorldObject worldObject)
    {
        base.StopOverride(worldObject);
        if (worldObject is MovableWorldObject movable)
        {
            movable.SetRigidbodyCollisions(true);
        }
    }
}
