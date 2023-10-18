using UnityEngine;

[RequireComponent(typeof(MovableWorldObject))]
[RequireComponent(typeof(ManipulatorBase))]
public abstract class ControllerBase : MonoBehaviour
{
    protected MovableWorldObject WorldObject { get; private set; }
    protected ManipulatorBase Manipulator { get; private set; }

    protected virtual void Awake()
    {
        WorldObject = GetComponent<MovableWorldObject>();
        Manipulator = GetComponent<ManipulatorBase>();
    }
}
