using System.Linq;

public class ZoneController : TriggerController
{
    protected override void Control()
    {
        InvokeActors(new PrioritizedTargets(TriggeredWorldObjects.ToArray()));
    }
}
