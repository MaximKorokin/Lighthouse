using System;
using UnityEngine;

[Serializable]
public class Skill : IInitializable
{
    [SerializeField]
    private EffectSettings _settings;
    [SerializeField]
    private bool _startsInCooldown;
    [SerializeField]
    private SkillConditionData _conditionData;
    [SerializeField]
    private SkillTargetChoosingData _targetChoosingData;

    private Effect[] _effects;

    public CooldownCounter CooldownCounter { get; private set; }

    public Skill(EffectSettings effectSettings)
    {
        _settings = effectSettings;
        Initialize();
    }

    public Skill(Effect[] effects, float cooldown)
    {
        _effects = effects;
        CooldownCounter = new CooldownCounter(cooldown);
        Initialize();
    }

    public void Initialize()
    {
        if (_effects == null && _settings == null)
        {
            Logger.Warn($"{nameof(_effects)} and {nameof(_settings)} are null");
            return;
        }
        _effects ??= _settings.GetEffects();
        CooldownCounter ??= new CooldownCounter(_settings.Cooldown);
        if (_startsInCooldown)
        {
            CooldownCounter.Reset();
        }
    }

    public bool Invoke(CastState castState, PrioritizedTargets targets, float cooldownDivider = 1)
    {
        if (CooldownCounter == null ||
            !_conditionData.EvaluateCondition(castState.Source, targets) ||
            !CooldownCounter.TryReset(cooldownDivider))
        {
            return false;
        }

        var target = _targetChoosingData.ChooseTarget(castState.Source, targets);
        if (target == null)
        {
            return false;
        }

        castState.Target = target;
        foreach (var effect in _effects)
        {
            effect.Invoke(castState);
        }
        return true;
    }
}
