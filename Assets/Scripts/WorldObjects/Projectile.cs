using System.Collections;
using UnityEngine;

public class Projectile : MovableWorldObject
{
    [field: SerializeField]
    public float LifeTime { get; protected set; }

    public ProjectileEffect Effect { get; private set; }

    private int _pierceLeft;
    private CastState _castState;

    protected override void Awake()
    {
        base.Awake();
        StartCoroutine(LifeTimeCoroutine());
    }

    public override void DestroyWorldObject()
    {
        _castState.Target = this;
        Effect.InvokeEnd(_castState);
        base.DestroyWorldObject();
    }

    public override void Act(WorldObject worldObject)
    {
        _castState.Target = worldObject;
        Effect.InvokeEffects(_castState);

        if (--_pierceLeft <= 0)
        {
            DestroyWorldObject();
        }

        var destroyableWorldObject = worldObject as DestroyableWorldObject;
        Debug.Log($"Projectile {name} hit {destroyableWorldObject.name}");
    }

    public void SetEffect(ProjectileEffect effect, CastState castState)
    {
        Effect = effect;
        _pierceLeft = effect.PierceAmount;

        _castState = castState;
        _castState.Source = this;
    }

    private IEnumerator LifeTimeCoroutine()
    {
        yield return new WaitForSeconds(LifeTime);
        DestroyWorldObject();
    }
}
