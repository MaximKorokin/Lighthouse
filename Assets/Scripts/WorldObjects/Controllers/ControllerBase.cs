using UnityEngine;

[RequireComponent(typeof(WorldObject))]
[RequireComponent(typeof(ActorBase))]
public abstract class ControllerBase : MonoBehaviour
{
    public BoolCounter _canControl = new(true);
    public bool CanControl { get => _canControl; set => _canControl.Set(value); }

    protected WorldObject WorldObject { get; private set; }
    protected ActorBase[] Actors { get; private set; }

    protected virtual void Awake()
    {
        WorldObject = GetComponent<WorldObject>();
        Actors = GetComponents<ActorBase>();
    }

    protected virtual void Update()
    {
        if (CanControl)
        {
            Control();
        }
    }

    protected void InvokeActors(PrioritizedTargets targets) => Actors.ForEach(x => x.Act(targets));

    protected abstract void Control();
}
