using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MovableWorldObject))]
public abstract class TargetController : TriggerController
{
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

    public abstract void ChooseTarget(ICollection<WorldObject> targets, TargetSearchingType targetType, WorldObject source, float yaw);

    public abstract void SetTarget(WorldObject worldObject, float yaw);
}

public enum TargetSearchingType
{
    Nearest,
    Random,
    Forward
}
