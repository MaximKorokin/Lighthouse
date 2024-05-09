using System.Linq;
using UnityEngine;

[RequireComponent(typeof(WorldObjectTriggerDetector))]
public class ActTriggerRequirement : ActRequirement
{
    [SerializeField]
    private TriggerTarget _triggerTarget;

    private TriggeredWorldObjectsCollection _triggeredObjectsCollection;

    private void Awake()
    {
        _triggeredObjectsCollection = new TriggeredWorldObjectsCollection(GetComponent<WorldObjectTriggerDetector>(), IsRightWorldObject);
        _triggeredObjectsCollection.Triggered += OnTriggered;
    }

    public override bool IsFulfilled()
    {
        return _triggeredObjectsCollection.Any();
    }

    private bool IsRightWorldObject(WorldObject worldObject)
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
}

public enum TriggerTarget
{
    Player,
}
