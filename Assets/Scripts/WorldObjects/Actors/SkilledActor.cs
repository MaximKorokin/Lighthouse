using System.Collections.Generic;
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

        WorldObject.Stats.Modified += OnWorldObjectStatsModified;
        OnWorldObjectStatsModified();
    }

    private void OnWorldObjectStatsModified()
    {
        _skills.ForEach(x => x.CooldownCounter.CooldownDivider = WorldObject.AttackSpeed);
    }

    protected override void ActInternal(PrioritizedTargets targets)
    {
        base.ActInternal(targets);
        CastState.Target = targets.MainTarget;

        //_skills.Any(x => x.Invoke(CastState, targets));
        _skills.ForEach(x => x.Invoke(CastState, targets));
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
