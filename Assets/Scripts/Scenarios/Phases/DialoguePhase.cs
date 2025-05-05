using UnityEngine;

public class DialoguePhase : DialoguePhaseBase
{
    [SerializeField]
    private Dialogue _dialogue;

    protected override Dialogue GetDialogue()
    {
        return _dialogue;
    }

    public override string IconName => "Dialogue2.png";
}
