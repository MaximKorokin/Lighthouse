using UnityEngine;

[RequireComponent(typeof(WorldObject))]
public abstract class ActorBase : MonoBehaviour
{
    [field: SerializeField]
    public bool CanAct { get; set; } = true;
    private WorldObject _worldObject;
    public WorldObject WorldObject { get => _worldObject = _worldObject != null ? _worldObject : GetComponent<WorldObject>(); }

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

    protected abstract void ActInternal(WorldObject worldObject);
    public abstract void Idle(WorldObject worldObject);
}
