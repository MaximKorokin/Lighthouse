using System.Linq;
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
        if (Direction == Vector2.zero)
        {
            MovableWorldObject.Direction = Vector2.zero;
            MovableWorldObject.Stop();
        }
        else
        {
            MovableWorldObject.Direction = Direction.normalized;
            MovableWorldObject.Move();
        }
        InvokeActors(new PrioritizedTargets(TriggeredWorldObjects.ToArray()));
    }
}
