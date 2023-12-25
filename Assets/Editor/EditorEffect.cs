using System;
using System.Linq;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

[CustomPropertyDrawer(typeof(Effect), true)]
public class EditorEffect : PropertyDrawerBase
{
    protected override void RedrawRootContainer()
    {
        RootContainer.Clear();
        if (Property.managedReferenceValue == null)
        {
            RootContainer.Add(CreateTypePopup());
        }
        else
        {
            RootContainer.Add(CreateTypeElement());
        }
    }

    private VisualElement CreateTypePopup()
    {
        var popup = new PopupField<Type>(typeof(Effect).Yield().Concat(ReflectionUtils.GetSubclasses<Effect>()).ToList(), 0);
        popup.RegisterValueChangedCallback(x =>
        {
            Property.managedReferenceValue = ReflectionUtils.CreateInstance<Effect>(x.newValue);
            Property.serializedObject.ApplyModifiedProperties();
            RedrawRootContainer();
        });
        return popup;
    }

    private VisualElement CreateTypeElement()
    {
        var propertyType = Property.managedReferenceValue.GetType();
        var foldout = new Foldout
        {
            text = propertyType.Name,
            value = false,
        };
        foreach (var p in ReflectionUtils.GetFieldsWithAttributes(propertyType, true, typeof(SerializeField), typeof(SerializeReference)))
        {
            var propertyField = new PropertyField();
            propertyField.BindProperty(Property.FindPropertyRelative(p.Name));
            foldout.Add(propertyField);
        }
        return foldout;
    }
}
