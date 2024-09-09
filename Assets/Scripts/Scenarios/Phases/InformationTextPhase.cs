using UnityEngine;

public class InformationTextPhase : SkippableActPhase
{
    [SerializeField]
    private string _text;
    [SerializeField]
    private float _showTime;
    [SerializeField]
    private TypingSpeed _typingSpeed;

    public override void Invoke()
    {
        base.Invoke();
        InformationText.Instance.ShowText(_text, _showTime, _typingSpeed);
        InformationText.Instance.ViewFinished -= OnFinishedShow;
        InformationText.Instance.ViewFinished += OnFinishedShow;
    }

    private void OnFinishedShow()
    {
        InformationText.Instance.ViewFinished -= OnFinishedShow;
        InvokeFinished();
    }

    protected override void OnSkipped()
    {
        InformationText.Instance.ShowText("", 0, TypingSpeed.Instant);
    }

    public override string IconName => "Info.png";
}
