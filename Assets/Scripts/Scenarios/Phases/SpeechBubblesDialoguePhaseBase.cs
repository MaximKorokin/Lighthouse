using UnityEngine;

public abstract class SpeechBubblesDialoguePhaseBase : SkippableActPhase
{
    [SerializeField]
    private float _speechViewTime;

    private SpeechBubbleDialogueViewer _viewer;

    public override void Invoke()
    {
        base.Invoke();

        _viewer = SpeechBubbleDialoguePool.Take(null);
        _viewer.SpeechViewTime = _speechViewTime;
        _viewer.SetDialogue(GetDialogue());
        _viewer.DialogueFinished -= OnDialogueFinished;
        _viewer.DialogueFinished += OnDialogueFinished;
    }

    protected abstract Dialogue GetDialogue();

    private void OnDialogueFinished()
    {
        _viewer.DialogueFinished -= OnDialogueFinished;
        SpeechBubbleDialoguePool.Return(_viewer);
        InvokeFinished();
    }

    protected override void OnSkipped()
    {
        DialoguesSystem.SkipDialogue();
    }
}
