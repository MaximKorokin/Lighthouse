using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DestroyableWorldObject))]
public class SkilledActor : ActorBase
{
    [SerializeField]
    private List<Skill> _skills;

    public IEnumerable<Skill> Skills => _skills;

    protected override void Awake()
    {
        base.Awake();
        _skills.ForEach(x => x.Initialize());
    }

    protected override void ActInternal(WorldObject worldObject)
    {
        base.ActInternal(worldObject);
        _skills.ForEach(x => x.Invoke(WorldObject, worldObject, WorldObject.AttackSpeed));
    }

    public override void Idle(WorldObject worldObject)
    {

    }

    public void AddSkill(Skill skill)
    {
        if (_skills.Contains(skill))
        {
            return;
        }
        _skills.Add(skill);
    }
}
