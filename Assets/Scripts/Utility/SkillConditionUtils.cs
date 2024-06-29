using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class SkillConditionUtils
{
    public static bool EvaluateCondition(this SkillConditionData conditionData, WorldObject source, WorldObject target)
    {
        return conditionData.Predicate switch
        {
            SkillConditionPredicate.True => true,

            SkillConditionPredicate.ActiveAbilityInputRecieved => InputReader.ActiveAbilityInputRecieved.HasOccured,
            SkillConditionPredicate.MoveAbilityInputRecieved => InputReader.MoveAbilityInputRecieved.HasOccured,

            SkillConditionPredicate.Has =>
                ConvertToDistancesWorldObjects(conditionData.ReferenceArgument, source, target).Any(),
            SkillConditionPredicate.AboveRange =>
                ConvertToDistancesWorldObjects(conditionData.ReferenceArgument, source, target)
                    .Any(x => Vector2.Distance(source.transform.position, x.transform.position) > conditionData.ValueArgument.ConvertToFloat(source)),
            SkillConditionPredicate.BelowRange =>
                ConvertToDistancesWorldObjects(conditionData.ReferenceArgument, source, target)
                    .Any(x => Vector2.Distance(source.transform.position, x.transform.position) < conditionData.ValueArgument.ConvertToFloat(source)),
            _ => false
        };
    }

    public static float ConvertToFloat(this SkillConditionValueArgument valueArgument, WorldObject source)
    {
        return valueArgument switch
        {
            SkillConditionValueArgument.None => 0f,
            SkillConditionValueArgument.ActionRange => source.ActionRange,
            SkillConditionValueArgument.HalfActionRange => source.ActionRange / 2,
            _ => 0f,
        };
    }

    public static IEnumerable<WorldObject> ConvertToDistancesWorldObjects(this SkillConditionReferenceArgument referenceArgument, WorldObject source, WorldObject target)
    {
        return referenceArgument switch
        {
            SkillConditionReferenceArgument.None => Enumerable.Empty<WorldObject>(),
            SkillConditionReferenceArgument.Target =>
                target == null ? Enumerable.Empty<WorldObject>() : target.Yield(),
            SkillConditionReferenceArgument.TriggeredTargets =>
                source.TryGetComponent<TriggerController>(out var controller) ? controller.TriggeredWorldObjects : Enumerable.Empty<WorldObject>(),
            SkillConditionReferenceArgument.LowPriorityTriggeredTargets =>
                source.TryGetComponent<TargetController>(out var controller)
                ? controller.TriggeredWorldObjects.Where(x => controller.IsValidTarget(x, TargetController.LowPriorityIndex))
                : Enumerable.Empty<WorldObject>(),
            SkillConditionReferenceArgument.HighPriorityTriggeredTargets =>
                source.TryGetComponent<TargetController>(out var controller)
                ? controller.TriggeredWorldObjects.Where(x => controller.IsValidTarget(x, TargetController.HighPriorityIndex))
                : Enumerable.Empty<WorldObject>(),
            _ => Enumerable.Empty<WorldObject>(),
        };
    }
}
