using UnityEngine;

[RequireComponent(typeof(WorldObject))]
public abstract class ActorBase : MonoBehaviour
{
    private WorldObject _worldObject;
    public WorldObject WorldObject { get => _worldObject = _worldObject != null ? _worldObject : GetComponent<WorldObject>(); }

    protected virtual void Awake()
    {

    }

    public abstract void Act(WorldObject worldObject);
}
