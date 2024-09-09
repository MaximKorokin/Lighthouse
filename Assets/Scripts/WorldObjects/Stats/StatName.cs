public enum StatName
{
    [DefaultValue(1)]
    SizeScale = 1,
    [DefaultValue(1)]
    MaxHealthPoints = 2,
    [DefaultValue(1)]
    MoveSpeedModifier = 3,
    AttackDamage = 4,
    ActionRange = 5,
    AutoLootRange = 6,
    [DefaultValue(1)]
    ActionCDModifier = 7,
    MaxShield = 8,
    HPRegen = 9,
    ShieldRegen = 10,
    ShieldDelay = 11,
    [DefaultValue(1)]
    VisionRange = 12,
}

public static class StatNameExtensions
{
    public static float GetDefaultValue(this StatName statName)
    {
        var attributes = statName.GetType().GetMember(statName.ToString())[0].GetCustomAttributes(typeof(DefaultValueAttribute), false);
        return attributes.Length > 0 ? ((DefaultValueAttribute)attributes[0]).Value : 0;
    }
}
