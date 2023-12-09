using UnityEngine;

[RequireComponent(typeof(DestroyableWorldObject))]
public class ProjectileActor : ActorBase
{
    public ProjectileEffect Effect { get; private set; }

    private int _pierceLeft;
    private CastState _castState;
    private DestroyableWorldObject _destroyable;

    protected override void Awake()
    {
        base.Awake();
        _destroyable = (WorldObject as DestroyableWorldObject);
        _destroyable.Destroying += OnDestroying;
    }

    private void OnDestroying()
    {
        _castState.Target = WorldObject;
        Effect.InvokeEnd(_castState);
    }

    public override void Act(WorldObject worldObject)
    {
        if (!_destroyable.IsAlive || _pierceLeft <= 0)
        {
            return;
        }

        _castState.Target = worldObject;
        Effect.InvokeEffects(_castState);

        if (--_pierceLeft <= 0)
        {
            _destroyable.DestroyWorldObject();
        }
    }

    public override void Idle(WorldObject worldObject)
    {
        _destroyable.DestroyWorldObject();
    }

    public void SetEffect(ProjectileEffect effect, CastState castState)
    {
        Effect = effect;
        _pierceLeft = effect.PierceAmount;

        _castState = castState;
        _castState.Source = WorldObject;
    }
}
