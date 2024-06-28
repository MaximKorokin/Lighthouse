using System;
using System.Linq;
using UnityEngine;

[Serializable]
public class Skill : IInitializable
{
    [SerializeField]
    private EffectSettings _settings;
    [SerializeField]
    private SkillCondition _condition;

    private Effect[] _effects;
    public CooldownCounter CooldownCounter;
    private Func<WorldObject, WorldObject, bool> _conditionPredicate;

    public void Initialize()
    {
        if (_settings == null)
        {
            return;
        }
        _effects = _settings.GetEffects();
        CooldownCounter = new CooldownCounter(_settings.Cooldown);
        CooldownCounter.Reset();

        if (_condition.HasFlag(SkillCondition.ActiveAbilityInputRecieved))
        {
            _conditionPredicate += (s, t) => InputReader.ActiveAbilityInputRecieved.HasOccured;
        }
        if (_condition.HasFlag(SkillCondition.MoveAbilityInputRecieved))
        {
            _conditionPredicate += (s, t) => InputReader.MoveAbilityInputRecieved.HasOccured;
        }

        if (_condition.HasFlag(SkillCondition.HasLPTriggeredTargetsBelowHalfAR))
        {
            _conditionPredicate += (s, t) =>
                s.TryGetComponent<TargetController>(out var targetController) &&
                targetController.TriggeredWorldObjects.Any(x =>
                    targetController.IsValidTarget(x, TargetController.LowPriorityIndex) &&
                    Vector2.Distance(s.transform.position, x.transform.position) < s.Stats[StatName.ActionRange] / 2);
        }
        if (_condition.HasFlag(SkillCondition.HasTriggeredTargets))
        {
            _conditionPredicate += (s, t) => s.TryGetComponent<TriggerController>(out var triggerController) && triggerController.TriggeredWorldObjects.Any();
        }

        if (_condition.HasFlag(SkillCondition.TargetAboveHalfActionRange))
        {
            _conditionPredicate += (s, t) => Vector2.Distance(s.transform.position, t.transform.position) > s.ActionRange / 2;
        }
        if (_condition.HasFlag(SkillCondition.TargetBelowHalfActionRange))
        {
            _conditionPredicate += (s, t) => Vector2.Distance(s.transform.position, t.transform.position) < s.ActionRange / 2;
        }
        if (_condition.HasFlag(SkillCondition.TargetBelowActionRange))
        {
            _conditionPredicate += (s, t) => Vector2.Distance(s.transform.position, t.transform.position) <= s.ActionRange;
        }
    }

    public void Invoke(WorldObject source, WorldObject target, float divider = 1)
    {
        if (_conditionPredicate != null && !_conditionPredicate.Invoke(source, target))
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

[Flags]
public enum SkillCondition
{
    ActiveAbilityInputRecieved = 1,
    MoveAbilityInputRecieved = 2,

    HasLPTriggeredTargetsBelowHalfAR = 32,
    HasTriggeredTargets = 64,

    TargetAboveHalfActionRange = 256,
    TargetBelowHalfActionRange = 512,
    TargetBelowActionRange = 1024,
}
