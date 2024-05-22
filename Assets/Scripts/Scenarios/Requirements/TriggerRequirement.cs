using System;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(WorldObjectTriggerDetector))]
public class TriggerRequirement : ActRequirement
{
    [SerializeField]
    private TriggerTarget _triggerTarget;
    [SerializeField]
    private TriggerOn _triggerOn;

    private TriggeredWorldObjectsCollection _triggeredObjectsCollection;

    private void Awake()
    {
        _triggeredObjectsCollection = new TriggeredWorldObjectsCollection(GetComponent<WorldObjectTriggerDetector>(), IsProperWorldObject);
        _triggeredObjectsCollection.Triggered += OnTriggered;
    }

    public override bool IsFulfilled()
    {
        return _triggeredObjectsCollection.Any();
    }

    private bool IsProperWorldObject(WorldObject worldObject)
    {
        return _triggerTarget switch
        {
            TriggerTarget.Player => worldObject is PlayerCreature,
            _ => false
        };
    }

    private void OnTriggered(WorldObject worldObject, bool entered)
    {
        if ((_triggerOn & TriggerOn.Enter) == TriggerOn.Enter && entered)
        {
            InvokeFulfilled();
        }
        else if ((_triggerOn & TriggerOn.Exit) == TriggerOn.Exit && !entered)
        {
            InvokeFulfilled();
        }
    }

    public override string IconName => "Waypoint.png";
}

public enum TriggerTarget
{
    Player,
}

public enum TriggerOn
{
    Enter = 1,
    Exit = 2,
    Both = 3,
}
