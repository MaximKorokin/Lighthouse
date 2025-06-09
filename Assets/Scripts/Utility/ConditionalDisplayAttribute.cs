using UnityEngine;

public class ConditionalDisplayAttribute : PropertyAttribute
{
    public string[] FieldPaths { get; set; }
    public object[] EqualityObjects { get; set; }

    public ConditionalDisplayAttribute(string fieldPath, object equalityObject)
    {
        FieldPaths = new[] { fieldPath };
        EqualityObjects = new[] { equalityObject };
    }

    public ConditionalDisplayAttribute(string[] fieldPaths, object[] equalityObjects)
    {
        FieldPaths = fieldPaths;
        EqualityObjects = equalityObjects;
    }
}
