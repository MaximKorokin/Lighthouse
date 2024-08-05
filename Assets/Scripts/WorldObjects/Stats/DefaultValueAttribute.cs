using System;

public class DefaultValueAttribute : Attribute
{
    public readonly float Value;

    public DefaultValueAttribute(float value)
    {
        Value = value;
    }
}