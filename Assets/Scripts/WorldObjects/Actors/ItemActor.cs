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

    protected override void ActInternal(WorldObject worldObject)
    {
        if (!_item.IsActive || !_item.IsAlive)
        {
            return;
        }

        base.ActInternal(worldObject);
        _item.DestroyWorldObject();
    }

    public override void Idle(WorldObject worldObject)
    {
        _item.DestroyWorldObject();
    }
}
