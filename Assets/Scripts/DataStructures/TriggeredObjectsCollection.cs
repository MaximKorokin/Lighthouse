using System;
using System.Collections;
using System.Collections.Generic;

public class TriggeredObjectsCollection<T> : IEnumerable<T>
{
    protected readonly HashSet<T> TriggeredObjects = new();
    private readonly TriggerDetectorBase<T> _detector;
    private readonly Func<T, bool> _additionalCondition;

    public event Action<T, bool> Triggered;

    public TriggeredObjectsCollection(TriggerDetectorBase<T> detector)
    {
        _detector = detector;
        _detector.TriggerEntered += OnTriggerEntered;
        _detector.TriggerExited += OnTriggerExited;
    }

    public TriggeredObjectsCollection(TriggerDetectorBase<T> detector, Func<T, bool> additionalCondition) : this(detector)
    {
        _additionalCondition = additionalCondition;
    }

    protected virtual void OnTriggerEntered(T obj)
    {
        if (_additionalCondition != null && !_additionalCondition(obj))
        {
            return;
        }
        TriggeredObjects.Add(obj);
        Triggered?.Invoke(obj, true);
    }

    protected virtual void OnTriggerExited(T obj)
    {
        TriggeredObjects.Remove(obj);
        Triggered?.Invoke(obj, false);
    }

    public IEnumerator<T> GetEnumerator()
    {
        return TriggeredObjects.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return TriggeredObjects.GetEnumerator();
    }

    ~TriggeredObjectsCollection()
    {
        _detector.TriggerEntered -= OnTriggerEntered;
        _detector.TriggerExited -= OnTriggerExited;
    }
}
