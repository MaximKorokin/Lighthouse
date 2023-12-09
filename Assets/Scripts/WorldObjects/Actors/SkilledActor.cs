using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(DestroyableWorldObject))]
public class SkilledActor : ActorBase
{
    [SerializeField]
    private List<EffectSettings> _effectsSettings;

    private List<Skill> _skills;

    protected override void Awake()
    {
        base.Awake();
        _skills = _effectsSettings.Select(x => new Skill(x)).ToList();
    }

    public override void Act(WorldObject worldObject)
    {
        if (!(WorldObject as DestroyableWorldObject).IsAlive)
        {
            return;
        }

        _skills.Where(x => x.CanUse(WorldObject.AttackSpeed)).ForEach(x => x.Invoke(WorldObject, worldObject));
    }

    public override void Idle(WorldObject worldObject)
    {

    }

    public void AddSkill(EffectSettings settings)
    {
        if (_effectsSettings.Contains(settings))
        {
            return;
        }
        _skills.Add(new Skill(settings));
    }
}
