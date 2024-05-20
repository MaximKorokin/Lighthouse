using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Item))]
public class ItemTriggerDetector : ValidatingTriggerDetector
{
    private Item _item;
    private readonly HashSet<Collider2D> _collidersInactive = new();

    protected override void Awake()
    {
        base.Awake();
        _item = WorldObject as Item;
        _item.Activated += () => _collidersInactive.ForEach(x => OnTriggerEnter2D(x));
    }

    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        if (_item.IsActive)
        {
            base.OnTriggerEnter2D(collider);
        }
        else
        {
            _collidersInactive.Add(collider);
        }
    }

    protected override void OnTriggerExit2D(Collider2D collider)
    {
        if (_item.IsActive)
        {
            base.OnTriggerExit2D(collider);
        }
        else
        {
            _collidersInactive.Remove(collider);
        }
    }

    protected override bool ValidateTarget(Collider2D collision, out WorldObject worldObject)
    {
        if (_item.IsActive && base.ValidateTarget(collision, out worldObject))
        {
            return true;
        }
        worldObject = null;
        return false;
    }
}
