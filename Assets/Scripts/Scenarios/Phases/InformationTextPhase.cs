using UnityEngine;

public class InformationTextPhase : ActPhase
{
    [SerializeField]
    private string _text;
    [SerializeField]
    private float _showTime;
    [SerializeField]
    private TypingSpeed _typingSpeed;

    public override void Invoke()
    {
        InformationText.ShowText(_text, _showTime, _typingSpeed);
        InformationText.FinishedShow -= OnFinishedShow;
        InformationText.FinishedShow += OnFinishedShow;
    }

    private void OnFinishedShow()
    {
        InformationText.FinishedShow -= OnFinishedShow;
        InvokeFinished();
    }

    public override string IconName => "Text.png";
}
