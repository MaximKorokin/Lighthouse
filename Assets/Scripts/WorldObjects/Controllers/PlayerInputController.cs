using UnityEngine;

[RequireComponent(typeof(MovableWorldObject))]
public class PlayerInputController : TriggerController
{
    private Vector2 _direction;
    private MovableWorldObject _movable;

    protected override void Awake()
    {
        base.Awake();
        _movable = GetComponent<MovableWorldObject>();

        _movable.Direction = _direction;
        InputReader.MoveInputRecieved += OnMoveVectorChanged;
    }

    protected override void Trigger(WorldObject worldObject, bool entered) { }

    private void OnMoveVectorChanged(Vector2 vector)
    {
        _direction = vector;
    }

    protected override void Control()
    {
        if (!_movable.IsAlive)
        {
            return;
        }

        _movable.Direction = _direction;
        if (_direction == Vector2.zero)
        {
            _movable.Stop();
        }
        else
        {
            _movable.Move();
        }

        InvokeActors(WorldObject);
    }

    private void OnDestroy()
    {
        InputReader.MoveInputRecieved -= OnMoveVectorChanged;
    }
}
