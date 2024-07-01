using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class MovableWorldObject : DestroyableWorldObject
{
    [field: SerializeField]
    public bool CanRotate { get; set; }
    [field: SerializeField]
    public bool CanFlip { get; set; }

    public Vector2 Direction
    {
        get => _direction;
        set
        {
            _direction = value.sqrMagnitude > 1f ? value.normalized : value;
            DirectionSet?.Invoke(_direction);
        }
    }
    public Vector2 TurnDirection { get; private set; } = Vector2.right;
    public bool IsMoving { get; private set; }
    [field: SerializeField]
    public bool IsFlipped { get; private set; }

    private Vector2 _direction;
    private Rigidbody2D _rigidbody;
    private LayerMask _rigidbodyExcludeLayerMask;
    private bool _previousFlipX;
    private float _currentMoveSpeed;

    public event Action<bool> Flipped;
    public event Action<Vector2> DirectionSet;

    protected override void Awake()
    {
        base.Awake();
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbodyExcludeLayerMask = _rigidbody.excludeLayers;
        DirectionSet += OnDirectionSet;
    }

    protected override void Start()
    {
        base.Start();
        Flipped?.Invoke(IsFlipped);
    }

    protected override void OnStatsModified()
    {
        base.OnStatsModified();

        SetAnimatorValue(AnimatorKey.MoveSpeed, Stats[StatName.MoveSpeed]);
    }

    protected virtual void FixedUpdate()
    {
        // this helps against "random" velocity sources such as collisions with other colliders
        if (_rigidbody.velocity != Vector2.zero)
        {
            _rigidbody.velocity = Vector2.zero;
        }

        if (IsMoving)
        {
            //_rigidbody.MovePosition((Vector2)transform.position + MoveSpeedModifier * _speed * Time.fixedDeltaTime * Direction);
            _rigidbody.velocity = _currentMoveSpeed * Direction;
        }
    }

    public void SetRigidbodyCollisions(bool enable)
    {
        _rigidbody.excludeLayers = enable ? _rigidbodyExcludeLayerMask : (-1 ^ LayerMask.GetMask(Constants.ObstacleLayerName));
    }

    public virtual void Move(float speedOverride = -1)
    {
        _currentMoveSpeed = speedOverride < 0 ? Stats[StatName.MoveSpeed] : speedOverride;
        IsMoving = true;
        SetAnimatorValue(AnimatorKey.IsMoving, true);
        //SetAnimatorValue(AnimatorKey.MoveSpeed, _currentMoveSpeed * Direction.magnitude);
    }

    public virtual void Stop()
    {
        IsMoving = false;

        SetAnimatorValue(AnimatorKey.IsMoving, false);
    }

    public override void DestroyWorldObject()
    {
        base.DestroyWorldObject();
        // Fog is shown if detects that player left trigger
        if (this is not PlayerCreature)
        {
            _rigidbody.simulated = false;
        }
        Stop();
    }

    private void OnDirectionSet(Vector2 direction)
    {
        if (direction != Vector2.zero)
        {
            TurnDirection = direction;
        }
        if (CanFlip && direction.x != 0)
        {
            IsFlipped = direction.x < 0;
            if (_previousFlipX != IsFlipped)
            {
                Flipped?.Invoke(IsFlipped);
            }
            _previousFlipX = IsFlipped;
        }
        if (CanRotate)
        {
            transform.right = direction;
        }
    }
}
