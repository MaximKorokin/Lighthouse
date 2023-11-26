using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class MovableWorldObject : DestroyableWorldObject
{
    private const float MoveSpeedModifier = 3;

    [field: SerializeField]
    public bool CanRotate { get; protected set; }
    [field: SerializeField]
    public bool CanFlip { get; protected set; }

    public Vector2 Direction { get => _direction; set => _direction = value.normalized; }
    public bool IsMoving { get; private set; }

    private Vector2 _direction;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody;

    public abstract void Act(WorldObject worldObject);

    protected override void Awake()
    {
        base.Awake();
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void OnStatsModified()
    {
        base.OnStatsModified();

        SetAnimatorValue(AnimatorKey.MoveSpeed, Stats[StatName.MoveSpeed]);
    }

    protected virtual void Update()
    {
        if (CanFlip && _spriteRenderer != null && Direction.x != 0)
        {
            _spriteRenderer.flipX = Direction.x < 0;
        }
        if (CanRotate)
        {
            transform.right = Direction;
        }
    }

    protected virtual void FixedUpdate()
    {
        _rigidbody.velocity = IsMoving ? (Stats[StatName.MoveSpeed] * MoveSpeedModifier * Direction) : Vector2.zero;
    }

    public void Move()
    {
        IsMoving = true;

        SetAnimatorValue(AnimatorKey.IsMoving, true);
    }

    public void Stop()
    {
        IsMoving = false;

        SetAnimatorValue(AnimatorKey.IsMoving, false);
    }

    public override void DestroyWorldObject()
    {
        base.DestroyWorldObject();
        _rigidbody.simulated = false;
        Stop();
    }
}
