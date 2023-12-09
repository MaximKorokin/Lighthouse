using System.Collections.Generic;

public class SinglePerTargetActor : EffectActor
{
    private readonly HashSet<WorldObject> _uses = new();

    public override void Act(WorldObject worldObject)
    {
        if (_uses.Add(worldObject))
        {
            base.Act(worldObject);
        }
    }

    public override void Idle(WorldObject worldObject)
    {

    }
}