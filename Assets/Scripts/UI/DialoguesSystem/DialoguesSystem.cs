using System;
using UnityEngine;

public class DialoguesSystem : MonoBehaviorSingleton<DialoguesSystem>
{
    [SerializeField]
    private DialogueView _dialogueView;

    public static event Action DialogueFinished;

    protected override void Awake()
    {
        base.Awake();
        _dialogueView.DialogueFinished += OnDialogueFinished;
    }

    private void OnDialogueFinished()
    {
        _dialogueView.gameObject.SetActive(false);
        DialogueFinished?.Invoke();
    }

    private void InitDialogueInternal(Dialogue dialogue)
    {
        _dialogueView.gameObject.SetActive(true);
        _dialogueView.SetDialogue(dialogue);
    }

    public static void InitDialogue(Dialogue dialogue)
    {
        if (Instance == null)
        {
            Logger.Warn($"{nameof(Instance)} of {nameof(DialoguesSystem)} singletone is null.");
            return;
        }
        Instance.InitDialogueInternal(dialogue);
    }

    public static void SkipDialogue()
    {
        if (Instance == null)
        {
            return;
        }
        Instance.OnDialogueFinished();
    }
}
