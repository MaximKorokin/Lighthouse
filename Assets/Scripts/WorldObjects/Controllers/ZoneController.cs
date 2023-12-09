using System.Collections.Generic;

public class ZoneController : TriggerController
{
    private readonly HashSet<WorldObject> _triggeredWorldObjects = new();

    protected override void Control()
    {
        _triggeredWorldObjects.ForEach(x => InvokeActors(x));
    }

    protected override void Trigger(WorldObject worldObject, bool entered)
    {
        if (entered)
        {
            _triggeredWorldObjects.Add(worldObject);
        }
        else
        {
            _triggeredWorldObjects.Remove(worldObject);
            IdleActors(worldObject);
        }
    }
}