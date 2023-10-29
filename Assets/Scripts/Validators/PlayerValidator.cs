public class PlayerValidator : ValidatorBase
{
    public override bool IsValidTarget(WorldObject worldObject)
    {
        return worldObject is PlayerCreature playerCreature && playerCreature.IsAlive;
    }
}
