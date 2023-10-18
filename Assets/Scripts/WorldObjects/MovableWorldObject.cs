using UnityEngine;

public abstract class MovableWorldObject : DestroyableWorldObject
{
    private Vector2 _direction;

    protected virtual void Update()
    {
        transform.Translate(_direction * Stats[StatName.Speed] * Time.deltaTime);
    }

    public void Move(Vector2 direction)
    {
        _direction = direction.normalized;
    }

    public void Stop()
    {
        _direction = Vector2.zero;
    }
}
