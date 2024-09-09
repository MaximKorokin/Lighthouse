using UnityEngine;

public class ActorActedRequirement : ActRequirement
{
    [SerializeField]
    private ActorBase _actor;
    public ActorBase Actor => _actor;

    private bool _hasActed;

    private void Awake()
    {
        _actor.Acting += OnActorActing;
        _actor.WorldObject.OnDestroyed(() => _actor.Acting -= OnActorActing);
    }

    private void OnActorActing()
    {
        _hasActed = true;
        InvokeFulfilled();
    }

    public override bool IsFulfilled()
    {
        return _hasActed;
    }

    public override string IconName => "Action.png";
}
