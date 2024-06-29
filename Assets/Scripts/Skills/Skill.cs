using System;
using UnityEngine;

[Serializable]
public class Skill : IInitializable
{
    [SerializeField]
    private EffectSettings _settings;
    [SerializeField]
    private SkillConditionData _conditionData;

    private Effect[] _effects;

    public CooldownCounter CooldownCounter { get; private set; }

    public void Initialize()
    {
        if (_settings == null)
        {
            return;
        }
        _effects = _settings.GetEffects();
        CooldownCounter = new CooldownCounter(_settings.Cooldown);
        CooldownCounter.Reset();
    }

    public void Invoke(WorldObject source, WorldObject target, float divider = 1)
    {
        if (!_conditionData.EvaluateCondition(source, target))
        {
            return;
        }

        if (CooldownCounter == null || !CooldownCounter.TryReset(divider))
        {
            return;
        }

        foreach (var effect in _effects)
        {
            effect.Invoke(new CastState(source, source, target));
        }
    }
}
