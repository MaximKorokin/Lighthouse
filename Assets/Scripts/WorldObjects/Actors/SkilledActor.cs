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

    protected override void ActInternal(WorldObject worldObject)
    {
        base.ActInternal(worldObject);
        _skills.ForEach(x => x.Invoke(WorldObject, worldObject, WorldObject.AttackSpeed));
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
