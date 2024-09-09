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

        var speechText = Property.FindPropertyRelative(ReflectionUtils.GetBackingField(typeof(Speech), nameof(Speech.Text)).Name).stringValue;
        speechText = LocalizationData.GetLocalizedValue(SystemLanguage.English, speechText);

        var characterPreview = CharactersPreviewsDataBase.FindById(((Speech)Property.boxedValue).CharacterPreviewId);
        element.text = characterPreview != null ? $"{LocalizationData.GetLocalizedValue(SystemLanguage.English, characterPreview.Name)}: {speechText}" : "";

        return element;
    }
}
