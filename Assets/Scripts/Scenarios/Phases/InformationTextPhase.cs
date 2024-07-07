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
        InformationText.ShowText(_text, _showTime, _typingSpeed);
        InformationText.FinishedShow -= OnFinishedShow;
        InformationText.FinishedShow += OnFinishedShow;
    }

    private void OnFinishedShow()
    {
        InformationText.FinishedShow -= OnFinishedShow;
        InvokeFinished();
    }

    protected override void OnSkipped()
    {
        InformationText.ShowText("", 0, TypingSpeed.Instant);
    }

    public override string IconName => "Info.png";
}
