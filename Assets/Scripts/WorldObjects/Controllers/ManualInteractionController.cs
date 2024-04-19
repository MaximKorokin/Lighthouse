using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ManualInteractionController : TriggerController
{
    protected override void Control()
    {
        if (TriggeredWorldObjects.Any() && Input.GetKeyDown(KeyCode.E))
        {
            InvokeActors(TriggeredWorldObjects.First());
        }
    }

    protected override void Trigger(WorldObject worldObject, bool entered) { }
}
