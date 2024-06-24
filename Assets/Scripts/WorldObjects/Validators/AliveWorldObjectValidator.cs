public class AliveWorldObjectValidator : WorldObjectValidator
{
    public override bool IsValidTarget(WorldObject worldObject, FactionsRelation relation)
    {
        if (worldObject is DestroyableWorldObject destroyable && !destroyable.IsAlive)
        {
            return false;
        }
        return base.IsValidTarget(worldObject, relation);
    }
}
