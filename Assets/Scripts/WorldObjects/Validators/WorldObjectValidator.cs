using System;
using UnityEngine;

/// <summary>
/// Used to define if world object could be targeted.
/// </summary>
[RequireComponent(typeof(WorldObject))]
public abstract class WorldObjectValidator : MonoBehaviour
{
    [SerializeField]
    private ValidTarget _validTargets;

    private WorldObject _worldObject;
    protected WorldObject WorldObject
    {
        get
        {
            if (_worldObject == null) _worldObject = GetComponent<WorldObject>();
            return _worldObject;
        } 
    }

    public virtual bool IsValidTarget(WorldObject worldObject, FactionsRelation relation)
    {
        var isRelationValid = 
            relation.HasFlag(FactionsRelation.Neutral) && WorldObject.Faction.IsNeutralTo(worldObject.Faction) ||
            relation.HasFlag(FactionsRelation.Ally) && WorldObject.Faction.IsAllyTo(worldObject.Faction) ||
            relation.HasFlag(FactionsRelation.Enemy) && WorldObject.Faction.IsEnemyTo(worldObject.Faction);

        var isFactionValid = 
            _validTargets.HasFlag(ValidTarget.Creature) && (worldObject is NPC or PlayerCreature) ||
            _validTargets.HasFlag(ValidTarget.DestroyableObstacle) && (worldObject is DestroyableObstacle) ||
            _validTargets.HasFlag(ValidTarget.Obstacle) && (worldObject is Obstacle);

        return isRelationValid && isFactionValid;
    }
}

[Flags]
public enum ValidTarget
{
    Creature = 1,
    DestroyableObstacle = 2,
    Obstacle = 4,
}
