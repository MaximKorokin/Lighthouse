public class EnemyValidator : ValidatorBase
{
    public override bool IsValidTarget(WorldObject worldObject)
    {
        return worldObject is EnemyCreature enemyCreature && enemyCreature.IsAlive;
    }
}
