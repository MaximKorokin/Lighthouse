using System;

public class DefaultValueAttribute : Attribute
{
    public readonly float Value;

    public DefaultValueAttribute(float value)
    {
        Value = value;
    }
}

public static class DefaultValueAttributeExtensions
{
    public static float GetDefaultValue(this object obj)
    {
        var attributes = obj.GetType().GetMember(obj.ToString())[0].GetCustomAttributes(typeof(DefaultValueAttribute), false);
        return attributes.Length > 0 ? ((DefaultValueAttribute)attributes[0]).Value : 0;
    }
}