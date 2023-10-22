using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Creature : MovableWorldObject
{
    [SerializeField]
    private List<Skill> _skills;
    public IEnumerable<Skill> Skills => _skills;

    private Dictionary<Skill, float> _skillsUsedTime;


    protected override void Awake()
    {
        base.Awake();
        _skillsUsedTime = Skills.ToDictionary(s => s, s => float.NegativeInfinity);
    }

    public override void Act(WorldObject worldObject)
    {
        Skills.Where(CanUseSkill).ForEach(UseSkill);
    }

    private bool CanUseSkill(Skill skill)
    {
        return Time.time - _skillsUsedTime[skill] > skill.Cooldown;
    }

    private void UseSkill(Skill skill)
    {
        _skillsUsedTime[skill] = Time.time;
        skill.Invoke(this);
    }

    public void AddSkill(Skill skill)
    {
        _skills.Add(skill);
        _skillsUsedTime.Add(skill, float.NegativeInfinity);
    }
}
