using System;
using UnityEngine;

public abstract class TriggerDetectorBase<T> : MonoBehaviour
{
    [SerializeField]
    private TriggerType _triggerType;

    public event Action<T> TriggerEntered;
    public event Action<T> TriggerExited;

    protected virtual void OnTriggerEnter2D(Collider2D collider)
    {
        if (ValidateTarget(collider, out var obj))
        {
            TriggerEntered?.Invoke(obj);
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D collider)
    {
        if (ValidateTarget(collider, out var obj))
        {
            TriggerExited?.Invoke(obj);
        }
    }

    protected virtual bool ValidateTarget(Collider2D collision, out T result)
    {
        result = default;
        return (_triggerType == TriggerType.Triggers && collision.isTrigger) ||
            (_triggerType == TriggerType.Colliders && !collision.isTrigger);
    }
}

public enum TriggerType
{
    Colliders,
    Triggers
}
