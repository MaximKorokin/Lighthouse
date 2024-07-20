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
    private WorldObjectCanvasProvider _canvasProvider;

    public WorldObjectCanvasProvider CanvasProvider => _canvasProvider;

    private SpeechBubbleController _controller;

    public override void Invoke()
    {
        base.Invoke();
        _controller = SpeechBubblesPool.Take(null);
        _controller.transform.SetParent(_canvasProvider.CanvasController.SpeechBubbleParent.transform, false);

        _controller.ShowText(_text, _showTime, _typingSpeed);
        _controller.ViewFinished -= OnFinishedShow;
        _controller.ViewFinished += OnFinishedShow;
    }

    private void OnFinishedShow()
    {
        _controller.ViewFinished -= OnFinishedShow;
        SpeechBubblesPool.Return(_controller);
        InvokeFinished();
    }

    protected override void OnSkipped()
    {
        _controller.ShowText("", 0, TypingSpeed.Instant);
    }

    public override string IconName => "Dialogue1.png";
}
