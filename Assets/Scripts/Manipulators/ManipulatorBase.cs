using UnityEngine;

[RequireComponent(typeof(MovableWorldObject))]
public abstract class ManipulatorBase : MonoBehaviour
{
    protected MovableWorldObject WorldObject { get; private set; }

    protected virtual void Awake()
    {
        WorldObject = GetComponent<MovableWorldObject>();
    }

    public abstract void Manipulate(WorldObject worldObject);

    public abstract bool IsValidTarget(WorldObject worldObject);
}
