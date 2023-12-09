using System;
using UnityEngine;

/// <summary>
/// Used to define if world object could be targeted.
/// </summary>
[RequireComponent(typeof(WorldObject))]
public abstract class ValidatorBase : MonoBehaviour
{
    [SerializeField]
    private ValidType _validTypes;

    protected WorldObject WorldObject { get; private set; }

    protected virtual void Awake()
    {
        WorldObject = GetComponent<WorldObject>();
    }

    public virtual bool IsValidTarget(WorldObject worldObject)
    {
        if (_validTypes == ValidType.None)
        {
            return false;
        }
        if ((_validTypes & ValidType.Enemy) == ValidType.Enemy && worldObject is EnemyCreature)
        {
            return true;
        }
        if ((_validTypes & ValidType.Player) == ValidType.Player && worldObject is PlayerCreature)
        {
            return true;
        }
        if ((_validTypes & ValidType.Obstacle) == ValidType.Obstacle && worldObject is Obstacle)
        {
            return true;
        }
        if ((_validTypes & ValidType.DestroyableObstacle) == ValidType.DestroyableObstacle && worldObject is DestroyableObstacle)
        {
            return true;
        }
        if ((_validTypes & ValidType.TemporaryWorldObject) == ValidType.TemporaryWorldObject && worldObject is TemporaryWorldObject)
        {
            return true;
        }
        return false;
    }
}

[Flags]
public enum ValidType
{
    None = 0,
    Enemy = 1,
    Player = 2,
    Obstacle = 4,
    DestroyableObstacle = 8,
    TemporaryWorldObject = 16,
}
