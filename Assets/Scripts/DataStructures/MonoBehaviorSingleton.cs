﻿using UnityEngine;

public abstract class MonoBehaviorSingleton<T> : MonoBehaviour where T : MonoBehaviorSingleton<T>
{
    public static T Instance { get; protected set; }

    protected virtual void Awake()
    {
        Instance = (T)this;
    }
}
