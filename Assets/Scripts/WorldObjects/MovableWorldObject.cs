using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class MovableWorldObject : DestroyableWorldObject
{
    [field: SerializeField]
    public bool CanRotate { get; protected set; }

    private Rigidbody2D _rigidbody;
    private Vector2 _direction;

    protected override void Awake()
    {
        base.Awake();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update()
    {
        if (CanRotate)
        {
            transform.right = _direction;
        }
    }

    protected virtual void FixedUpdate()
    {
        _rigidbody.velocity = Stats[StatName.Speed] * _direction;
    }

    public void Move(Vector2 direction)
    {
        _direction = direction.normalized;
    }

    public void Stop()
    {
        _direction = Vector2.zero;
    }

    public override void DestroyWorldObject()
    {
        base.DestroyWorldObject();
        Stop();
    }
}
