using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[CustomPropertyDrawer(typeof(Speech))]
public class EditorSpeech : PropertyDrawerBase
{
    protected override void RedrawRootContainer(VisualElement rootContainer, SerializedProperty property)
    {
        rootContainer.Clear();

        var element = typeof(Speech).CreateTypeFoldout(property);

        var speechText = property.FindPropertyRelative(ReflectionUtils.GetBackingField(typeof(Speech), nameof(Speech.Text)).Name).stringValue;
        speechText = LocalizationData.GetLocalizedValue(SystemLanguage.English, speechText);

        var characterPreview = CharactersPreviewsDataBase.FindById(((Speech)property.boxedValue).CharacterPreviewId);
        element.text = characterPreview != null ? $"{LocalizationData.GetLocalizedValue(SystemLanguage.English, characterPreview.Name)}: {speechText}" : "";

        rootContainer.Add(element);
    }
}
