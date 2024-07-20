using System;
using UnityEngine;

public abstract class TextViewer : MonoBehaviour
{
    [SerializeField]
    protected TypewriterText Typewriter;

    private readonly CooldownCounter _showCounter = new(0);

    public event Action ViewFinished;

    private void Update()
    {
        if (_showCounter.IsOver())
        {
            EndView();
            ViewFinished?.Invoke();
        }
    }

    public void ShowText(string text, float showTime, TypingSpeed typingSpeed)
    {
        if (!_showCounter.IsOver())
        {
            ViewFinished?.Invoke();
        }

        StartView();
        _showCounter.Reset();
        LocalizationManager.SetLanguageChangeListener(
            Typewriter.Text,
            text,
            t =>
            {
                var timeToShow = Typewriter.SetText(t, typingSpeed);
                _showCounter.Cooldown = showTime + timeToShow;
            });
    }

    protected abstract void StartView();
    protected abstract void EndView();

    private void OnDestroy()
    {
        LocalizationManager.RemoveLanguageChangeListener(Typewriter.Text);
    }
}
