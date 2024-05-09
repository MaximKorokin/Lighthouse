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
        var isRelationValid = relation switch
        {
            FactionsRelation.Ally => WorldObject.Faction.IsAllyTo(worldObject.Faction),
            FactionsRelation.Neutral => WorldObject.Faction.IsNeutralTo(worldObject.Faction),
            FactionsRelation.Enemy => WorldObject.Faction.IsEnemyTo(worldObject.Faction),
            _ => false
        };

        return isRelationValid && worldObject switch
        {
            NPC or PlayerCreature => (_validTargets & ValidTarget.Creature) == ValidTarget.Creature,
            DestroyableWorldObject => (_validTargets & ValidTarget.DestroyableObstacle) == ValidTarget.DestroyableObstacle,
            _ => false
        };
    }
}

[Flags]
public enum ValidTarget
{
    None = 0,
    Creature = 1,
    DestroyableObstacle = 2,
}
