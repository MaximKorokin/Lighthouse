using UnityEngine;

[RequireComponent(typeof(MovableWorldObject))]
[RequireComponent(typeof(ValidatorBase))]
public abstract class ControllerBase : MonoBehaviour
{
    protected MovableWorldObject WorldObject { get; private set; }
    protected ValidatorBase Validator { get; private set; }

    protected virtual void Awake()
    {
        WorldObject = GetComponent<MovableWorldObject>();
        Validator = GetComponent<ValidatorBase>();
    }

    protected virtual void Update()
    {
        if (WorldObject.IsAlive)
        {
            Control();
        }
    }

    protected abstract void Control();
}
