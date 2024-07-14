using System;

[Serializable]
public struct SkillTargetChoosingData
{
    public SkillTargetChoosingType Type;
    public SkillTargetChoosingFunc Func;
}

public enum SkillTargetChoosingType
{
    Main = 0,
    Primary = 1,
    Secondary = 2,
}

public enum SkillTargetChoosingFunc
{
    First = 0,
    Nearest = 1,
    Random = 2,
}