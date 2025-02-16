using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkilledActor : ActorBase
{
    [SerializeField]
    private List<Skill> _skills;
    
    protected CastState CastState;

    public IEnumerable<Skill> Skills => _skills;

    protected virtual void Awake()
    {
        _skills.ForEach(x => x.Initialize());
        CastState = new CastState(WorldObject);
    }

    protected override void ActInternal(PrioritizedTargets targets)
    {
        base.ActInternal(targets);
        CastState.Target = targets.MainTarget;

        //_skills.Any(x => x.Invoke(CastState, targets, WorldObject.AttackSpeed));
        _skills.ForEach(x => x.Invoke(CastState, targets, WorldObject.AttackSpeed));
    }

    public void AddSkill(Skill skill)
    {
        if (_skills.Contains(skill))
        {
            return;
        }
        _skills.Add(skill);
    }

    public void SetCastState(CastState castState)
    {
        CastState = castState;
        CastState.Source = WorldObject;
    }
}
