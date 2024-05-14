using System;
using System.Linq;
using UnityEditor;
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

        Func<CharacterPreview, string> displayNameSelector = prev => prev != null ? $"{prev?.Name} ({prev?.Id})" : "Select value";
        var popup = new PopupField<CharacterPreview>(CharactersPreviewsDataBase.Instance.Items.ToList(), 0, displayNameSelector, displayNameSelector);
        element.Insert(0, popup);

        var characterPreviewId = Property.FindPropertyRelative(ReflectionUtils.GetBackingField(typeof(Speech), nameof(Speech.CharacterPreviewId)).Name);
        popup.RegisterValueChangedCallback(x =>
        {
            characterPreviewId.stringValue = x.newValue.Id.ToString();
            Property.serializedObject.ApplyModifiedProperties();
        });
        popup.value = CharactersPreviewsDataBase.FindById(characterPreviewId.stringValue);

        var speechText = Property.FindPropertyRelative(ReflectionUtils.GetBackingField(typeof(Speech), nameof(Speech.Text)).Name);
        element.text = popup.value != null ? $"{popup.value?.Name}: {speechText.stringValue}" : "";

        return element;
    }
}
