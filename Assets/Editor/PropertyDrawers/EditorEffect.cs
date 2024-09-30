using System;
using System.Linq;
using UnityEditor;
using UnityEngine.UIElements;

[CustomPropertyDrawer(typeof(Effect), true)]
public class EditorEffect : PropertyDrawerBase
{
    private static Effect _effectToCopy;

    protected override void RedrawRootContainer()
    {
        RootContainer.Clear();

        var buttonsContainer = new VisualElement();
        buttonsContainer.style.flexDirection = FlexDirection.RowReverse;
        buttonsContainer.style.alignSelf = Align.FlexEnd;
        RootContainer.Add(buttonsContainer);
        buttonsContainer.Add(CreatePasteButton());

        if (Property.managedReferenceValue == null)
        {
            var typePopup = CreateTypePopup();
            typePopup.style.flexGrow = 1;
            RootContainer.Add(typePopup);
            RootContainer.Add(buttonsContainer);
            RootContainer.style.flexDirection = FlexDirection.Row;
        }
        else
        {
            buttonsContainer.Add(CreateCopyButton());

            var typeFoldout = Property.managedReferenceValue.GetType().CreateTypeFoldout(Property);
            var foldoutToggle = typeFoldout.Q<Toggle>();
            foldoutToggle.style.marginRight = 0;
            foldoutToggle.Add(buttonsContainer);
            RootContainer.Add(typeFoldout);
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

    private Button CreateCopyButton()
    {
        var property = Property;
        var button = new Button(() =>
        {
            _effectToCopy = property.managedReferenceValue as Effect;
        })
        { text = "Copy" };
        button.style.width = 50;
        return button;
    }

    private Button CreatePasteButton()
    {
        // Making closure because properties may be changed
        // Seems like the PropertyDrawer object is being reused for each type
        var property = Property;
        var rootContainer = RootContainer;
        var button = new Button(() =>
        {
            if (_effectToCopy == null)
            {
                Logger.Warn("No Effect copied.");
                return;
            }

            var json = EditorJsonUtility.ToJson(_effectToCopy);
            var copiedEffect = ReflectionUtils.CreateInstance<Effect>(_effectToCopy.GetType());
            EditorJsonUtility.FromJsonOverwrite(json, copiedEffect);

            property.managedReferenceValue = copiedEffect;
            property.serializedObject.ApplyModifiedProperties();
            property.serializedObject.Update();

            RootContainer = rootContainer;
            Property = property;
            RedrawRootContainer();
        })
        { text = "Paste" };
        button.style.width = 50;
        return button;
    }
}
