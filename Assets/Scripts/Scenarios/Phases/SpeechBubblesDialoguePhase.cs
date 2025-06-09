using UnityEngine;

public class SpeechBubblesDialoguePhase : SpeechBubblesDialoguePhaseBase
{
    [SerializeField]
    private Dialogue _dialogue;

    protected override Dialogue GetDialogue()
    {
        return _dialogue;
    }

    public override string IconName => "Dialogue3.png";
}
