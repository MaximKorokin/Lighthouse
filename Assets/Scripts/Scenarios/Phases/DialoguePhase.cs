using UnityEngine;

public class DialoguePhase : ActPhase
{
    [SerializeField]
    private Dialogue _dialogue;

    public override void Invoke()
    {
        DialoguesSystem.InitDialogue(_dialogue, InvokeEnded);
    }

    public override string IconName => "Dialogue.png";
}
