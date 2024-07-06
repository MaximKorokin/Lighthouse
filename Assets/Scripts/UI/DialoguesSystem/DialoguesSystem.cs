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

    private void OnDialogueFinished(Dialogue dialogue)
    {
        if (dialogue != null && dialogue.PauseGame)
        {
            GameManager.Resume();
        }
        _dialogueView.gameObject.SetActive(false);
        DialogueFinished?.Invoke();
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

    public static void InitDialogue(Dialogue dialogue)
    {
        Instance.InitDialogueInternal(dialogue);
    }
}
