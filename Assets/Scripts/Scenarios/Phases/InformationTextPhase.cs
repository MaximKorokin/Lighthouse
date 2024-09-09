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
        InformationTextViewer.Instance.ViewText(_text, _showTime, _typingSpeed);
        InformationTextViewer.Instance.ViewFinished -= OnViewFinished;
        InformationTextViewer.Instance.ViewFinished += OnViewFinished;
    }

    private void OnViewFinished()
    {
        InformationTextViewer.Instance.ViewFinished -= OnViewFinished;
        InvokeFinished();
    }

    protected override void OnSkipped()
    {
        InformationTextViewer.Instance.FinishViewText();
    }

    public override string IconName => "Info.png";
}
