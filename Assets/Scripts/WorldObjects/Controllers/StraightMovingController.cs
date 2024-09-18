using UnityEngine;

public class StraightMovingController : MovableController
{
    public Vector2 Direction { get; set; }

    public void SetTargetPosition(Vector2 targetPosition)
    {
        Direction = targetPosition - (Vector2)transform.position;
    }

    protected override void Control()
    {
        MovableWorldObject.Direction = Direction.normalized;
        MovableWorldObject.Move();
    }

    protected override void Trigger(WorldObject worldObject, bool entered)
    {
        base.Trigger(worldObject, entered);
        if (entered)
        {
            InvokeActors(new PrioritizedTargets(worldObject, TriggeredWorldObjects, TriggeredWorldObjects, TriggeredWorldObjects));
        }
    }
}
