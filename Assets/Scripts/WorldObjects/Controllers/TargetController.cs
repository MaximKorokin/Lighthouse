using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(MovableWorldObject))]
public abstract class TargetController : TriggerController
{
    public const int HighPriorityIndex = 0;
    public const int LowPriorityIndex = 1;

    [SerializeField]
    private ValidTarget[] _priorities;

    protected MovableWorldObject MovableWorldObject { get; private set; }

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

    public bool IsValidTarget(WorldObject worldObject, int priority)
    {
        return _priorities == null
            || _priorities.Length == 0
            || _priorities.Skip(priority).Take(1).Any(x => x.IsValidTarget(worldObject));
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
