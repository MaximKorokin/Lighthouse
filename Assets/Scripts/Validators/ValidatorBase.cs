using UnityEngine;

[RequireComponent(typeof(MovableWorldObject))]
public abstract class ValidatorBase : MonoBehaviour
{
    protected MovableWorldObject WorldObject { get; private set; }

    protected virtual void Awake()
    {
        WorldObject = GetComponent<MovableWorldObject>();
    }

    public abstract bool IsValidTarget(WorldObject worldObject);
}
