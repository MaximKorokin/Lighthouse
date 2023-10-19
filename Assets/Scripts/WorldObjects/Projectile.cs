using System.Collections;
using UnityEngine;

public class Projectile : MovableWorldObject
{
    [field: SerializeField]
    public float LifeTime { get; protected set; }
    [field: SerializeField]
    public ProjectileEffect Effect { get; protected set; }

    protected override void Awake()
    {
        base.Awake();
        StartCoroutine(LifeTimeCoroutine());
    }

    private IEnumerator LifeTimeCoroutine()
    {
        yield return new WaitForSeconds(LifeTime);
        DestroyWorldObject();
    }

    public override void DestroyWorldObject()
    {
        Effect.Invoke(this, this);
        base.DestroyWorldObject();
    }
}
