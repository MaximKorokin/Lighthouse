using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class MovableWorldObject : DestroyableWorldObject
{
    [field: SerializeField]
    public bool CanRotate { get; protected set; }

    public Vector2 Direction { get; private set; }

    private Rigidbody2D _rigidbody;

    public abstract void Act(WorldObject worldObject);

    protected override void Awake()
    {
        base.Awake();
        _rigidbody = GetComponent<Rigidbody2D>();
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
    }

    public void Stop()
    {
        Direction = Vector2.zero;
    }

    public override void DestroyWorldObject()
    {
        base.DestroyWorldObject();
        Stop();
    }
}
