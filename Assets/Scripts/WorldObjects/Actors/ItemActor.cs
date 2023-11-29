using UnityEngine;

[RequireComponent(typeof(DestroyableWorldObject))]
public class ItemActor : EffectActor
{
    public override void Act(WorldObject worldObject)
    {
        var destroyable = WorldObject as DestroyableWorldObject;
        if (!destroyable.IsAlive)
        {
            return;
        }

        base.Act(worldObject);
        destroyable.DestroyWorldObject();
    }
}
