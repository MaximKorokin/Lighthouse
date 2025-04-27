public class IdleTargetController : TargetController
{
    protected override void Control()
    {
        base.Control();
        if (Target == null)
        {
            return;
        }
        InvokeActors(new PrioritizedTargets(Target, TriggeredWorldObjects, PrimaryTargets, SecondaryTargets));
    }
}
