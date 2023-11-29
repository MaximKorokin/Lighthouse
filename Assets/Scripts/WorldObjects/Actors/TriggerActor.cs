using UnityEngine;

[RequireComponent(typeof(WorldObjectTriggerDetector))]
public class TriggerActor : EffectActor
{
    private void Awake()
    {
        var triggerDetector = GetComponent<WorldObjectTriggerDetector>();
        triggerDetector.TriggerEntered += Act;
        triggerDetector.TriggerExited += Interrupt;
    }

    private void Interrupt(WorldObject worldObject)
    {

    }
}
