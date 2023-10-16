using System;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(Stat))]
public class EditorStat : PropertyDrawer
{
    private const string NameFieldName = "Name";
    private const string ValueFieldName = "Value";

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        position = EditorGUI.PrefixLabel(position, new GUIContent(GetStatName(property)));

        var valueRect = new Rect(position.x, position.y, 100, EditorGUIUtility.singleLineHeight);
        var valueProperty = property.FindPropertyRelative(ValueFieldName);
        valueProperty.floatValue = EditorGUI.FloatField(valueRect, valueProperty.floatValue);

        EditorGUI.EndProperty();
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
