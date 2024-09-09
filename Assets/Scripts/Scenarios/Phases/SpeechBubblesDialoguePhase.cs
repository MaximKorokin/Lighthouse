using UnityEngine;

public class SpeechBubblesDialoguePhase : SkippableActPhase
{
    [SerializeField]
    private Dialogue _dialogue;
    [SerializeField]
    private float _speechViewTime;

    private SpeechBubbleDialogueViewer _viewer;

    public override void Invoke()
    {
        base.Invoke();

        if (_dialogue == null || _dialogue.Speeches.Length == 0)
        {
            Logger.Warn($"{nameof(_dialogue)} is null or empty");
            InvokeFinished();
            return;
        }

        _viewer = SpeechBubbleDialoguePool.Take(null);
        _viewer.SpeechViewTime = _speechViewTime;
        _viewer.SetDialogue(_dialogue);
        _viewer.DialogueFinished -= OnDialogueFinished;
        _viewer.DialogueFinished += OnDialogueFinished;
    }

    private void OnDialogueFinished()
    {
        _viewer.DialogueFinished -= OnDialogueFinished;
        SpeechBubbleDialoguePool.Return(_viewer);
        InvokeFinished();
    }

    protected override void OnSkipped()
    {
        _viewer.FinishViewText();
    }

    public override string IconName => "Dialogue3.png";
}
