using System;
using UnityEngine;

public class FrameBoundEvent<T>
{
    private readonly object _invoker;
    private Action<T> _event;
    private int _invokeFrame = int.MinValue;

    public FrameBoundEvent(object invoker)
    {
        _invoker = invoker;
    }

    /// <summary>
    /// Returns True if <see cref="Invoke"/> was called this or previous frame
    /// </summary>
    public bool HasOccured => _invokeFrame + 1 >= Time.frameCount;

    public T Value { get; private set; }

    public void Invoke(object invoker, T val)
    {
        if (_invokeFrame == Time.frameCount) return;

        if (_invoker == invoker)
        {
            _invokeFrame = Time.frameCount;
            Value = val;
            _event?.Invoke(val);
        }
        else
        {
            Logger.Error("Attempt of calling the event with wrong invoker");
        }
    }

    public static FrameBoundEvent<T> operator +(FrameBoundEvent<T> e, Action<T> action)
    {
        e._event += action;
        return e;
    }

    public static FrameBoundEvent<T> operator -(FrameBoundEvent<T> e, Action<T> action)
    {
        e._event -= action;
        return e;
    }
}
