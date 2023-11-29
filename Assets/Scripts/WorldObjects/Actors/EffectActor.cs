using UnityEngine;

[RequireComponent(typeof(DestroyableWorldObject))]
public abstract class EffectActor : ActorBase
{
    [field: SerializeField]
    protected Effect Effect { get; private set; }

    public override void Act(WorldObject worldObject)
    {
        if (Effect != null)
        {
            Effect.Invoke(new CastState(WorldObject, WorldObject, worldObject));
        }
    }
}
