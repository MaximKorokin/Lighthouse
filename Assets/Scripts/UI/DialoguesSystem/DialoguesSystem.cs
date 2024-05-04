using UnityEngine;

public class DialoguesSystem : MonoBehaviorSingleton<DialoguesSystem>
{
    [SerializeField]
    private DialogueView _dialogueView;

    protected override void Awake()
    {
        base.Awake();
        _dialogueView.DialogueEnded += OnDialogueEnded;
    }

    private void OnDialogueEnded()
    {
        Game.Resume();
        _dialogueView.gameObject.SetActive(false);
    }

    private void InitDialogueInternal(Dialogue dialogue)
    {
        Game.Pause();
        _dialogueView.gameObject.SetActive(true);
        _dialogueView.SetDialogue(dialogue);
    }

    public static void InitDialogue(Dialogue dialogue)
    {
        Instance.InitDialogueInternal(dialogue);
    }
}
