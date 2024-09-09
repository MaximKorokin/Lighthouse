using UnityEngine;

public class SpeechBubblePhase : SkippableActPhase
{
    [SerializeField]
    private string _text;
    [SerializeField]
    private float _showTime;
    [SerializeField]
    private TypingSpeed _typingSpeed;
    [SerializeField]
    private AudioClip _typingSoundOverride;
    [SerializeField]
    private WorldCanvasProvider _canvasProvider;

    public WorldCanvasProvider CanvasProvider => _canvasProvider;

    private SpeechBubbleViewer _viewer;

    public override void Invoke()
    {
        base.Invoke();
        _viewer = SpeechBubblePool.Take(null);
        _viewer.transform.SetParent(_canvasProvider.CanvasController.SpeechBubbleParent.transform, false);

        if (_typingSoundOverride != null)
        {
            _viewer.SetTypingSound(_typingSoundOverride);
        }
        else if (_canvasProvider.TryGetComponent<CharacterSetter>(out var characterSetter))
        {
            var characterPreview = CharactersPreviewsDataBase.FindById(characterSetter.CharacterPreviewId);
            if (characterPreview != null)
            {
                _viewer.SetTypingSound(characterPreview.TypingSound);
            }
        }

        _viewer.ViewText(_text, _showTime, _typingSpeed);
        _viewer.ViewFinished -= OnViewFinished;
        _viewer.ViewFinished += OnViewFinished;
    }

    private void OnViewFinished()
    {
        _viewer.ViewFinished -= OnViewFinished;
        SpeechBubblePool.Return(_viewer);
        InvokeFinished();
    }

    protected override void OnSkipped()
    {
        _viewer.FinishViewText();
    }

    public override string IconName => "Dialogue1.png";
}
