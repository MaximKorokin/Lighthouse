using System.Linq;
using UnityEngine;

[RequireComponent(typeof(WorldObjectTriggerDetector))]
public class TriggerRequirement : ActRequirement
{
    [SerializeField]
    private TriggerTarget _triggerTarget;

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
        if (entered)
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
