using System;
using System.Linq;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

[CustomPropertyDrawer(typeof(Effect), true)]
public class EditorEffect : PropertyDrawer
{
    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        var container = new VisualElement();
        RedrawRootContainer(container, property);
        return container;
    }

    private void RedrawRootContainer(VisualElement container, SerializedProperty property)
    {
        container.Clear();
        if (property.managedReferenceValue == null)
        {
            container.Add(CreateTypePopup(container, property));
        }
        else
        {
            container.Add(CreateTypeElement(container, property));
        }
    }

    private VisualElement CreateTypePopup(VisualElement container, SerializedProperty property)
    {
        var popup = new PopupField<Type>(typeof(Effect).Yield().Concat(ReflectionUtils.GetSubclasses<Effect>()).ToList(), 0);
        popup.RegisterValueChangedCallback(x =>
        {
            property.managedReferenceValue = ReflectionUtils.CreateInstance<Effect>(x.newValue);
            property.serializedObject.ApplyModifiedProperties();
            RedrawRootContainer(container, property);
        });
        return popup;
    }

    private VisualElement CreateTypeElement(VisualElement container, SerializedProperty property)
    {
        var propertyType = property.managedReferenceValue.GetType();
        var foldout = new Foldout
        {
            text = propertyType.Name,
            value = false,
        };
        foreach (var p in ReflectionUtils.GetFieldsWithAttributes(propertyType, true, typeof(SerializeField), typeof(SerializeReference)))
        {
            var propertyField = new PropertyField();
            propertyField.BindProperty(property.FindPropertyRelative(p.Name));
            foldout.Add(propertyField);
        }
        return foldout;
    }
}
