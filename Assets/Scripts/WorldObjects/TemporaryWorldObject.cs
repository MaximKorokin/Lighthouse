using UnityEngine;

public class TemporaryWorldObject : MovableWorldObject
{
    [field: SerializeField]
    public float LifeTime { get; set; }

    protected override void Awake()
    {
        base.Awake();
        // Calling coroutine from outside because it will stop if this GameObject would be disabled
        CoroutinesHandler.StartUniqueCoroutine(this, CoroutinesUtils.WaitForSeconds(LifeTime), DestroyWorldObject);
    }

    // Overriding because GameObject may be destroyed earlier (projectile collided with target)
    public override void DestroyWorldObject()
    {
        if (this != null) base.DestroyWorldObject();
    }
}
