using UnityEngine;

[RequireComponent(typeof(Item))]
public class ItemActor : EffectActor
{
    private Item _item;

    protected override void Awake()
    {
        base.Awake();
        _item = WorldObject as Item;
    }

    public override void Act(WorldObject worldObject)
    {
        if (!_item.IsActive || !_item.IsAlive)
        {
            return;
        }

        base.Act(worldObject);
        _item.DestroyWorldObject();
    }
}
