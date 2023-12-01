using UnityEngine;

[RequireComponent(typeof(DestroyableWorldObject))]
public class ProjectileActor : ActorBase
{
    public ProjectileEffect Effect { get; private set; }

    private int _pierceLeft;
    private CastState _castState;

    protected override void Awake()
    {
        base.Awake();
        (WorldObject as DestroyableWorldObject).Destroying += OnDestroying;
    }

    private void OnDestroying()
    {
        _castState.Target = WorldObject;
        Effect.InvokeEnd(_castState);
    }

    public override void Act(WorldObject worldObject)
    {
        if (!(WorldObject as DestroyableWorldObject).IsAlive || _pierceLeft <= 0)
        {
            return;
        }

        _castState.Target = worldObject;
        Effect.InvokeEffects(_castState);

        if (--_pierceLeft <= 0)
        {
            (WorldObject as DestroyableWorldObject).DestroyWorldObject();
        }
    }

    public void SetEffect(ProjectileEffect effect, CastState castState)
    {
        Effect = effect;
        _pierceLeft = effect.PierceAmount;

        _castState = castState;
        _castState.Source = WorldObject;
    }
}
