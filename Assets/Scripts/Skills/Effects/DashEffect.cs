using UnityEngine;

public class DashEffect : ControllerOverrideEffect
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

    protected override WorldObject GetTarget(CastState castState)
    {
        return castState.Source;
    }

    protected override void StartOverride(CastState castState)
    {
        base.StartOverride(castState);
        if (GetTarget(castState) is MovableWorldObject movable)
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
