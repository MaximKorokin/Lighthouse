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

    private void OnDialogueEnded(Dialogue dialogue)
    {
        if (dialogue != null && dialogue.PauseGame)
        {
            GameManager.Resume();
        }
        _endDialogueCallback?.Invoke();
        _dialogueView.gameObject.SetActive(false);
    }

    private void InitDialogueInternal(Dialogue dialogue)
    {
        if (dialogue != null && dialogue.PauseGame)
        {
            GameManager.Pause();
        }
        _dialogueView.gameObject.SetActive(true);
        _dialogueView.SetDialogue(dialogue);
    }

    public static void InitDialogue(Dialogue dialogue, Action endCallback = null)
    {
        Instance._endDialogueCallback = endCallback;
        Instance.InitDialogueInternal(dialogue);
    }
}
