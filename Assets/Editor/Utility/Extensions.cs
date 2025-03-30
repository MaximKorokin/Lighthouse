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
        foldout.PopulateVisualElement(type, property);
        return foldout;
    }

    public static void PopulateVisualElement(this VisualElement visualElement, Type type, SerializedProperty property, Func<SerializedProperty, VisualElement> childPropertyOverride = null)
    {
        foreach (var field in ReflectionUtils.GetFieldsWithAttributes(type, true, typeof(SerializeField), typeof(SerializeReference)))
        {
            if (field.HasAttribute(typeof(HideInInspector)))
            {
                continue;
            }

            var relativeProperty = property.FindPropertyRelative(field.Name);
            if (relativeProperty != null)
            {
                VisualElement propertyField = null;
                if (childPropertyOverride != null)
                {
                    propertyField = childPropertyOverride(relativeProperty);
                }
                if (propertyField == null)
                {
                    propertyField = new PropertyField();
                    (propertyField as PropertyField).BindProperty(relativeProperty);
                }
                visualElement.Add(propertyField);
            }
        }
    }
}
