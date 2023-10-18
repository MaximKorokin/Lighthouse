using UnityEngine;

public class PlayerProjectileManipulator : ProjecltileManipulator
{
    public override bool IsValidTarget(WorldObject worldObject)
    {
        return worldObject is EnemyCreature enemyCreature && enemyCreature.IsAlive;
    }
}
