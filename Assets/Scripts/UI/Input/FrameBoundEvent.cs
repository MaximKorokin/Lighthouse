﻿using System;
using UnityEngine;

public class FrameBoundEvent<T>
{
    private readonly object _invoker;
    private Action<T> _event;
    private int _invokeFrame;

    public FrameBoundEvent(object invoker)
    {
        _invoker = invoker;
    }

    public bool HasOccured => _invokeFrame == Time.frameCount;

    public void Invoke(object invoker, T val)
    {
        if (_invoker == invoker)
        {
            _invokeFrame = Time.frameCount;
            _event?.Invoke(val);
        }
        else
        {
            Logger.Error("Event is trying to be called with wrong invoker");
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