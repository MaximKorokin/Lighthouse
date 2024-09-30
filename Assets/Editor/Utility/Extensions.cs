using System;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public static class Extensions
{
    public static Foldout CreateTypeFoldout(this Type type, SerializedProperty property, bool opened = false)
    {
        var foldout = new Foldout
        {
            text = type.Name,
            value = opened,
        };
        foreach (var field in ReflectionUtils.GetFieldsWithAttributes(type, true, typeof(SerializeField), typeof(SerializeReference)))
        {
            if (field.HasAttribute(typeof(HideInInspector)))
            {
                continue;
            }
            var relativeProperty = property.FindPropertyRelative(field.Name);
            if (relativeProperty != null)
            {
                var propertyField = new PropertyField();
                propertyField.BindProperty(relativeProperty);
                foldout.Add(propertyField);
            }
        }
        return foldout;
    }
}
