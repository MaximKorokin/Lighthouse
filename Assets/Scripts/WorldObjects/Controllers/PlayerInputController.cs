using UnityEngine;

[RequireComponent(typeof(MovableWorldObject))]
public class PlayerInputController : ControllerBase
{
    private Vector2 _direction;
    private MovableWorldObject _movable;

    protected override void Awake()
    {
        base.Awake();
        _movable = GetComponent<MovableWorldObject>();
        InputManager.MoveVectorChanged += OnMoveVectorChanged;
    }

    private void OnMoveVectorChanged(Vector2 vector)
    {
        _direction = vector;
    }

    protected override void Control()
    {
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
        InputManager.MoveVectorChanged -= OnMoveVectorChanged;
    }
}
