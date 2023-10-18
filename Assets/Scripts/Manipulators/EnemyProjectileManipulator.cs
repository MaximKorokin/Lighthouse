using UnityEngine;

public class EnemyProjectileManipulator : ProjecltileManipulator
{
    public override bool IsValidTarget(WorldObject worldObject)
    {
        return worldObject is PlayerCreature playerCreature && playerCreature.IsAlive;
    }
}
