using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueViewer : DialogueViewerBase
{
    private const float SpeechViewTimeConstant = 3;

    [SerializeField]
    private Image _characterIcon;
    [SerializeField]
    private TMP_Text _characterNameText;

    protected override void Awake()
    {
        base.Awake();
        SpeechViewTime = SpeechViewTimeConstant;
    }

    protected override void OnViewStarted()
    {
        var characterPreview = CharactersPreviewsDataBase.FindById(CurrentSpeech.CharacterPreviewId);
        if (characterPreview != null)
        {
            SetTypingSound(characterPreview.TypingSound);
            _characterIcon.sprite = characterPreview.Icon;
            var displayName = characterPreview.GetName();
            LocalizationManager.SetLanguageChangeListener(
                _characterNameText,
                $"<color=#{characterPreview.Color.ToHexString()}>{displayName}</color>",
                text => _characterNameText.text = text);
        }
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        LocalizationManager.RemoveLanguageChangeListener(_characterNameText);
    }
}
