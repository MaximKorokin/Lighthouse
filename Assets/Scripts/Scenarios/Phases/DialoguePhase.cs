using UnityEngine;

public class DialoguePhase : DialoguePhaseBase
{
    [SerializeField]
    private Dialogue _dialogue;

    protected override Dialogue GetDialogue()
    {
        return _dialogue;
    }

    public override void Invoke()
    {
        GameManager.IsControlInputBlocked = true;
        base.Invoke();
    }

    protected override void OnDialogueFinished()
    {
        base.OnDialogueFinished();
        GameManager.IsControlInputBlocked = false;
    }

    public override string IconName => "Dialogue2.png";
}
