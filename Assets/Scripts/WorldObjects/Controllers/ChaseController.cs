using UnityEngine;

public class ChaseController : TargetController
{
    protected override void Control()
    {
        base.Control();
        if (Target == null)
        {
            MovableWorldObject.Stop();
            return;
        }
        var direction = (Vector2)Target.transform.position - (Vector2)transform.position;
        MovableWorldObject.Direction = direction.normalized;

        if (direction.magnitude > WorldObject.ActionRange)
        {
            MovableWorldObject.Move();
        }
        else
        {
            MovableWorldObject.Stop();
        }
        InvokeActors(new PrioritizedTargets(Target, TriggeredWorldObjects, PrimaryTargets, SecondaryTargets));
    }
}
