using System.Linq;
using UnityEngine;

[RequireComponent(typeof(WorldObjectFindingTriggerDetector))]
public class TriggerRequirement : ActRequirement
{
    [SerializeField]
    private TriggerTarget _triggerTarget;
    [SerializeField]
    private TriggerOn _triggerOn;

    private TriggeredWorldObjectsCollection _triggeredObjectsCollection;

    private void Awake()
    {
        _triggeredObjectsCollection = new TriggeredWorldObjectsCollection(GetComponent<WorldObjectFindingTriggerDetector>(), IsProperWorldObject);
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
            TriggerTarget.Creature => worldObject is Creature,
            _ => false
        };
    }

    private void OnTriggered(WorldObject worldObject, bool entered)
    {
        if (_triggerOn.HasFlag(TriggerOn.Enter) && entered)
        {
            InvokeFulfilled();
        }
        else if (_triggerOn.HasFlag(TriggerOn.Exit) && !entered)
        {
            InvokeFulfilled();
        }
    }

    public override string IconName => "Waypoint.png";
}

public enum TriggerTarget
{
    Player,
    Creature,
}

public enum TriggerOn
{
    Enter = 1,
    Exit = 2,
    Both = 3,
}
