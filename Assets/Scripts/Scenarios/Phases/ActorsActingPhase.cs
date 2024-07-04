using System.Collections.Generic;
using UnityEngine;

public class ActorsActingPhase : ActPhase
{
    [SerializeField]
    private ActorBase[] _actors;
    [SerializeField]
    private ActorActionType _type;

    public IEnumerable<ActorBase> Actors => _actors;

    public override void Invoke()
    {
        if (_type == ActorActionType.Act)
        {
            _actors.ForEach(x => x.Act(x.WorldObject));
        }
        else if (_type == ActorActionType.Idle)
        {
            _actors.ForEach(x => x.Idle(x.WorldObject));
        }
        InvokeFinished();
    }

    public override string IconName => "Gear.png";
    public override Color IconColor => _type == ActorActionType.Act ? MyColors.Green : MyColors.Red;
}

public enum ActorActionType
{
    Act,
    Idle,
}
