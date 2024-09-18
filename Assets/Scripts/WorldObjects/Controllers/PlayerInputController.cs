using UnityEngine;

[RequireComponent(typeof(MovableWorldObject))]
public class PlayerInputController : TargetController
{
    private Vector2 _direction;

    protected override void Awake()
    {
        base.Awake();

        MovableWorldObject.Direction = _direction;
        InputReader.MoveInputRecieved += OnMoveVectorChanged;
    }

    private void OnMoveVectorChanged(Vector2 vector)
    {
        _direction = vector;
    }

    protected override void Control()
    {
        MovableWorldObject.Direction = _direction;
        if (_direction == Vector2.zero)
        {
            MovableWorldObject.Stop();
        }
        else
        {
            MovableWorldObject.Move();
        }

        base.Control();

        InvokeActors(new PrioritizedTargets(Target, TriggeredWorldObjects, PrimaryTargets, SecondaryTargets));
    }

    private void OnDestroy()
    {
        InputReader.MoveInputRecieved -= OnMoveVectorChanged;
    }
}
