using System.Linq;
using UnityEngine;

[RequireComponent(typeof(MovableWorldObject))]
[RequireComponent(typeof(ValidatorBase))]
public abstract class ControllerBase : MonoBehaviour
{
    protected MovableWorldObject WorldObject { get; private set; }
    protected ValidatorBase Validator { get; private set; }

    protected virtual void Awake()
    {
        WorldObject = GetComponent<MovableWorldObject>();
        Validator = GetComponent<ValidatorBase>();
    }

    protected virtual void Update()
    {
        if (WorldObject.IsAlive)
        {
            Control();
        }
    }

    public bool TryFindTarget(TargetType targetType, WorldObject source, float yaw)
    {
        var worldObjects = Physics2DUtils.GetWorldObjectsInRadius(transform.position, source.ActionRange)
            .Where(w => Validator.IsValidTarget(w))
            .ToArray();
        if (worldObjects.Length > 0)
        {
            ChooseTarget(worldObjects, targetType, source, yaw);
            return true;
        }
        else
        {
            return false;
        }
    }

    protected abstract void ChooseTarget(WorldObject[] targets, TargetType targetType, WorldObject source, float yaw);

    public abstract void SetTarget(WorldObject worldObject, float yaw);

    protected abstract void Control();
}
