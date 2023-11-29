using UnityEngine;

public class Item : MovableWorldObject
{
    [field: SerializeField]
    public Effect Effect { get; private set; }

    public override void Act(WorldObject worldObject)
    {
        if (!IsAlive)
        {
            return;
        }

        if (Effect != null)
        {
            Effect.Invoke(new CastState(this, this, worldObject));
        }
        DestroyWorldObject();
    }
}
