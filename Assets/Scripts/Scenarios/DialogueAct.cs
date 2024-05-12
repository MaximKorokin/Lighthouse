using UnityEngine;

public class DialogueAct : ScenarioAct
{
    [SerializeField]
    private Dialogue _dialogue;

    protected override void Act()
    {
        DialoguesSystem.InitDialogue(_dialogue, () => IsUsed = true);
    }
}
