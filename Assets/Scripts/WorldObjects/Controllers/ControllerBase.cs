using UnityEngine;

public abstract class ControllerBase : MonoBehaviour
{
    public BoolCounter _canControl = new(true);
    public bool CanControl { get => _canControl; set => _canControl.Set(value); }

    protected WorldObject WorldObject { get; private set; }
    protected ActorBase[] Actors { get; private set; }

    protected virtual void Awake()
    {
        WorldObject = this.GetRequiredComponent<WorldObject>();
        Actors = GetComponents<ActorBase>() ?? new ActorBase[0];
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
