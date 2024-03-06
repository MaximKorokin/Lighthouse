using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ManualInteractionController : TriggerController
{
    private readonly HashSet<WorldObject> _targets = new();

    protected override void Control()
    {
        if (_targets.Any() && Input.GetKeyDown(KeyCode.E))
        {
            InvokeActors(_targets.First());
        }
    }

    protected override void Trigger(WorldObject worldObject, bool entered)
    {
        if (entered)
        {
            _targets.Add(worldObject);
        }
        else
        {
            _targets.Remove(worldObject);
        }
    }
}
