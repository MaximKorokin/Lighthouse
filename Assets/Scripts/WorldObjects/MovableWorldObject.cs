using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class MovableWorldObject : DestroyableWorldObject
{
    private const float MoveSpeedModifier = 2f;

    [field: SerializeField]
    public bool CanRotate { get; protected set; }
    [field: SerializeField]
    public bool CanFlip { get; protected set; }

    public Vector2 Direction { get => _direction; set => _direction = value.sqrMagnitude > 1f ? value.normalized : value; }
    public bool IsMoving { get; private set; }

    private Vector2 _direction;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody;

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
        // this helps against "random" velocity sources such as collisions with other colliders
        if (_rigidbody.velocity != Vector2.zero)
        {
            _rigidbody.velocity = Vector2.zero;
        }

        if (IsMoving)
        {
            _rigidbody.MovePosition((Vector2)transform.position + MoveSpeedModifier * Stats[StatName.MoveSpeed] * Time.fixedDeltaTime * Direction);
        }
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
