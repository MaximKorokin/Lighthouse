using UnityEngine;

public class DashEffect : MoveOverrideEffect
{
    protected override Vector2 GetDirection(CastState castState)
    {
        Vector2 direction;
        if (castState.Source == castState.Target)
        {
            direction = (castState.Source as MovableWorldObject).TurnDirection;
        }
        else
        {
            direction = castState.Target.transform.position - castState.Source.transform.position;
        }
        return direction.normalized;
    }

    protected override void StartOverride(CastState castState)
    {
        base.StartOverride(castState);
        if (castState.GetTarget() is MovableWorldObject movable)
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
