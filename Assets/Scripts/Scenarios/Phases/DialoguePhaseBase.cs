using UnityEngine;

public abstract class DialoguePhaseBase : SkippableActPhase
{
    [SerializeField]
    private bool _pauseGame;

    public override void Invoke()
    {
        if (_pauseGame) GameManager.Pause();

        base.Invoke();
        DialoguesSystem.InitDialogue(GetDialogue());
        DialoguesSystem.DialogueFinished -= OnDialogueFinished;
        DialoguesSystem.DialogueFinished += OnDialogueFinished;
    }

    protected abstract Dialogue GetDialogue();

    private void OnDialogueFinished()
    {
        if (_pauseGame) GameManager.Resume();

        DialoguesSystem.DialogueFinished -= OnDialogueFinished;
        InvokeFinished();
    }

    protected override void OnSkipped()
    {
        DialoguesSystem.SkipSpeech();
    }
}
