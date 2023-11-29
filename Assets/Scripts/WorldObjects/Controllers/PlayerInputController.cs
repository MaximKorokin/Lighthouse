using UnityEngine;

public class PlayerInputController : ControllerBase
{
    private Vector2 _direction;

    protected override void Awake()
    {
        base.Awake();
        InputManager.MoveVectorChanged += OnMoveVectorChanged;
    }

    private void OnMoveVectorChanged(Vector2 vector)
    {
        _direction = vector;
    }

    protected override void Control()
    {
        InvokeActors(WorldObject);

        if (WorldObject.Direction == _direction)
        {
            return;
        }

        WorldObject.Direction = _direction;
        if (_direction == Vector2.zero)
        {
            WorldObject.Stop();
        }
        else
        {
            WorldObject.Move();
        }
    }

    private void OnDestroy()
    {
        InputManager.MoveVectorChanged -= OnMoveVectorChanged;
    }
}
