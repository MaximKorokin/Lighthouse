using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class MovableWorldObject : DestroyableWorldObject
{
    [field: SerializeField]
    public bool CanRotate { get; protected set; }
    [field: SerializeField]
    public bool CanFlip { get; protected set; }

    public Vector2 Direction { get; private set; }

    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody;

    public abstract void Act(WorldObject worldObject);

    protected override void Awake()
    {
        base.Awake();
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected virtual void Update()
    {
        if (CanRotate)
        {
            transform.right = Direction;
        }
    }

    protected virtual void FixedUpdate()
    {
        _rigidbody.velocity = Stats[StatName.Speed] * Direction;
    }

    public void Move(Vector2 direction)
    {
        Direction = direction.normalized;

        if (CanFlip && _spriteRenderer != null && Direction.x != 0)
        {
            _spriteRenderer.flipX = Direction.x < 0;
        }

        SetAnimatorValue("Speed", direction.sqrMagnitude);
    }

    public void Stop()
    {
        Direction = Vector2.zero;

        SetAnimatorValue("Speed", 0);
    }

    public override void DestroyWorldObject()
    {
        base.DestroyWorldObject();
        _rigidbody.simulated = false;
        Stop();
    }
}
