using System;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

[CustomPropertyDrawer(typeof(Stat))]
public class EditorStat : PropertyDrawer
{
    private const string NameFieldName = "Name";
    private const string ValueFieldName = "Value";

    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        var root = new BindableElement();
        root.Bind(property.serializedObject);
        root.style.flexDirection = FlexDirection.Row;

        var nameLabel = new Label
        {
            bindingPath = NameFieldName
        };
        nameLabel.style.width = Length.Percent(40);
        var valueField = new FloatField
        {
            bindingPath = ValueFieldName
        };
        valueField.style.width = Length.Percent(50);

        root.Add(nameLabel);
        root.Add(valueField);
        return root;
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
