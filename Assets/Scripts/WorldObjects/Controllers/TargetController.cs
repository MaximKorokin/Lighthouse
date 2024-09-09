using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(MovableWorldObject))]
public abstract class TargetController : TriggerController
{
    [SerializeField]
    private ValidTarget _primaryTargetTypes;

    protected MovableWorldObject MovableWorldObject { get; private set; }
    protected IEnumerable<WorldObject> PrimaryTargets => TriggeredWorldObjects.Where(IsPrimaryTarget);
    protected IEnumerable<WorldObject> SecondaryTargets => TriggeredWorldObjects.Except(PrimaryTargets);

    protected override void Awake()
    {
        base.Awake();
        MovableWorldObject = GetComponent<MovableWorldObject>();
    }

    protected override void Update()
    {
        if (MovableWorldObject.IsAlive)
        {
            base.Update();
        }
    }

    public bool IsPrimaryTarget(WorldObject worldObject)
    {
        return _primaryTargetTypes.IsValidTarget(worldObject);
    }

    public abstract void ChooseTarget(IEnumerable<WorldObject> targets, TargetSearchingType targetType, WorldObject source, float yaw);

    public abstract void SetTarget(WorldObject worldObject, float yaw);
}

public enum TargetSearchingType
{
    Nearest,
    Random,
    Forward
}
