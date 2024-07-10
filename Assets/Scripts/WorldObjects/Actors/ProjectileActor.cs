using UnityEngine;

[RequireComponent(typeof(DestroyableWorldObject))]
public class ProjectileActor : EffectActor
{
    private ProjectileEffect _projectileEffect;
    private int _pierceLeft;
    private DestroyableWorldObject _destroyable;

    private readonly CooldownCounter _obstacleHitCooldown = new(.1f);

    protected override void Awake()
    {
        base.Awake();
        _destroyable = WorldObject as DestroyableWorldObject;
        _destroyable.Destroying += OnDestroying;
        _obstacleHitCooldown.Reset();
    }

    private void OnDestroying()
    {
        CastState.Target = WorldObject;
        _projectileEffect?.InvokeEnd(CastState);
    }

    protected override void ActInternal(WorldObject worldObject)
    {
        if (!_destroyable.IsAlive ||
            _pierceLeft <= 0 ||
            (worldObject.gameObject.IsObstacle() && !_obstacleHitCooldown.IsOver()))
        {
            return;
        }

        base.ActInternal(worldObject);

        if (--_pierceLeft <= 0 || worldObject.gameObject.IsObstacle())
        {
            _destroyable.DestroyWorldObject();
        }
    }

    public override void Idle(WorldObject worldObject)
    {

    }

    public void SetProjectileEffect(ProjectileEffect effect, CastState castState)
    {
        _projectileEffect = effect;
        _pierceLeft = effect.PierceAmount;
        SetEffects(effect.Effects, castState);
    }
}
