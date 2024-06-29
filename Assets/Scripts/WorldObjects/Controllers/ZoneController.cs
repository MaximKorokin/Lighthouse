using System.Linq;

public class ZoneController : TriggerController
{
    protected override void Control()
    {
        // Needs this copy because collection could be changed in progress of actors invoking
        TriggeredWorldObjects.ToArray().ForEach(x => InvokeActors(x));
    }

    protected override void Trigger(WorldObject worldObject, bool entered)
    {
        if (!entered)
        {
            IdleActors(worldObject);
        }
    }
}
