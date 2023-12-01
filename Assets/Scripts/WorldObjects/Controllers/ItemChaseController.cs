using UnityEngine;

[RequireComponent(typeof(Item))]
class ItemChaseController : ChaseController
{
    [SerializeField]
    [Range(0, 1)]
    private float _drag;
    private Item _item;

    protected override void Awake()
    {
        base.Awake();
        _item = GetComponent<Item>();
    }

    protected override void Control()
    {
        if (_item.IsActive)
        {
            base.Control();
        }
        else
        {
            _item.Direction *= 1 - _drag;
            if (_item.Direction == Vector2.zero)
            {
                _item.Stop();
            }
            else
            {
                _item.Move();
            }
        }
    }
}