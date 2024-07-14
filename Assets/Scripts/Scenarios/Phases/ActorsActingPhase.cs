using System.Collections.Generic;
using UnityEngine;

public class ActorsActingPhase : ActPhase
{
    [SerializeField]
    private ActorBase[] _actors;

    public IEnumerable<ActorBase> Actors => _actors;

    public override void Invoke()
    {
        _actors.ForEach(x => x.Act(new PrioritizedTargets(x.WorldObject.Yield())));
        InvokeFinished();
    }

    public override string IconName => "Gear.png";
}
