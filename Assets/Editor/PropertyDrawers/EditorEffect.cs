using System;
using System.Linq;
using UnityEditor;
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
            var propertyType = Property.managedReferenceValue.GetType();
            RootContainer.Add(propertyType.CreateTypeFoldout(Property));
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
}
