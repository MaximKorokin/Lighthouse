using System;
using UnityEngine;

public class DialoguesSystem : MonoBehaviorSingleton<DialoguesSystem>
{
    [SerializeField]
    private DialogueView _dialogueView;

    private Action _endDialogueCallback;

    protected override void Awake()
    {
        base.Awake();
        _dialogueView.DialogueEnded += OnDialogueEnded;
    }

    private void OnDialogueEnded()
    {
        _endDialogueCallback?.Invoke();
        Game.Resume();
        _dialogueView.gameObject.SetActive(false);
    }

    private void InitDialogueInternal(Dialogue dialogue)
    {
        Game.Pause();
        _dialogueView.gameObject.SetActive(true);
        _dialogueView.SetDialogue(dialogue);
    }

    public static void InitDialogue(Dialogue dialogue, Action endCallback = null)
    {
        Instance._endDialogueCallback = endCallback;
        Instance.InitDialogueInternal(dialogue);
    }
}
