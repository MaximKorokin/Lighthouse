using System;
using UnityEngine.EventSystems;

public abstract class DialogueViewerBase : AudioTextViewer, IPointerDownHandler
{
    private readonly ReadOnceValue<bool> _canTransitToNextSpeech = new(true);

    private Dialogue _currentDialogue;
    private int _currentSpeechIndex;

    protected Speech CurrentSpeech => _currentDialogue.Speeches[_currentSpeechIndex];

    public event Action DialogueFinished;

    public float SpeechViewTime { get; set; }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_currentDialogue == null)
        {
            return;
        }

        if (Typewriter.IsTyping)
        {
            _canTransitToNextSpeech.Set(false);
            ViewText(_currentDialogue.Speeches[_currentSpeechIndex].Text, SpeechViewTime, TypingSpeed.Instant);
        }
        else
        {
            FinishViewText();
        }
    }

    public virtual void SetDialogue(Dialogue dialogue)
    {
        if (!gameObject.activeInHierarchy)
        {
            return;
        }
        if (dialogue == null)
        {
            Logger.Warn("Dialogue is null");
            DialogueFinished?.Invoke();
            return;
        }
        _currentDialogue = dialogue;
        _currentSpeechIndex = 0;
        ViewText(CurrentSpeech.Text, SpeechViewTime, CurrentSpeech.TypingSpeed);
    }

    protected override void OnViewFinished()
    {
        if (_canTransitToNextSpeech)
        {
            _currentSpeechIndex++;
            if (_currentDialogue.Speeches.Length <= _currentSpeechIndex)
            {
                DialogueFinished?.Invoke();
            }
            else
            {
                ViewText(CurrentSpeech.Text, SpeechViewTime, CurrentSpeech.TypingSpeed);
            }
        }
    }
}
