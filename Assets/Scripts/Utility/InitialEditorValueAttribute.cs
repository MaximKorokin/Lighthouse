using System;

/// <summary>
/// Default C# initializer is not enough for fileds marked with [SerializeField] when duplicating GameObject
/// </summary>
public class InitialEditorValueAttribute : Attribute
{
    public object Value { get; set; }

    public InitialEditorValueAttribute(object value)
    {
        Value = value;
    }
}
