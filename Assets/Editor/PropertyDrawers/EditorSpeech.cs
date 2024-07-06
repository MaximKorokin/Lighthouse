using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[CustomPropertyDrawer(typeof(Speech))]
public class EditorSpeech : PropertyDrawerBase
{
    protected override void RedrawRootContainer()
    {
        RootContainer.Clear();
        RootContainer.Add(CreateSpeechFoldout());
    }

    private Foldout CreateSpeechFoldout()
    {
        var element = typeof(Speech).CreateTypeFoldout(Property);

        Func<CharacterPreview, string> displayNameSelector = prev => prev != null
            ? $"{LocalizationData.GetLocalizedValue(SystemLanguage.English, (string.IsNullOrWhiteSpace(prev?.DisplayName) ? prev?.Name : prev?.DisplayName))} ({LocalizationData.GetLocalizedValue(SystemLanguage.English, prev?.Name)})"
            : "Select value";
        var popup = new PopupField<CharacterPreview>(CharactersPreviewsDataBase.Instance.Items.ToList(), 0, displayNameSelector, displayNameSelector);
        element.Insert(0, popup);

        var characterPreviewId = Property.FindPropertyRelative(ReflectionUtils.GetBackingField(typeof(Speech), nameof(Speech.CharacterPreviewId)).Name);
        popup.RegisterValueChangedCallback(x =>
        {
            characterPreviewId.stringValue = x.newValue.Id.ToString();
            Property.serializedObject.ApplyModifiedProperties();
        });
        popup.value = CharactersPreviewsDataBase.FindById(characterPreviewId.stringValue);

        var speechText = Property.FindPropertyRelative(ReflectionUtils.GetBackingField(typeof(Speech), nameof(Speech.Text)).Name).stringValue;
        speechText = LocalizationData.GetLocalizedValue(SystemLanguage.English, speechText);
        element.text = popup.value != null ? $"{LocalizationData.GetLocalizedValue(SystemLanguage.English, popup.value?.Name)}: {speechText}" : "";

        return element;
    }
}
