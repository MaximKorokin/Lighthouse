using System;
using UnityEngine;

/// <summary>
/// Due to its "static" nature, Effect should not store any inner state
/// </summary>
public abstract class Effect : ScriptableObject
{
    [field: SerializeField]
    public string Name { get; private set; }

    public event Action Invoking;

    public virtual void Invoke(WorldObject source, WorldObject target)
    {
        Invoking?.Invoke();
    }
}
