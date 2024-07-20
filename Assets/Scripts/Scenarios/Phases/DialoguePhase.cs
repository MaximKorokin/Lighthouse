using UnityEngine;

public class DialoguePhase : SkippableActPhase
{
    [SerializeField]
    private Dialogue _dialogue;

    public override void Invoke()
    {
        base.Invoke();
        DialoguesSystem.InitDialogue(_dialogue);
        DialoguesSystem.DialogueFinished -= OnDialogueFinished;
        DialoguesSystem.DialogueFinished += OnDialogueFinished;
    }

    private void OnDialogueFinished()
    {
        DialoguesSystem.DialogueFinished -= OnDialogueFinished;
        InvokeFinished();
    }

    protected override void OnSkipped()
    {
        DialoguesSystem.SkipDialogue();
    }

    public override string IconName => "Dialogue2.png";
}
