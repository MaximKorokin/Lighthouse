public class AliveValidator : ValidatorBase
{
    public override bool IsValidTarget(WorldObject worldObject, FactionsRelation relation)
    {
        var destroyableWorldObject = worldObject as DestroyableWorldObject;
        if (destroyableWorldObject == null || !destroyableWorldObject.IsAlive)
        {
            return false;
        }
        return base.IsValidTarget(worldObject, relation);
    }
}