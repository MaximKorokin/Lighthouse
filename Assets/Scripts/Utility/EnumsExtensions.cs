using UnityEngine;

public static class EnumsExtensions
{
    public static bool IsValidTarget(this ValidTarget target, WorldObject worldObject)
    {
        return target.HasFlag(ValidTarget.Creature) && (worldObject is Creature)
            || target.HasFlag(ValidTarget.DestroyableObstacle) && (worldObject is DestroyableObstacle)
            || target.HasFlag(ValidTarget.Obstacle) && (worldObject is Obstacle)
            || target.HasFlag(ValidTarget.TemporaryWorldObject) && (worldObject is TemporaryWorldObject);
    }

    public static bool IsValidRelation(this FactionsRelation relation, WorldObject source, WorldObject target)
    {
        return relation.HasFlag(FactionsRelation.Neutral) && source.Faction.IsNeutralTo(target.Faction)
            || relation.HasFlag(FactionsRelation.Ally) && source.Faction.IsAllyTo(target.Faction)
            || relation.HasFlag(FactionsRelation.Enemy) && source.Faction.IsEnemyTo(target.Faction);
    }

    public static bool IsValidTriggerType(this TriggerType triggerType, Collider2D collider)
    {
        return (triggerType.HasFlag(TriggerType.Triggers) && collider.isTrigger)
            || (triggerType.HasFlag(TriggerType.Colliders) && !collider.isTrigger);
    }
}
