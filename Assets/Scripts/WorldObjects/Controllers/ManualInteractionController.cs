using System.Linq;
using UnityEngine;

public class ManualInteractionController : TriggerController
{
    protected override void Control()
    {
        if (TriggeredWorldObjects.Any() && Input.GetKeyDown(KeyCode.E))
        {
            InvokeActors(new PrioritizedTargets(TriggeredWorldObjects));
        }
    }
}
