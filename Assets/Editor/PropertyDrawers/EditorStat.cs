using System;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

[CustomPropertyDrawer(typeof(Stat))]
public class EditorStat : PropertyDrawerBase
{
    private const string NameFieldName = "Name";
    private const string ValueFieldName = "Value";

    protected override void RedrawRootContainer(VisualElement rootContainer, SerializedProperty property)
    {
        var root = new BindableElement();
        root.Bind(property.serializedObject);

        var valueField = new FloatField(GetStatName(property))
        {
            bindingPath = ValueFieldName
        };
        valueField.style.width = Length.Percent(100);

        root.Add(valueField);
        rootContainer.Add(root);
    }

    public static string GetStatName(SerializedProperty property)
    {
        return Enum.GetName(typeof(StatName), property.FindPropertyRelative(NameFieldName).intValue);
    }

    public static void SetStatName(SerializedProperty property, string name)
    {
        property.FindPropertyRelative(NameFieldName).intValue = (int)Enum.Parse(typeof(StatName), name);
    }
}
