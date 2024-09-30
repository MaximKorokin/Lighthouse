using UnityEngine;

public class ConditionalDisplayAttribute : PropertyAttribute
{
    public string FieldPath { get; set; }
    public object EqualityObject { get; set; }

    public ConditionalDisplayAttribute(string fieldPath, object equalityObject)
    {
        FieldPath = fieldPath;
        EqualityObject = equalityObject;
    }
}
