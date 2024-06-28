using System;
using System.Linq;
using UnityEngine;

public abstract class TriggerDetectorBase<T> : MonoBehaviour
{
    [SerializeField]
    private DetectingVariant[] _variants;

    public event Action<T> TriggerEntered;
    public event Action<T> TriggerExited;

    protected virtual void OnTriggerEnter2D(Collider2D collider)
    {
        T obj = default;
        if (_variants.Any(x =>
            x.TriggerType.IsValidTriggerType(collider) &&
            TryGetTargetingObject(collider, out obj) &&
            IsValidTarget(obj, x)))
        {
            TriggerEntered?.Invoke(obj);
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D collider)
    {
        T obj = default;
        if (_variants.Any(x =>
            x.TriggerType.IsValidTriggerType(collider) &&
            TryGetTargetingObject(collider, out obj) &&
            IsValidTarget(obj, x)))
        {
            TriggerExited?.Invoke(obj);
        }
    }

    protected abstract bool TryGetTargetingObject(Collider2D collision, out T result);

    protected abstract bool IsValidTarget(T obj, DetectingVariant variant);

    public bool IsValidTarget(T obj)
    {
        return _variants.Any(x => IsValidTarget(obj, x));
    }
}

[Flags]
public enum TriggerType
{
    Colliders = 1,
    Triggers = 2,
}

[Flags]
public enum ValidTarget
{
    Creature = 1,
    DestroyableObstacle = 2,
    Obstacle = 4,
    TemporaryWorldObject = 8,
}

[Serializable]
public partial struct DetectingVariant
{
    public TriggerType TriggerType;
    public ValidTarget ValidTargets;
    public FactionsRelation Relation;
}
