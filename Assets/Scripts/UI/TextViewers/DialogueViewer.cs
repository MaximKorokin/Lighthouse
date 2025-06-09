using UnityEngine;
using UnityEngine.UI;

public class DialogueViewer : DialogueViewerBase
{
    private const float SpeechViewTimeConstant = float.PositiveInfinity;

    [SerializeField]
    private Image _characterIcon;
    [SerializeField]
    private TextLocalizer _characterNameText;

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
            _characterNameText.SetText($"<color=#{characterPreview.Color.ToHexString()}>{displayName}</color>");
        }
    }
}
