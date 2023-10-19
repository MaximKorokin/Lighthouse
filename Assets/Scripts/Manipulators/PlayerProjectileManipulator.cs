using UnityEngine;

public class PlayerProjectileManipulator : ProjectileManipulator
{
    public override bool IsValidTarget(WorldObject worldObject)
    {
        return worldObject is EnemyCreature enemyCreature && enemyCreature.IsAlive;
    }
}
