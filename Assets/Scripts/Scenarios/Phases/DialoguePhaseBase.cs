public abstract class DialoguePhaseBase : SkippableActPhase
{
    public override void Invoke()
    {
        base.Invoke();
        DialoguesSystem.InitDialogue(GetDialogue());
        DialoguesSystem.DialogueFinished -= OnDialogueFinished;
        DialoguesSystem.DialogueFinished += OnDialogueFinished;
    }

    protected abstract Dialogue GetDialogue();

    private void OnDialogueFinished()
    {
        DialoguesSystem.DialogueFinished -= OnDialogueFinished;
        InvokeFinished();
    }

    protected override void OnSkipped()
    {
        DialoguesSystem.SkipDialogue();
    }
}
