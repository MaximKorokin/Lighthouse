using UnityEngine;

[RequireComponent(typeof(MovableWorldObject))]
[RequireComponent(typeof(ActorBase))]
public abstract class ControllerBase : MonoBehaviour
{
    protected MovableWorldObject WorldObject { get; private set; }
    protected ActorBase[] Actors { get; private set; }

    protected virtual void Awake()
    {
        WorldObject = GetComponent<MovableWorldObject>();
        Actors = GetComponents<ActorBase>();
    }

    protected virtual void Update()
    {
        if (WorldObject.IsAlive)
        {
            Control();
        }
    }

    protected void InvokeActors(WorldObject worldObject) => Actors.ForEach(x => x.Act(worldObject));

    protected abstract void Control();
}
