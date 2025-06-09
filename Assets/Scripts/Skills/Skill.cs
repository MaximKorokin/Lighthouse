using System;
using System.Linq;
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

    public bool Invoke(CastState castState, PrioritizedTargets targets)
    {
        if (_effects == null || _effects.Length == 0)
        {
            return false;
        }

        if (CooldownCounter == null ||
            !_conditionData.EvaluateCondition(castState.Source, targets) ||
            !CooldownCounter.TryReset())
        {
            return false;
        }

        var chosenTargets = _targetChoosingData.ChooseTargets(castState.Source, targets);
        if (chosenTargets == null || chosenTargets.Any(x => x == null))
        {
            return false;
        }

        foreach (var target in chosenTargets)
        {
            castState.Target = target;
            foreach (var effect in _effects)
            {
                if (effect == null)
                {
                    Logger.Error($"Effect in {nameof(Skill)} is null. {nameof(EffectSettings)} name is {_settings.Preview.Name}.");
                    continue;
                }
                effect.Invoke(castState);
            }
        }
        return true;
    }
}
