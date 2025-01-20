using System;
using UnityEngine;

public class DialoguesSystem : MonoBehaviorSingleton<DialoguesSystem>
{
    private const UIState UIStateKey = UIState.Dialogue;

    [SerializeField]
    private DialogueViewer _dialogueView;

    public static event Action DialogueFinished;

    protected override void Awake()
    {
        base.Awake();
        _dialogueView.DialogueFinished += OnDialogueFinished;
    }

    private void OnDialogueFinished()
    {
        UIStateManager.Observable.Set(UIStateKey, false);
        DialogueFinished?.Invoke();
    }

    private void InitDialogueInternal(Dialogue dialogue)
    {
        _dialogueView.SetDialogue(dialogue);
    }

    public static void InitDialogue(Dialogue dialogue)
    {
        UIStateManager.Observable.Set(UIStateKey, true);
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
        Instance._dialogueView.FinishViewText();
    }
}
