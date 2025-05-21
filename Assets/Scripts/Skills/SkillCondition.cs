using System;

[Serializable]
public struct SkillConditionData
{
    public SkillConditionPredicate Predicate;
    public SkillConditionReferenceArgument ReferenceArgument;
    public SkillConditionValueArgument ValueArgument;
}

public enum SkillConditionPredicate
{
    True = 0,                               // Always returns True

    ActiveAbilityInputRecieved = 1 << 0,    // 1
    MoveAbilityInputRecieved = 1 << 1,      // 2

    Has = 1 << 10,                          // 1024
    AboveRange = 1 << 11,                   // 2048
    BelowRange = 1 << 12,                   // 4096

    IsAccessibleBelowRange = 1 << 15,
}

public enum SkillConditionReferenceArgument
{
    None = 0,
    Target = 1,
    TriggeredTargets = 2,
    PrimaryTargets = 3,
    SecondaryTargets = 4,
}

public enum SkillConditionValueArgument
{
    None = 0,
    ActionRange = 1,
    HalfActionRange = 2,
    ThirdActionRange = 3,
    QuarterActionRange = 4,
}
