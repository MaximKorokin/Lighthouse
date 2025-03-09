using System;
using System.Reflection;

public class DefaultValueAttribute : Attribute
{
    public readonly object Value;

    public DefaultValueAttribute(object value)
    {
        Value = value;
    }
}

public static class DefaultValueAttributeExtensions
{
    public static object GetDefaultValue(this object obj)
    {
        var members = obj.GetType().GetMember(obj.ToString());
        if (members == null || members.Length == 0)
        {
            Logger.Warn($"Could not be recieve member {obj} in type {obj.GetType()}.");
            return null;
        }
        var attribute = members[0].GetCustomAttribute<DefaultValueAttribute>(false);
        return attribute?.Value;
    }
}
