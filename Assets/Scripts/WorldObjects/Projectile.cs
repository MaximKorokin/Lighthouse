using System.Collections;
using UnityEngine;

public class Projectile : MovableWorldObject
{
    [field: SerializeField]
    public float LifeTime { get; protected set; }

    public ProjectileEffect Effect { get; private set; }

    private int _pierceLeft;

    protected override void Awake()
    {
        base.Awake();
        StartCoroutine(LifeTimeCoroutine());
    }

    public override void DestroyWorldObject()
    {
        Effect?.InvokeEnd(this, this);
        base.DestroyWorldObject();
    }

    public override void Act(WorldObject worldObject)
    {
        Effect.InvokeEffects(this, worldObject);

        if (_pierceLeft <= 0)
        {
            DestroyWorldObject();
        }

        var destroyableWorldObject = worldObject as DestroyableWorldObject;
        Debug.Log($"Projectile {name} hit {destroyableWorldObject.name}");
    }

    public void SetEffect(ProjectileEffect effect)
    {
        Effect = effect;
        _pierceLeft = effect.PierceAmount;
    }

    private IEnumerator LifeTimeCoroutine()
    {
        yield return new WaitForSeconds(LifeTime);
        DestroyWorldObject();
    }
}
