using System.Linq;
using UnityEngine;

public class SpeechBubblesDialoguePhase : SkippableActPhase
{
    [SerializeField]
    private Dialogue _dialogue;
    [SerializeField]
    private float _speechViewTime;

    private SpeechBubbleController _controller;
    private int _currentSpeechIndex;

    public override void Invoke()
    {
        base.Invoke();

        if (_dialogue == null || _dialogue.Speeches.Length == 0)
        {
            Logger.Warn($"{nameof(_dialogue)} is null or empty");
            InvokeFinished();
            return;
        }

        _currentSpeechIndex = 0;

        _controller = SpeechBubblesPool.Take(null);
        _controller.ViewFinished -= OnViewFinished;
        _controller.ViewFinished += OnViewFinished;

        ShowCurrentSpeech();
    }

    private void ShowCurrentSpeech()
    {
        var currentSpeech = _dialogue.Speeches[_currentSpeechIndex];

        var characterObjects = CharactersMapper.GetMappedObjects(currentSpeech.CharacterPreviewId);
        if (characterObjects == null || !characterObjects.Any())
        {
            Logger.Warn($"Could not find any mapped characters with id {currentSpeech.CharacterPreviewId}");
        }
        var canvasController = characterObjects.Select(x => x.GetComponent<WorldCanvasProvider>()).FirstOrDefault(x => x != null);
        if (canvasController == null)
        {
            Logger.Warn($"Could not find any mapped characters with id {currentSpeech.CharacterPreviewId} and attached {nameof(WorldCanvasProvider)}");
        }

        _controller.transform.SetParent(canvasController.CanvasController.SpeechBubbleParent.transform, false);
        _controller.ShowText(currentSpeech.Text, _speechViewTime, currentSpeech.TypingSpeed);
    }

    private void OnViewFinished()
    {
        if (_dialogue.Speeches.Length <= ++_currentSpeechIndex)
        {
            _controller.ViewFinished -= OnViewFinished;
            SpeechBubblesPool.Return(_controller);
            InvokeFinished();
        }
        else
        {
            ShowCurrentSpeech();
        }
    }

    protected override void OnSkipped()
    {
        _currentSpeechIndex = _dialogue.Speeches.Length;
        _controller.ShowText("", 0, TypingSpeed.Instant);
    }

    public override string IconName => "Dialogue3.png";
}
