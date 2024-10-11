using System;
using System.Linq;
using UnityEditor;
using UnityEngine.UIElements;

[CustomPropertyDrawer(typeof(Effect), true)]
public class EditorEffect : PropertyDrawerBase
{
    private static Effect _effectToCopy;

    protected override void RedrawRootContainer(VisualElement rootContainer, SerializedProperty property)
    {
        rootContainer.Clear();

        var buttonsContainer = new VisualElement();
        buttonsContainer.style.flexDirection = FlexDirection.RowReverse;
        buttonsContainer.style.alignSelf = Align.FlexEnd;
        rootContainer.Add(buttonsContainer);
        buttonsContainer.Add(CreatePasteButton(rootContainer, property));

        if (property.managedReferenceValue == null)
        {
            var typePopup = CreateTypePopup(rootContainer, property);
            typePopup.style.flexGrow = 1;
            rootContainer.Add(typePopup);
            rootContainer.Add(buttonsContainer);
            rootContainer.style.flexDirection = FlexDirection.Row;
        }
        else
        {
            buttonsContainer.Add(CreateCopyButton(rootContainer, property));

            var typeFoldout = property.managedReferenceValue.GetType().CreateTypeFoldout(property);
            var foldoutToggle = typeFoldout.Q<Toggle>();
            foldoutToggle.style.marginRight = 0;
            foldoutToggle.Add(buttonsContainer);
            rootContainer.Add(typeFoldout);
        }
    }

    private VisualElement CreateTypePopup(VisualElement rootContainer, SerializedProperty property)
    {
        var popup = new PopupField<Type>(typeof(Effect).Yield().Concat(ReflectionUtils.GetSubclasses<Effect>()).ToList(), 0);
        popup.RegisterValueChangedCallback(x =>
        {
            property.managedReferenceValue = ReflectionUtils.CreateInstance<Effect>(x.newValue);
            property.serializedObject.ApplyModifiedProperties();
            RedrawRootContainer(rootContainer, property);
        });
        return popup;
    }

    private Button CreateCopyButton(VisualElement rootContainer, SerializedProperty property)
    {
        var button = new Button(() =>
        {
            _effectToCopy = property.managedReferenceValue as Effect;
        })
        { text = "Copy" };
        button.style.width = 50;
        return button;
    }

    private Button CreatePasteButton(VisualElement rootContainer, SerializedProperty property)
    {
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

            RedrawRootContainer(rootContainer, property);
        })
        { text = "Paste" };
        button.style.width = 50;
        return button;
    }
}
