using UnityEngine;

public class EnemyProjectileManipulator : ProjectileManipulator
{
    public override bool IsValidTarget(WorldObject worldObject)
    {
        return worldObject is PlayerCreature playerCreature && playerCreature.IsAlive;
    }
}
