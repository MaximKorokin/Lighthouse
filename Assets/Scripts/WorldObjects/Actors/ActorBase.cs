using System;
using UnityEngine;

[RequireComponent(typeof(WorldObject))]
public abstract class ActorBase : MonoBehaviour
{
    public BoolCounter _canAct = new(true);
    public bool CanAct { get => _canAct; set => _canAct.Set(value); }

    private WorldObject _worldObject;
    public WorldObject WorldObject { get => _worldObject = _worldObject != null ? _worldObject : GetComponent<WorldObject>(); }

    public event Action Acting;

    protected virtual void Awake()
    {

    }

    public void Act(WorldObject worldObject)
    {
        if (CanAct)
        {
            ActInternal(worldObject);
        }
    }

    protected virtual void ActInternal(WorldObject worldObject)
    {
        Acting?.Invoke();
    }

    public abstract void Idle(WorldObject worldObject);
}
