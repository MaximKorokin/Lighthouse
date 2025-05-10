using UnityEngine;

public class ProjectileActor : SkilledActor
{
    private ProjectileEffect _projectileEffect;
    private int _pierceLeft;
    private DestroyableWorldObject _destroyable;

    private readonly CooldownCounter _obstacleHitCooldown = new(.1f);

    protected override void Awake()
    {
        base.Awake();
        _destroyable = RequireUtils.CastRequired<WorldObject, DestroyableWorldObject>(WorldObject);
        _destroyable.Destroying += OnDestroying;
        _obstacleHitCooldown.Reset();
    }

    private void OnDestroying()
    {
        CastState.Target = WorldObject;
        _projectileEffect?.InvokeEnd(CastState);
    }

    protected override void ActInternal(PrioritizedTargets targets)
    {
        if (!_destroyable.IsAlive ||
            _pierceLeft <= 0 ||
            targets.MainTarget == null ||
            (targets.MainTarget.gameObject.IsObstacle() && !_obstacleHitCooldown.IsOver()))
        {
            return;
        }

        base.ActInternal(targets);

        if (--_pierceLeft <= 0 || targets.MainTarget.gameObject.IsObstacle())
        {
            _destroyable.DestroyWorldObject();
        }
    }

    public void SetProjectileEffect(ProjectileEffect effect, CastState castState)
    {
        _projectileEffect = effect;
        _pierceLeft = effect.PierceAmount;
        AddSkill(new Skill(effect.Effects, 0));
        SetCastState(castState);
    }
}
