using UnityEngine;

public class DialoguesDistributorGetPhase : DialoguePhaseBase
{
    [SerializeField]
    private DialoguesDistributorKey _key;

    protected override Dialogue GetDialogue()
    {
        return DialoguesDistributorAddPhase.GetNextDialogue(_key);
    }

    public override string IconName => "DialogueRemove.png";
}
