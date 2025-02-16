using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class SkillUtils
{
    #region Skill condition
    public static bool EvaluateCondition(this SkillConditionData conditionData, WorldObject source, PrioritizedTargets targets)
    {
        return conditionData.Predicate switch
        {
            SkillConditionPredicate.True => true,

            SkillConditionPredicate.ActiveAbilityInputRecieved => InputReader.ActiveAbilityInputRecieved.HasOccured,
            SkillConditionPredicate.MoveAbilityInputRecieved => InputReader.MoveAbilityInputRecieved.HasOccured,

            SkillConditionPredicate.Has =>
                ConvertToWorldObjects(conditionData.ReferenceArgument, targets).Any(),
            SkillConditionPredicate.AboveRange =>
                ConvertToWorldObjects(conditionData.ReferenceArgument, targets)
                    .Any(x => Vector2.Distance(source.transform.position, x.transform.position) > conditionData.ValueArgument.ConvertToFloat(source)),
            SkillConditionPredicate.BelowRange =>
                ConvertToWorldObjects(conditionData.ReferenceArgument, targets)
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
            SkillConditionValueArgument.ThirdActionRange => source.ActionRange / 3,
            SkillConditionValueArgument.QuarterActionRange => source.ActionRange / 4,
            _ => 0f,
        };
    }

    public static IEnumerable<WorldObject> ConvertToWorldObjects(this SkillConditionReferenceArgument referenceArgument, PrioritizedTargets targets)
    {
        return referenceArgument switch
        {
            SkillConditionReferenceArgument.None => Enumerable.Empty<WorldObject>(),
            SkillConditionReferenceArgument.Target => targets.MainTarget.Yield(),
            SkillConditionReferenceArgument.TriggeredTargets => targets.Targets,
            SkillConditionReferenceArgument.SecondaryTargets => targets.SecondaryTargets,
            SkillConditionReferenceArgument.PrimaryTargets => targets.PrimaryTargets,
            _ => Enumerable.Empty<WorldObject>(),
        };
    }

    #endregion

    #region Skill target choosing

    public static IEnumerable<WorldObject> ChooseTargets(this SkillTargetChoosingData targetChoosingData, WorldObject source, PrioritizedTargets targets)
    {
        var toChooseFrom = targetChoosingData.Type.ConvertToWorldObjects(source, targets);

        if (!toChooseFrom.Any()) return null;

        return targetChoosingData.Func switch
        {
            SkillTargetChoosingFunc.First => toChooseFrom.Take(1),
            SkillTargetChoosingFunc.Random => toChooseFrom.Skip(Random.Range(0, toChooseFrom.Count() - 1)).Take(1),
            SkillTargetChoosingFunc.Nearest => toChooseFrom.MinBy(w => (w.transform.position - source.transform.position).sqrMagnitude).Yield(),
            SkillTargetChoosingFunc.All => toChooseFrom,
            _ => null,
        };
    }

    public static IEnumerable<WorldObject> ConvertToWorldObjects(this SkillTargetChoosingType targetChoosingType, WorldObject source, PrioritizedTargets targets)
    {
        return targetChoosingType switch
        {
            SkillTargetChoosingType.Main => targets.MainTarget.Yield(),
            SkillTargetChoosingType.Primary => targets.PrimaryTargets,
            SkillTargetChoosingType.Secondary => targets.SecondaryTargets,
            SkillTargetChoosingType.Source => source.Yield(),
            _ => Enumerable.Empty<WorldObject>(),
        };
    }

    #endregion
}
