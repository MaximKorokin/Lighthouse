using System.Collections.Generic;
using UnityEngine;

public class ItemTriggerDetector : WorldObjectInteractingTriggerDetector
{
    private Item _item;
    private readonly HashSet<Collider2D> _collidersInactive = new();

    protected void Awake()
    {
        _item = RequireUtils.CastRequired<WorldObject, Item>(WorldObject);
        _item.Activated += () => _collidersInactive.ForEach(x => OnTriggerEnter2D(x));
    }

    public override void OnTriggerEnter2D(Collider2D collider)
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

    public override void OnTriggerExit2D(Collider2D collider)
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

    protected override bool IsValidTarget(WorldObject obj, DetectingVariant variant)
    {
        return _item.IsActive && base.IsValidTarget(obj, variant);
    }
}
