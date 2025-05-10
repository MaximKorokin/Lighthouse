using System;
using UnityEngine;

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

    private Rigidbody2D _rigidbody;
    private Rigidbody2D Rigidbody => gameObject.LazyGetComponent(ref _rigidbody);

    public Rigidbody2DExtender _rigidbodyExtender;
    public Rigidbody2DExtender RigidbodyExtender => this.LazyInitialize(ref _rigidbodyExtender, () => new Rigidbody2DExtender(Rigidbody));

    private Vector2 _direction;
    private bool _previousFlipX;
    private float _currentMoveSpeed;

    public event Action<bool> Flipped;
    public event Action<Vector2> DirectionSet;

    protected override void Awake()
    {
        base.Awake();
        DirectionSet += OnDirectionSet;
    }

    protected override void Start()
    {
        base.Start();
        Flipped?.Invoke(IsFlipped);
        _previousFlipX = IsFlipped;
    }

    protected override void OnStatsModified()
    {
        base.OnStatsModified();

        SetAnimatorValue(AnimatorKey.MoveSpeed, Stats[StatName.MoveSpeedModifier]);
    }

    protected virtual void FixedUpdate()
    {
        // this helps against "random" velocity sources such as collisions with other colliders
        if (Rigidbody.velocity != Vector2.zero)
        {
            Rigidbody.velocity = Vector2.zero;
        }

        if (IsMoving)
        {
            //Rigidbody.MovePosition((Vector2)transform.position + MoveSpeedModifier * _speed * Time.fixedDeltaTime * Direction);
            Rigidbody.velocity = _currentMoveSpeed * Direction;
        }
    }

    public virtual void Move(float speedOverride = -1)
    {
        _currentMoveSpeed = speedOverride < 0 ? Stats[StatName.MoveSpeedModifier] : speedOverride;

        if (IsMoving) return;

        IsMoving = true;
        SetAnimatorValue(AnimatorKey.IsMoving, true);
        //SetAnimatorValue(AnimatorKey.MoveSpeed, _currentMoveSpeed * Direction.magnitude);
    }

    public virtual void Stop()
    {
        if (!IsMoving) return;

        IsMoving = false;
        SetAnimatorValue(AnimatorKey.IsMoving, false);
    }

    public override void DestroyWorldObject()
    {
        // Fog is shown if detects that player left trigger
        if (this is not PlayerCreature)
        {
            Rigidbody.simulated = false;
        }
        Stop();
        base.DestroyWorldObject();
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
